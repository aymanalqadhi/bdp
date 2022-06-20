namespace BDP.Domain.Entities;

public sealed class Product : AuditableEntity<Product>
{
    /// <summary>
    /// Gets or sets the title of the sellable
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the sellable
    /// </summary
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who listed this sellable
    /// </summary>
    public User OfferedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets whether the sellable is available
    /// </summary>
    public bool IsAvailable { get; set; } = true;

    /// <summary>
    /// Gets or sets the collection of reviews
    /// </summary>
    public ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();
}