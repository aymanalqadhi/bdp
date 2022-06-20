namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a a base sellable item
/// </summary>
/// <typeparam name="TEntity">The </typeparam>
public abstract class Sellable<TEntity, TReview> : AuditableEntity<TEntity>
    where TEntity : class
    where TReview : SellableReview<TEntity>
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
    public ICollection<TReview> Reviews { get; set; } = new List<TReview>();
}