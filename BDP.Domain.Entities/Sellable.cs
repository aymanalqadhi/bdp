namespace BDP.Domain.Entities;

/// <summary>
/// A recrod to represent keys for the <see cref="Sellable"/> entity
/// </summary>
/// <param name="Id">The id field of the key</param>
public sealed record SellableKey(Guid Id) : EntityKey<Sellable>(Id);

/// <summary>
/// A class to represent a sellable item
/// </summary>
/// <typeparam name="TKey">The key type of the sellable item</typeparam>
public class Sellable : AuditableEntity
{
    /// <summary>
    /// Gets or sets the id of the sellable item
    /// </summary>
    public SellableKey Id { get; set; } = new SellableKey(Guid.NewGuid());

    /// <summary>
    /// Gets or sets the title of the sellable
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the sellable
    /// </summary
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the price of the sellable
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the user who listed this sellable
    /// </summary>
    public User OfferedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets whether the sellable is available
    /// </summary>
    public bool IsAvailable { get; set; } = true;

    /// <summary>
    /// Gets or sets the list of attachments attached to the sellable
    /// </summary>
    public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    /// <summary>
    /// Gets or sets the list of reviews left for this sellable item
    /// </summary>
    public ICollection<SellableReview> Reviews { get; set; } = new List<SellableReview>();
}