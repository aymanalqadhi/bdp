namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a transaction made between two users
/// </summary>
public sealed class Transaction : AuditableEntity
{
    /// <summary>
    /// Gets or sets the amount associated with the transaction
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the user who made the transaction
    /// </summary>
    public User From { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who recieves the transaction
    /// </summary>
    public User To { get; set; } = null!;

    /// <summary>
    /// Gets or sets the confirmation token of the transaction
    /// </summary>
    public string ConfirmationToken { get; set; } = null!;

    /// <summary>
    /// Gets or set the confirmation of the transaction
    /// </summary>
    public TransactionConfirmation? Confirmation { get; set; }

    /// <summary>
    /// Sets the confirmation token to null if the passed condition is true
    /// </summary>
    /// <param name="cond">The condition to check against</param>
    /// <returns>
    /// this with ConfirmationToken set to null if the condition it true, unchanged otherwise
    /// </returns>
    public Transaction ConcealConfirmationTokenIf(bool cond)
    {
        if (cond)
            ConfirmationToken = null!;

        return this;
    }
}