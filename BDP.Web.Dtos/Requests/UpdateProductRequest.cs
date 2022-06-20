using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class UpdateProductRequest
{
    /// <summary>
    /// Gets or sets the title of the product
    /// </summary>
    [Required]
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the product
    /// </summary>
    [Required]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the price of the product
    /// </summary>
    [Required]
    public decimal Price { get; set; }
}
