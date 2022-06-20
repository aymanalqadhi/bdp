namespace BDP.Domain.Entities;

/// <summary>
/// A class to rerperesnt sellable product stock batch entity
/// </summary>
public sealed class StockBatch : AuditableEntity<StockBatch>
{
    /// <summary>
    /// Gets or sets the quantity of the stock batch
    /// </summary>
    public uint Quantity { get; set; } = 0;

    /// <summary>
    /// Gets or sets the sellable variant to add the batch to
    /// </summary>
    public ProductVariant Variant { get; set; } = null!;
}