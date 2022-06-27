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
    /// Gets a reservable proudct variant reservation windows
    /// </summary>
    /// <param name="variantId">The id of the product variant to get reservation windows for</param>
    /// <returns>A query builder for the product variant reservation windows</returns>
    IQueryBuilder<ReservationWindow> GetReservationWindows(EntityKey<ProductVariant> variantId);

    /// <summary>
    /// Asynchrnously adds a reservation window for a reservable variant
    /// </summary>
    /// <param name="userId">The id of the user owning the product variant</param>
    /// <param name="variantId">The id of the variant</param>
    /// <param name="weekdays">The weekdays of reservation window</param>
    /// <param name="start">The start of the reservation </param>
    /// <param name="end"></param>
    /// <returns>The created reservation window</returns>
    /// <exception cref="InvalidProductVaraintTypeException"></exception>
    Task<ReservationWindow> AddAsync(
        EntityKey<User> userId,
        EntityKey<ProductVariant> variantId,
        Weekday weekdays,
        TimeOnly start,
        TimeOnly end);

    /// <summary>
    /// Asynchrnously removes a reservable product variant reservation window
    /// </summary>
    /// <param name="userId">The id of the user owning the product variant</param>
    /// <param name="windowId"></param>
    /// <returns></returns>
    Task RemoveAsync(EntityKey<User> userId, EntityKey<ReservationWindow> windowId);

    #endregion Public Methods
}