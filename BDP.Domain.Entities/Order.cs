namespace BDP.Domain.Entities;

public sealed class Order : Purchase<Order, SellableVariant>
{
    /// <summary>
    /// Gets or sets the quantity of the order
    /// </summary>
    public uint Quantity { get; set; }
}
