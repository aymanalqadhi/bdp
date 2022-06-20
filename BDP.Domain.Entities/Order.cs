namespace BDP.Domain.Entities;

public sealed class Order : Purchase<Order>
{
    /// <summary>
    /// Gets or sets the quantity of the order
    /// </summary>
    public uint Quantity { get; set; }
}