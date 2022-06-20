using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A data-transfer object for <see cref="Transaction"/>
/// </summary>
public sealed class TransactionConfirmationDto : EntityDto<TransactionConfirmation>
{
    /// <summary>
    /// Gets or sets whether the transaction has been accepted or not
    /// </summary>
    public bool IsAccepted { get; set; }
}