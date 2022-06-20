using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;

namespace BDP.Application.App;

public class ProductReviewsService : IProductReviewsService
{
    #region Private fields

    private readonly IUnitOfWork _uow;

    #endregion Private fields

    #region Ctors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the app</param>
    public ProductReviewsService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion Ctors

    #region Public methods

    /// <inheritdoc/>
    public IQueryBuilder<ProductReview> GetFor(EntityKey<Product> productId)
        => _uow.ProductReviews.Query().Where(r => r.Product.Id == productId);

    /// <inheritdoc/>
    public IQueryBuilder<ProductReview> GetReviewForUser(EntityKey<User> userId, EntityKey<Product> productId)
    {
        return _uow.ProductReviews
            .Query()
            .Where(r => r.Product.Id == productId && r.LeftBy.Id == userId);
    }

    /// <inheritdoc/>
    public async Task<ProductReview> ReviewAsync(
        EntityKey<User> userId,
        EntityKey<Product> productId,
        double rating,
        string? comment = null)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        if (!await CanReviewAsync(userId, productId))
            throw new ItemAlreadyReviewedException($"product #{productId} cannot be reviewed by user #{userId}");

        var user = await _uow.Users.Query().FindAsync(userId);
        var product = await _uow.Products.Query().FindAsync(productId);

        var review = new ProductReview
        {
            Rating = rating,
            Comment = comment,
            LeftBy = user,
            Product = product
        };

        _uow.ProductReviews.Add(review);
        await _uow.CommitAsync(tx);

        return review;
    }

    /// <inheritdoc/>
    public async Task<ProductReviewSummary> SummaryForAsync(EntityKey<Product> productId)
    {
        var reviews = _uow.ProductReviews.Query()
            .Where(r => r.Product.Id == productId)
            .AsAsyncEnumerable();

        var reviewsCount = await reviews.CountAsync();
        var ratingAvg = reviewsCount > 0 ? await reviews.AverageAsync(r => r.Rating) : 0;

        return new ProductReviewSummary(ratingAvg, reviewsCount);
    }

    /// <inheritdoc/>
    public async Task<bool> CanReviewAsync(EntityKey<User> userId, EntityKey<Product> productId)
    {
        var product = await _uow.Products.Query().FindAsync(productId);

        var tx = await _uow.Orders.Query()
            .Where(o => o.Payment.From.Id == userId)
            .Select(o => o.Payment)
            .FirstOrDefaultAsync();

        if (tx is null)
        {
            tx = await _uow.Reservations.Query()
               .Where(o => o.Payment.From.Id == userId)
               .Select(o => o.Payment)
               .FirstOrDefaultAsync();
        }

        return (tx?.Confirmation?.IsAccepted ?? false) &&
               !await _uow.ProductReviews.Query().AnyAsync(r =>
                   r.Product.Id == productId && r.LeftBy.Id == userId
                );
    }

    #endregion Public methods
}