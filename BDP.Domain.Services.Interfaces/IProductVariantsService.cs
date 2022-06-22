using BDP.Domain.Entities;
using BDP.Domain.Repositories;

namespace BDP.Domain.Services;

/// <summary>
/// An interface to be implemented by product variants services
/// </summary>
public interface IProductVariantsService
{
    #region Public Methods

    /// <summary>
    /// Asynchronously adds a reservable product variant
    /// </summary>
    /// <param name="productId">The product to add the variant to</param>
    /// <param name="name">The name of the variant</param>
    /// <param name="description">The description of the variant</param>
    /// <param name="price">The price of the variant</param>
    /// <param name="attachments">The attachments of the variant</param>
    /// <returns>The created variant object</returns>
    /// <exception cref="InvalidPriceException"></exception>
    Task<ProductVariant> AddReservableAsync(
        EntityKey<Product> productId,
        string name,
        string? description,
        decimal price,
        IEnumerable<IUploadFile>? attachments = null
     );

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
    /// Asynchronously adds a sellable product variant
    /// </summary>
    /// <param name="productId">The product to add the variant to</param>
    /// <param name="name">The name of the variant</param>
    /// <param name="description">The description of the variant</param>
    /// <param name="price">The price of the variant</param>
    /// <param name="attachments">The attachments of the variant</param>
    /// <returns>The created variant object</returns>
    /// <exception cref="InvalidPriceException"></exception>
    Task<ProductVariant> AddSellableAsync(
        EntityKey<Product> productId,
        string name,
        string? description,
        decimal price,
        IEnumerable<IUploadFile>? attachments = null
     );

    /// <summary>
    /// Asynchronously adds a stock batch to a sellable product variant
    /// </summary>
    /// <param name="variantId">The id of the variant to add the stock batch for</param>
    /// <param name="quantity">The quantity of the stock batch</param>
    /// <returns>The created stock batch</returns>
    /// <exception cref="InvalidProductVaraintTypeException"></exception>
    Task<StockBatch> AddStockBatchAsync(EntityKey<ProductVariant> variantId, uint quantity);

    /// <summary>
    /// Gets variants for a specific product
    /// </summary>
    /// <param name="productId">The id of the product to get its variants</param>
    /// <returns>A query builder for product variants</returns>
    IQueryBuilder<ProductVariant> GetVariants(EntityKey<Product> productId);

    /// <summary>
    /// Asynchrnously removes a reservable product variant reservation window
    /// </summary>
    /// <param name="windowId"></param>
    /// <returns></returns>
    Task RemoveReservationWindowAsync(EntityKey<ReservationWindow> windowId);

    /// <summary>
    /// Asynchronously removes a sellable product variant stock batch
    /// </summary>
    /// <param name="batchId">The id of the batch to remove</param>
    /// <returns></returns>
    /// <exception cref="NotEnoughStockException"></exception>
    Task RemoveStockBatchAsync(EntityKey<StockBatch> batchId);

    /// <summary>
    /// Asynchronously removes a product variant
    /// </summary>
    /// <param name="variantid">The id of the product variant to be removed</param>
    /// <returns></returns>
    Task RemoveVariantAsync(EntityKey<ProductVariant> variantid);

    /// <summary>
    /// Gets resrvation windows for a reservable product variant
    /// </summary>
    /// <param name="variantId">
    /// The id of the product variant which to get the reservation windows for
    /// </param>
    /// <returns>A query builder for the reservation windows </returns>
    IQueryBuilder<ReservationWindow> ReservationWindowsFor(EntityKey<ProductVariant> variantId);

    /// <summary>
    /// Asynchronously calculates the total available quantity of a sellable product
    /// variant. This method will calculate the quantity with regard to available stock batches
    /// </summary>
    /// <param name="variantId">
    /// The id of the product variant to calculate the total quantity for
    /// </param>
    /// <returns>The total available quantity</returns>
    Task<long> TotalAvailableQuantityAsync(EntityKey<ProductVariant> variantId);

    #endregion Public Methods
}