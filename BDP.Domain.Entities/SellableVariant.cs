namespace BDP.Domain.Entities;

public sealed class SellableVariant : ProductVariant<SellableVariant>
{
    /// <summary>
    /// Gets or sets the available quantity of the variant
    /// </summary>
    public uint AvailableQuantity { get; set; } = 0;
}
