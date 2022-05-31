using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class UpdateEventProgressRequess
{
    /// <summary>
    /// Gets or sets the image to be added
    /// </summary>
    [Required]
    [Range(0.0, 1.0)]
    public double Progress { get; set; }
}