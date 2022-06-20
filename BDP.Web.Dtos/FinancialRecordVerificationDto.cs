using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A data-transfer object for <see cref="FinancialRecordVerification"/>
/// </summary>
public sealed class FinancialRecordVerificationDto : EntityDto<FinancialRecordVerification>
{
    /// <summary>
    /// Gets or sets whether the record has been accepted
    /// </summary>
    public bool IsAccepted { get; set; }

    /// <summary>
    /// Gets or sets the document of the record verification
    /// </summary>
    public Uri? Document { get; set; } = null!;

    /// <summary>
    /// Gets or sets additional notes on verifications
    /// </summary>
    public string? Notes { get; set; }
}