using BDP.Domain.Entities;
using BDP.Web.Dtos.Attributes;
using Microsoft.AspNetCore.Http;

namespace BDP.Web.Dtos.Requests;

public class CreateProductVariantRequest
{
    /// <summary>
    /// Gets or sets the name of the variant
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the variant
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the price of the variant
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the type of the product variant
    /// </summary>
    public ProductVariantType Type { get; set; }

    /// <summary>
    /// Gets or sets the collection of images associated with the product variant
    /// </summary>
    [MaxFileSize(1024 * 1024 * 8)]
    [AllowedExtensions(".jpg", ".jpeg", ".png")]
    public IList<IFormFile>? Attachments { get; set; }
}