namespace BDP.Domain.Entities;

/// <summary>
/// A recrod to represent keys for the <see cref="FinancialRecord"/> entity
/// </summary>
/// <param name="Id">The id field of the key</param>
public sealed record FinancialRecordKey(Guid Id) : EntityKey<FinancialRecord>(Id);

/// <summary>
/// A class to represent a financial record requeted by a user
/// </summary>
public sealed class FinancialRecord : AuditableEntity
{
    /// <summary>
    /// Gets or sets the id of the financial record
    /// </summary>
    public FinancialRecordKey Id { get; set; } = new FinancialRecordKey(Guid.NewGuid());

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