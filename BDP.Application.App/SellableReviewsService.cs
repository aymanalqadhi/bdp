using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services.Exceptions;
using BDP.Domain.Services.Interfaces;

namespace BDP.Application.App;

public class SellableReviewsService : ISellableReviewsService
{
    #region Private fields

    private readonly IUnitOfWork _uow;

    #endregion

    #region Ctors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the app</param>
    public SellableReviewsService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion

    #region Public methods

    /// <inheritdoc/>
    public IAsyncEnumerable<SellableReview> GetForAsync(Sellable item, int page, int pageSize)
        => _uow.SellableReviews.FilterAsync(page, pageSize, r => r.Item.Id == item.Id, descOrder: true);

    /// <inheritdoc/>
    public Task<SellableReview?> GetReviewForUser(Sellable item, User user)
        => _uow.SellableReviews.FirstOrDefaultAsync(r => r.Item.Id == item.Id && r.LeftBy.Id == user.Id);

    /// <inheritdoc/>
    public async Task<SellableReview> ReviewAsync(
        Sellable item, User user, double rating, string? comment = null)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        if (rating < 0 || rating > 5)
            throw new InvalidRatingValueException(rating);

        if (!await CanReviewAsync(item, user))
            throw new ItemAlreadyReviewedException($"item #{item.Id} cannot be reviewed by user #{user.Id}");

        var review = new SellableReview
        {
            Rating = rating,
            Comment = comment,
            LeftBy = user,
            Item = item
        };

        _uow.SellableReviews.Add(review);
        await _uow.CommitAsync(tx);

        return review;
    }

    /// <inheritdoc/>
    public async Task<SellableReviewInfo> ReviewInfoForAsync(Sellable item)
    {
        var reviews = _uow.SellableReviews.FilterAsync(r => r.Item.Id == item.Id);

        var reviewsCount = await reviews.CountAsync();
        var ratingAvg = reviewsCount > 0 ? await reviews.AverageAsync(r => r.Rating) : 0;

        return new SellableReviewInfo(ratingAvg, reviewsCount);
    }

    /// <inheritdoc/>
    public async Task<bool> CanReviewAsync(Sellable item, User user)
    {
        if (item is Product)
        {
            if (!await _uow.ProductOrders.AnyAsync(
                o => o.Product.Id == item.Id &&
                     o.Transaction.From.Id == user.Id &&
                     o.Transaction.Confirmation != null &&
                     o.Transaction.Confirmation.Outcome == TransactionConfirmationOutcome.Confirmed))
            {
                return false;
            }
        }
        else if (item is Service)
        {
            if (!await _uow.ServiceReservations.AnyAsync(
                o => o.Service.Id == item.Id &&
                     o.Transaction.From.Id == user.Id &&
                     o.Transaction.Confirmation != null &&
                     o.Transaction.Confirmation.Outcome == TransactionConfirmationOutcome.Confirmed))
            {
                return false;
            }
        }

        return !await _uow.SellableReviews.AnyAsync(
            r => r.Item.Id == item.Id &&
                 r.LeftBy.Id == user.Id);
    }

    #endregion
}
