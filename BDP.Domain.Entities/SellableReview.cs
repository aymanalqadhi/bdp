namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a sellable review
/// </summary>
public class SellableReview : AuditableEntity<SellableReview>
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
    /// Gets or sets the rated item
    /// </summary>
    public Sellable Item { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who left the review
    /// </summary>
    public User LeftBy { get; set; } = null!;
}