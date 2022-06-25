using BDP.Domain.Entities;

namespace BDP.Domain.Services.Exceptions;

/// <summary>
/// An exception thrown when an order is already early-confimred by the provider
/// </summary>
public sealed class ReservationAlreadyEarlyConfirmedException : PurchaseAlreadyEarlyConfirmedException<Reservation>
{
    /// <inheritdoc/>
    public ReservationAlreadyEarlyConfirmedException(EntityKey<Reservation> purchaseId)
        : base(purchaseId, $"order #{purchaseId} already early-confirmed")
    {
    }
}