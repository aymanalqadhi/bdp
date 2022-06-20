namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a financial record requeted by a user
/// </summary>
public sealed class FinancialRecord : AuditableEntity<FinancialRecord>
{
    /// <summary>
    /// Gets or sets the amount associated with the financial record
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the user associated with the financial record
    /// </summary>
    public User MadeBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the note left by the user
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Gets or sets the verification of the record
    /// </summary>
    public FinancialRecordVerification? Verification { get; set; }
}