using BDP.Domain.Entities;

namespace BDP.Domain.Services.Exceptions;

/// <summary>
/// An exception thrown when an order is already early-confimred by the provider
/// </summary>
public sealed class OrderAlreadyEarlyConfirmedException : PurchaseAlreadyEarlyConfirmedException<Order>
{
    /// <inheritdoc/>
    public OrderAlreadyEarlyConfirmedException(EntityKey<Order> purchaseId)
        : base(purchaseId, $"order #{purchaseId} already early-confirmed")
    {
    }
}