﻿namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a financial record verification
/// </summary>
public sealed class FinancialRecordVerification : AuditableEntity<FinancialRecordVerification>
{
    /// <summary>
    /// Gets or sets whether the financial record was approved or not
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    /// Gets or sets the financial record id
    /// </summary>
    public EntityKey<FinancialRecord> FinancialRecordId { get; set; } = null!;

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
    /// Gets or sets an additional note
    /// </summary>
    public string? Note { get; set; }
}