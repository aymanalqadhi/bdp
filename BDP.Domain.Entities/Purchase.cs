namespace BDP.Domain.Entities;

public abstract class Purchase<TEnitity> : AuditableEntity<TEnitity>
    where TEnitity : class
{
    /// <summary>
    /// Gets or sets the transaction associated with this purchase
    /// </summary>
    public Transaction Transaction { get; set; } = null!;
}