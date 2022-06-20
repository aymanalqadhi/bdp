namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a product sold by a user
/// </summary>
public sealed class Product : Sellable<Product, ProductReview>
{
    /// <summary>
    /// Gets or sets the available quantity of the product
    /// </summary>
    public uint Quantity { get; set; }
}