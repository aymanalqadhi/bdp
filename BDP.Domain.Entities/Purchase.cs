namespace BDP.Domain.Entities;

public class Purchase : AuditableEntity<Purchase>
{
    /// <summary>
    /// Gets or sets the transaction associated with this purchase
    /// </summary>
    public Transaction Transaction { get; set; } = null!;
}