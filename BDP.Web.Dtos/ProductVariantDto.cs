using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A data-transfer object for <see cref="ProductVariant"/>
/// </summary>

public abstract class ProductVariantDto : MutableEntityDto<ProductVariant>
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
    /// Gets or sets the list of attachments of the variant
    /// </summary>
    public IEnumerable<Uri> Attachments { get; set; } = new List<Uri>();
}