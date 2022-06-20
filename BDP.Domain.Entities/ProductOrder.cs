namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a product order
/// </summary>
public sealed class ProductOrder : Purchase
{
    /// <summary>
    /// Gets or sets the product of the order
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Gets or sets the ordered quantity
    /// </summary>
    public uint Quantity { get; set; }
}