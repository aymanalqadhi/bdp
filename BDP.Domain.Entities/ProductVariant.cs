namespace BDP.Domain.Entities;

public sealed class ProductVariant : AuditableEntity<ProductVariant>
{
    /// <summary>
    /// Gets or sets the name of the variant
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the variant
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the price of the variant
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the type of the product variant
    /// </summary>
    public ProductVariantType Type { get; set; }

    /// <summary>
    /// Gets the parent product of the variant
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Gets or sets the list of attachments of the variant
    /// </summary>
    public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
}

/// <summary>
/// An enum to represent product variant types
/// </summary>
public enum ProductVariantType
{
    Sellable,
    Reservable,
}