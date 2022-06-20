using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A data-transfer object for <see cref="Transaction"/>
/// </summary>
public sealed class TransactionDto : EntityDto<Transaction>
{
    /// <summary>
    /// Gets or sets the amount set by the transaction
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the source of the transaction
    /// </summary>
    public UserDto From { get; set; } = null!;

    /// <summary>
    /// Gets or sets the destination of the transaction
    /// </summary>
    public UserDto To { get; set; } = null!;

    /// <summary>
    /// Gets or sets the confimation of the request
    /// </summary>
    public TransactionConfirmationDto? Confirmation { get; set; }
}