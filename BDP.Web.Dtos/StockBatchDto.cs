using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A data-transfer object for <see cref="StockBatch"/> with sellable type
/// </summary>
public sealed class StockBatchDto : EntityDto<StockBatch>
{
    /// <summary>
    /// Gets or sets the quantity of the stock batch
    /// </summary>
    public uint Quantity { get; set; }
}