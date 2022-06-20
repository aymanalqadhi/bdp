using BDP.Domain.Entities;
using BDP.Domain.Repositories;

namespace BDP.Domain.Services;

public interface IProductReviewsService
{
    /// <summary>
    /// Gets reviews for a specific product
    /// </summary>
    /// <param name="productId">The id of the product which to get reviews for</param>
    /// <returns>A query builder for reviews for the specified product</returns>
    IQueryBuilder<ProductReview> GetFor(EntityKey<Product> productId);

    /// <summary>
    /// Gets the review for an product made by a specific user
    /// </summary>
    /// <param name="userId">the id of the user to get the review for</param>
    /// <param name="productId">The id of the product to get the review for</param>
    /// <returns></returns>
    IQueryBuilder<ProductReview> GetReviewForUser(EntityKey<User> userId, EntityKey<Product> productId);

    /// <summary>
    /// Asynchronously adds a review to a sellable item
    /// </summary>
    /// <param name="userId">The id of the user leaving the review</param>
    /// <param name="productId">The id of the product to add the review for</param>
    /// <param name="rating">The rating value (between 1 and 5)</param>
    /// <param name="comment">An optional comment string</param>
    /// <returns>The created review</returns>
    Task<ProductReview> ReviewAsync(
        EntityKey<User> userId,
        EntityKey<Product> productId,
        double rating,
        string? comment = null);

    /// <summary>
    /// Asynchronously Gets the review summary for a specific product
    /// </summary>
    /// <param name="productId">The id of the product which to get the info for</param>
    /// <returns>Product review summary</returns>
    Task<ProductReviewSummary> SummaryForAsync(EntityKey<Product> productId);

    /// <summary>
    /// Asynchronously gets whether a user can review the item or not
    /// </summary>
    /// <param name="userId">The id of the reviewing user</param>
    /// <param name="productId">The id of the product to check for</param>
    /// <returns></returns>
    Task<bool> CanReviewAsync(EntityKey<User> userId, EntityKey<Product> productId);
}

/// <summary>
/// A record to hold reviews summary for a product
/// </summary>
/// <param name="AverageRating"></param>
/// <param name="ReviewsCount"></param>
public record ProductReviewSummary(double AverageRating, int ReviewsCount);