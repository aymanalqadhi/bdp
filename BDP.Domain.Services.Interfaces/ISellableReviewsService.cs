using BDP.Domain.Entities;

namespace BDP.Domain.Services.Interfaces;

public interface ISellableReviewsService
{
    /// <summary>
    /// Asynchronosly gets reviews for a specific sellable, limited with pagination
    /// </summary>
    /// <param name="item">The item which to get reviews for</param>
    /// <param name="page">The page to get</param>
    /// <param name="pageSize">The pages size</param>
    /// <returns></returns>
    IAsyncEnumerable<SellableReview> GetForAsync(Sellable item, int page, int pageSize);

    /// <summary>
    /// Asynchronously gets the review for an item for a specific
    /// user if it exists
    /// </summary>
    /// <param name="item">The item to get the review for</param>
    /// <param name="user">the user to get the review for</param>
    /// <returns></returns>
    Task<SellableReview?> GetReviewForUser(Sellable item, User user);

    /// <summary>
    /// Asynchronously adds a review to a sellable item
    /// </summary>
    /// <param name="item">The item to add the review for</param>
    /// <param name="user">The user leaving the review</param>
    /// <param name="rating">The rating value (between 1 and 5)</param>
    /// <param name="comment">An optional comment string</param>
    /// <returns>The created review</returns>
    Task<SellableReview> ReviewAsync(Sellable item, User user, double rating, string? comment = null);

    /// <summary>
    /// Asynchronously Gets the review info for a specific item
    /// </summary>
    /// <param name="user">The user which to get the info for</param>
    /// <returns></returns>
    Task<SellableReviewInfo> ReviewInfoForAsync(Sellable item);

    /// <summary>
    /// Asynchronously gets whether a user can review the item or not
    /// </summary>
    /// <param name="item">The item to check for</param>
    /// <param name="user">The reviewing user</param>
    /// <returns></returns>
    Task<bool> CanReviewAsync(Sellable item, User user);
}

public record SellableReviewInfo(double AverageRating, int ReviewsCount);