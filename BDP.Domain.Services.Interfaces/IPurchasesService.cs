using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services.Exceptions;

namespace BDP.Domain.Services;

/// <summary>
/// A service to manage purchases (orders and reservations) on the application
/// </summary>
public interface IPurchasesService
{
    #region Public Methods

    /// <summary>
    /// Asynchronously makes an order for a sellable product variant
    /// </summary>
    /// <param name="userId">The id of the user to make the order for</param>
    /// <param name="variantId">The product variant to order</param>
    /// <param name="quantity">The quantity to be ordered</param>
    /// <returns>The created order</returns>
    /// <exception cref="InvalidProductVaraintTypeException"></exception>
    /// <exception cref="NotEnoughStockException"></exception>
    /// <exception cref="InsufficientBalanceException"></exception>
    Task<Order> OrderAsync(
        EntityKey<User> userId,
        EntityKey<ProductVariant> variantId,
        uint quantity);

    /// <summary>
    /// Asynchronously makes a reservation for a reservable product variant
    /// </summary>
    /// <param name="userId">The id of the user to make the reservation for</param>
    /// <param name="variantId">The product variant to reserve</param>
    /// <returns>The created reservation</returns>
    /// <exception cref="InvalidProductVaraintTypeException"></exception>
    /// <exception cref="InsufficientBalanceException"></exception>
    Task<Reservation> ReserveAsync(
        EntityKey<User> userId,
        EntityKey<ProductVariant> variantId);

    /// <summary>
    /// Asynchronously checks whether a product has pending purchases or not
    /// </summary>
    /// <param name="productId">The id of the product to check for</param>
    /// <returns>True if the product has pending purchases, false otherwise</returns>
    Task<bool> HasPendingPurchasesAsync(EntityKey<Product> productId);

    /// <summary>
    /// Gets all orders for all sellable variants of a specific product
    /// </summary>
    /// <param name="productId">The id of the product to get pending orders for</param>
    /// <param name="pending">If true, only pending orders will be fetched</param>
    /// <returns></returns>
    IQueryBuilder<Order> GetOrdersFor(EntityKey<Product> productId, bool pending = false);

    /// <summary>
    /// Gets all reservations for all reservable variants of a specific product
    /// </summary>
    /// <param name="productId">The id of the product to get pending reservations for</param>
    /// <param name="pending">If true, only pending reservations will be fetched</param>
    /// <returns></returns>
    IQueryBuilder<Reservation> GetReservationsFor(EntityKey<Product> productId, bool pending = false);

    /// <summary>
    /// Gets orders for a specific user
    /// </summary>
    /// <param name="userId">The id of the user to get the orders for</param>
    /// <param name="pending">If true, only pending orders will be fetched</param>
    /// <returns>A query builder for user orders</returns>
    IQueryBuilder<Order> GetOrdersFor(EntityKey<User> userId, bool pending = false);

    /// <summary>
    /// Gets resrvations for a specific user
    /// </summary>
    /// <param name="userId">The id of the user to get the reservations for</param>
    /// <param name="pending">If true, only pending reservations will be fetched</param>
    /// <returns>A query builder for user reservations</returns>
    IQueryBuilder<Reservation> GetReservationsFor(EntityKey<User> userId, bool pending = false);

    #endregion Public Methods
}