namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a financial record verification
/// </summary>
public sealed class FinancialRecordVerification : AuditableEntity
{
    /// <summary>
    /// Gets or sets the outcome of the financial record verification
    /// </summary>
    public FinancialRecordVerificationOutcome Outcome { get; set; }

    /// <summary>
    /// Gets or sets the financial record id
    /// </summary>
    public long FinancialRecordId { get; set; }

    /// <summary>
    /// Gets or sets the financial record this verification belongs to
    /// </summary>
    public FinancialRecord FinancialRecord { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who made this verification
    /// </summary>
    public User VerifiedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the document of the record
    /// </summary>
    public Attachment? Document { get; set; } = null!;

    /// <summary>
    /// Gets or sets additional notes on verifications
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// An enum to represent the outcome of a financial record verification
/// </summary>
public enum FinancialRecordVerificationOutcome
{
    Rejected,
    Accepted,
}