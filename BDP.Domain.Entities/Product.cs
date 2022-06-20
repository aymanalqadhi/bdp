namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a product sold by a user
/// </summary>
public sealed class Product : Sellable<Product>
{
    /// <summary>
    /// Gets or sets the available quantity of the product
    /// </summary>
    public uint Quantity { get; set; }

    /// <summary>
    /// Gets or sets the collection of reviews
    /// </summary>
    public ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();
}