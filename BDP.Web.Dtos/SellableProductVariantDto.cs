using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A data-transfer object for <see cref="ProductVariant"/> with sellable type
/// </summary>

public sealed class SellableProductVariantDto : ProductVariantDto
{
    /// <summary>
    /// Gets or sets the available quantity of the variant
    /// </summary>
    public uint AvailableQuantity { get; set; }
}