using BDP.Domain.Entities;
using BDP.Domain.Repositories;

using System.Linq.Expressions;

namespace BDP.Domain.Services;

/// <summary>
/// A service to manage purchases (orders and reservations) on the application
/// </summary>
public interface IPurchasesService
{
    #region Public Methods

    /// <summary>
    /// Asynchronously gets the available quantity for a sellable product variant
    /// </summary>
    /// <param name="variantId">The id of the sellable product variant</param>
    /// <returns>The avaiable quantity</returns>
    Task<uint> AvailableSellableVariantQuantityAsync(EntityKey<ProductVariant> variantId);

    /// <summary>
    /// Asynchronously checks if a product variant can be purchased or not
    /// </summary>
    /// <param name="variantId">The id of the product to check for</param>
    /// <returns>True if the product variant is available for purchase, false otherwise</returns>
    Task<bool> CanPurchaseAsync(EntityKey<ProductVariant> variantId);

    /// <summary>
    /// Asynchronously makes an order for a sellable product variant
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="variantId"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    Task<Order> OrderAsync(
        EntityKey<User> userId,
        EntityKey<ProductVariant> variantId,
        uint quantity);

    /// <summary>
    /// Asynchronously makes a reservation for a reservable product variant
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="variantId"></param>
    /// <returns></returns>
    Task<Order> ReserveAsync(
        EntityKey<User> userId,
        EntityKey<ProductVariant> variantId);

    #endregion Public Methods
}