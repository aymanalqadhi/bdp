namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a confirmation to a <see cref="Transaction"/> instance
/// </summary>
public sealed class TransactionConfirmation : AuditableEntity<TransactionConfirmation>
{
    /// <summary>
    /// Gets or set the outcome of the transaction confirmation
    /// </summary>
    public TransactionConfirmationOutcome Outcome { get; set; }

    /// <summary>
    /// Gets or sets the id of the transaction
    /// </summary>
    public Guid TransactionId { get; set; }

    /// <summary>
    /// Gets or sets the transaction this confirmation belongs to
    /// </summary>
    public Transaction Transaction { get; set; } = null!;
}

/// <summary>
/// An enum to represent the outcome of a transaction confirmation
/// </summary>
public enum TransactionConfirmationOutcome
{
    Declined,
    Confirmed,
}