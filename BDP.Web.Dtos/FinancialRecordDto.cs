using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A data-transfer object for <see cref="FinancialRecord"/>
/// </summary>
public class FinancialRecordDto : EntityDto<FinancialRecord>
{
    /// <summary>
    /// Gets or sets the amount associated with the financial record
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the verification of the record
    /// </summary>
    public FinancialRecordVerificationDto? Verification { get; set; }

    /// <summary>
    /// Gets or sets the user associated with the financial record
    /// </summary>
    public UserDto MadeBy { get; set; } = null!;

    /// <summary>
    /// An optinal note set by the user
    /// </summary>
    public string? Note { get; set; }
}