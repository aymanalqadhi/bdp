using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;

namespace BDP.Application.App;

public class SellableReviewsService : ISellableReviewsService
{
    #region Private fields

    private readonly IUnitOfWork _uow;

    #endregion Private fields

    #region Ctors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the app</param>
    public SellableReviewsService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion Ctors

    #region Public methods

    /// <inheritdoc/>
    public IQueryBuilder<SellableReview> GetForAsync(EntityKey<Sellable> itemId)
        => _uow.SellableReviews.Query().Where(r => r.Item.Id == itemId);

    /// <inheritdoc/>
    public Task<SellableReview?> GetReviewForUserAsync(EntityKey<User> userId, EntityKey<Sellable> itemId)
    {
        return _uow.SellableReviews
            .Query()
            .FirstOrDefaultAsync(r => r.Item.Id == itemId && r.LeftBy.Id == userId);
    }

    /// <inheritdoc/>
    public async Task<SellableReview> ReviewAsync(
        EntityKey<User> userId, EntityKey<Sellable> itemId, double rating, string? comment = null)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        if (rating < 0 || rating > 5)
            throw new InvalidRatingValueException(rating);

        if (!await CanReviewAsync(userId, itemId))
            throw new ItemAlreadyReviewedException($"item #{itemId} cannot be reviewed by user #{userId}");

        var user = await _uow.Users.Query().FindAsync(userId);
        var item = await _uow.Sellables.Query().FindAsync(itemId);

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
    public async Task<SellableReviewInfo> ReviewInfoForAsync(EntityKey<Sellable> itemId)
    {
        var reviews = _uow.SellableReviews
            .Query()
            .Where(r => r.Item.Id == itemId)
            .AsAsyncEnumerable();

        var reviewsCount = await reviews.CountAsync();
        var ratingAvg = reviewsCount > 0 ? await reviews.AverageAsync(r => r.Rating) : 0;

        return new SellableReviewInfo(ratingAvg, reviewsCount);
    }

    /// <inheritdoc/>
    public async Task<bool> CanReviewAsync(EntityKey<User> userId, EntityKey<Sellable> itemId)
    {
        var item = await _uow.Sellables.Query().FindAsync(itemId);

        if (item is Product)
        {
            if (!await _uow.ProductOrders.Query().AnyAsync(
                o => o.Product.Id == itemId &&
                     o.Transaction.From.Id == userId &&
                     o.Transaction.Confirmation != null &&
                     o.Transaction.Confirmation.Outcome == TransactionConfirmationOutcome.Confirmed))
            {
                return false;
            }
        }
        else if (item is Service)
        {
            if (!await _uow.ServiceReservations.Query().AnyAsync(
                o => o.Service.Id == itemId &&
                     o.Transaction.From.Id == userId &&
                     o.Transaction.Confirmation != null &&
                     o.Transaction.Confirmation.Outcome == TransactionConfirmationOutcome.Confirmed))
            {
                return false;
            }
        }

        return !await _uow.SellableReviews.Query().AnyAsync(
            r => r.Item.Id == itemId && r.LeftBy.Id == userId);
    }

    #endregion Public methods
}