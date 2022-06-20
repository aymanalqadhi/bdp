namespace BDP.Domain.Entities;

/// <summary>
/// A recrod to represent keys for the <see cref="Purchase"/> entity
/// </summary>
/// <param name="Id">The id field of the key</param>
public sealed record PurchaseKey(Guid Id) : EntityKey<Purchase>(Id);

public class Purchase : AuditableEntity
{
    /// <summary>
    /// Gets or sets the id of the purchase
    /// </summary>
    public PurchaseKey Id { get; set; } = new PurchaseKey(Guid.NewGuid());

    /// <summary>
    /// Gets or sets the transaction associated with this purchase
    /// </summary>
    public Transaction Transaction { get; set; } = null!;
}