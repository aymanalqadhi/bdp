using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A data-transfer object for <see cref="Purchase{TEntity}"/>
/// </summary>
public abstract class PurchaseDto<TEntity> : EntityDto<TEntity>
    where TEntity : Purchase<TEntity>
{
    /// <summary>
    /// Gets or sets whether the purchase was accepted by the offering party
    /// </summary>
    public bool IsEarlyAccepted { get; set; } = false;

    /// <summary>
    /// Gets or sets the payment transaction of the purchase
    /// </summary>
    public TransactionDto Payment { get; set; } = null!;
}