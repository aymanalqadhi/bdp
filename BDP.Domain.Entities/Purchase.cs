namespace BDP.Domain.Entities;

public abstract class Purchase<TEntity> : AuditableEntity<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Gets or sets whether the purchase was accepted by the offering party
    /// </summary>
    public bool IsEarlyAccepted { get; set; } = false;

    /// <summary>
    /// Gets or sets the payment transaction of the purchase
    /// </summary>
    public Transaction Payment { get; set; } = null!;

    /// <summary>
    /// Gets or sets the purchased variant
    /// </summary>
    public ProductVariant Variant { get; set; } = null!;
}