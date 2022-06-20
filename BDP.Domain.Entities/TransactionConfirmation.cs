namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a confirmation to a <see cref="Transaction"/> instance
/// </summary>
public sealed class TransactionConfirmation : AuditableEntity<TransactionConfirmation>
{
    /// <summary>
    /// Gets or sets whether the transaction was accepted or not
    /// </summary>
    public bool IsAccepted { get; set; }

    /// <summary>
    /// Gets or sets the id of the transaction
    /// </summary>
    public EntityKey<Transaction> TransactionId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the transaction this confirmation belongs to
    /// </summary>
    public Transaction Transaction { get; set; } = null!;
}