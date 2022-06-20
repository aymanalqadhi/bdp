namespace BDP.Domain.Entities;

public abstract class Purchase<TEnitity, TSellable> : AuditableEntity<TEnitity>
    where TEnitity : class
    where TSellable : Sellable<TSellable>
{
    /// <summary>
    /// Gets or sets the transaction associated with this purchase
    /// </summary>
    public Transaction Transaction { get; set; } = null!;

    /// <summary>
    /// Gets or sets the purchased item
    /// </summary>
    public TSellable Item { get; set; } = null!;
}