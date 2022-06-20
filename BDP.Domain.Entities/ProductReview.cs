namespace BDP.Domain.Entities;

public sealed class ProductReview : AuditableEntity<ProductReview>
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
    public User LeftBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the reviews product
    /// </summary>
    public Product Product { get; set; } = null!;
}