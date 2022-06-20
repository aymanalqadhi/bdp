namespace BDP.Web.Dtos;

public class SellableReviewDto
{
    /// <summary>
    /// Gets or sets the id of the review
    /// </summary>
    public Guid Id { get; set; }

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

    /// <summary>
    /// Gets or sets the date at which the review was left
    /// </summary>
    public DateTime CreatedAt { get; set; }
}