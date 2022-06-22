using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A dto for <see cref="Order"/>
/// </summary>
public sealed class OrderDto : PurchaseDto<Order>
{
    /// <summary>
    /// Gets or sets the quantity of the order
    /// </summary>
    public uint Quantity { get; set; }
}