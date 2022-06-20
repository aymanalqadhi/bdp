using BDP.Domain.Entities;
using BDP.Domain.Repositories;

namespace BDP.Domain.Services;

public interface ISellableReviewsService
{
    /// <summary>
    /// Asynchronosly gets reviews for a specific sellable, limited with pagination
    /// </summary>
    /// <param name="sellableId">The id of the item which to get reviews for</param>
    /// <returns></returns>
    IQueryBuilder<SellableReview> GetForAsync(Guid sellableId);

    /// <summary>
    /// Asynchronously gets the review for an item for a specific
    /// user if it exists
    /// </summary>
    /// <param name="userId">the id of the user to get the review for</param>
    /// <param name="itemId">The id of the item to get the review for</param>
    /// <returns></returns>
    Task<SellableReview?> GetReviewForUserAsync(Guid userId, Guid itemId);

    /// <summary>
    /// Asynchronously adds a review to a sellable item
    /// </summary>
    /// <param name="userId">The id of the user leaving the review</param>
    /// <param name="itemId">The id of the item to add the review for</param>
    /// <param name="rating">The rating value (between 1 and 5)</param>
    /// <param name="comment">An optional comment string</param>
    /// <returns>The created review</returns>
    Task<SellableReview> ReviewAsync(Guid userId, Guid itemId, double rating, string? comment = null);

    /// <summary>
    /// Asynchronously Gets the review info for a specific item
    /// </summary>
    /// <param name="itemId">The id of the item which to get the info for</param>
    /// <returns></returns>
    Task<SellableReviewInfo> ReviewInfoForAsync(Guid itemId);

    /// <summary>
    /// Asynchronously gets whether a user can review the item or not
    /// </summary>
    /// <param name="userId">The id of the reviewing user</param>
    /// <param name="itemId">The id of the item to check for</param>
    /// <returns></returns>
    Task<bool> CanReviewAsync(Guid userId, Guid itemId);
}

public record SellableReviewInfo(double AverageRating, int ReviewsCount);