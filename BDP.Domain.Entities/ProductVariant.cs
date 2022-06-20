namespace BDP.Domain.Entities;

public abstract class ProductVariant<TEntity> : AuditableEntity<TEntity>
    where TEntity : class
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
    /// Gets the parent product of the variant
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Gets or sets the list of attachments of the variant
    /// </summary>
    public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
}