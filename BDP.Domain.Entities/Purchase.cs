namespace BDP.Domain.Entities;

public class Purchase : AuditableEntity
{
    /// <summary>
    /// Gets or sets the transaction associated with this purchase
    /// </summary>
    public Transaction Transaction { get; set; } = null!;
}