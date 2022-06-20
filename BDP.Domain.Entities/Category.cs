namespace BDP.Domain.Entities;

/// <summary>
/// An class that represents a products cateogry
/// </summary>
public sealed class Category : AuditableEntity<Category>
{
    /// <summary>
    /// Gets or sets the name of the category
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the parent (if exists) of the category
    /// </summary>
    public Category? Parent { get; set; }

    /// <summary>
    /// Gets or sets the collection of subcategories of this category
    /// </summary>
    public ICollection<Category> Children { get; set; } = new List<Category>();

    /// <summary>
    /// Gets or sets the collection of products in this category
    /// </summary>
    public ICollection<Product> Products { get; set; } = new List<Product>();
}