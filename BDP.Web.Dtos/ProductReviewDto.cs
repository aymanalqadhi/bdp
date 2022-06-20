using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A data-transfer object for <see cref="ProductReview"/>
/// </summary>
public sealed class ProductReviewDto : MutableEntityDto<ProductReview>
{
    /// <summary>
    /// Gets or sets teh rating value
    /// </summary>
    public double Rating { get; set; }

    /// <summary>
    /// Gets or sets the comment left by the reviewer
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the user who left the review
    /// </summary>
    public UserDto LeftBy { get; set; } = null!;
}