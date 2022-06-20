using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class SellableReviewRequest
{
    /// <summary>
    /// Gets or sets the rating value
    /// </summary>
    [Required]
    [Range(1, 5, ErrorMessage = "rating must be in the range 1-5")]
    public double Rating { get; set; }

    /// <summary>
    /// Gets or sets the comment of the review
    /// </summary>
    public string? Comment { get; set; }
}
