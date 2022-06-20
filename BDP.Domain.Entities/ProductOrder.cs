namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a product order
/// </summary>
public sealed class ProductOrder : Purchase<ProductOrder, Product>
{
    /// <summary>
    /// Gets or sets the ordered quantity
    /// </summary>
    public uint Quantity { get; set; }
}