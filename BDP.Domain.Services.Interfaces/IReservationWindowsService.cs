using BDP.Domain.Entities;
using BDP.Domain.Repositories;

namespace BDP.Domain.Services;

/// <summary>
/// An interface to be implemented by reservable product variants reservation
/// windows services
/// </summary>
public interface IReservationWindowsService
{
    #region Public Methods

    /// <summary>
    /// Asynchrnously adds a reservation window for a reservable variant
    /// </summary>
    /// <param name="variantId">The id of the variant</param>
    /// <param name="weekdays">The weekdays of reservation window</param>
    /// <param name="start">The start of the reservation </param>
    /// <param name="end"></param>
    /// <returns>The created reservation window</returns>
    /// <exception cref="InvalidProductVaraintTypeException"></exception>
    Task<ReservationWindow> AddReservationWindowAsync(
        EntityKey<ProductVariant> variantId,
        Weekday weekdays,
        TimeOnly start,
        TimeOnly end);

    /// <summary>
    /// Asynchrnously removes a reservable product variant reservation window
    /// </summary>
    /// <param name="windowId"></param>
    /// <returns></returns>
    Task RemoveReservationWindowAsync(EntityKey<ReservationWindow> windowId);

    /// <summary>
    /// Gets resrvation windows for a reservable product variant
    /// </summary>
    /// <param name="variantId">
    /// The id of the product variant which to get the reservation windows for
    /// </param>
    /// <returns>A query builder for the reservation windows </returns>
    IQueryBuilder<ReservationWindow> ReservationWindowsFor(EntityKey<ProductVariant> variantId);

    #endregion Public Methods
}