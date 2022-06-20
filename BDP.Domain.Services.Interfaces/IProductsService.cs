using BDP.Domain.Entities;
using BDP.Domain.Repositories;

namespace BDP.Domain.Services;

/// <summary>
/// A service to manage products
/// </summary>
public interface IProductsService
{
    #region Public Methods

    /// <summary>
    /// Asynchronously adds a product
    /// </summary>
    /// <param name="userId">The id of the user to add the product for</param>
    /// <param name="title">The title of the product</param>
    /// <param name="description">The description of the product</param>
    /// <param name="categoryIds">An array of ids of categories to add the product for</param>
    /// <returns>The created product</returns>l
    Task<Product> AddAsync(
        EntityKey<User> userId,
        string title,
        string description,
        IEnumerable<Category> categoryIds);

    /// <summary>
    /// Asynchronously adds a reservable product variant
    /// </summary>
    /// <param name="productId">The product to add the variant to</param>
    /// <param name="name">The name of the variant</param>
    /// <param name="description">The description of the variant</param>
    /// <param name="price">The price of the variant</param>
    /// <param name="attachments">The attachments of the variant</param>
    /// <returns>The created variant object</returns>
    Task<ProductVariant> AddReservableVariantAsync(
        EntityKey<Product> productId,
        string name,
        string? description,
        decimal price,
        IEnumerable<IUploadFile>? attachments = null
     );

    /// <summary>
    /// Asynchronously adds a sellable product variant
    /// </summary>
    /// <param name="productId">The product to add the variant to</param>
    /// <param name="name">The name of the variant</param>
    /// <param name="description">The description of the variant</param>
    /// <param name="price">The price of the variant</param>
    /// <param name="attachments">The attachments of the variant</param>
    /// <returns>The created variant object</returns>
    Task<ProductVariant> AddSellableVariantAsync(
        EntityKey<Product> productId,
        string name,
        string? description,
        decimal price,
        IEnumerable<IUploadFile>? attachments = null
     );

    /// <summary>
    /// Gets products by category
    /// </summary>
    /// <param name="categoryId">The id of the category to get products in</param>
    /// <returns>A query builder for products in the specified category</returns>
    IQueryBuilder<Product> GetByCategory(EntityKey<Category> categoryId);

    /// <summary>
    /// Gets products for a user, limited with pagination
    /// </summary>
    /// <param name="userId">The id of the user which to get the products for</param>
    /// <returns></returns>
    IQueryBuilder<Product> GetFor(EntityKey<User> userId);

    /// <summary>
    /// Gets products
    /// </summary>
    /// <returns>A query builder for products</returns>
    IQueryBuilder<Product> GetProducts();

    /// <summary>
    /// Asynchronously removes a product
    /// </summary>
    /// <param name="productId">The id of the product to remove</param>
    /// <param name="cancelPurchases">If true, cancel all pending purchases of the product</param>
    /// <returns></returns>
    Task RemoveAsync(EntityKey<Product> productId, bool cancelPurchases = false);

    /// <summary>
    /// Asynchronously removes a product variant
    /// </summary>
    /// <param name="variantid">The id of the product variant to be removed</param>
    /// <returns></returns>
    Task RemoveVariantAsync(EntityKey<ProductVariant> variantid);

    /// <summary>
    /// Searches products gloablly
    /// </summary>
    /// <param name="query">The query to look for</param>
    /// <returns></returns>
    IQueryBuilder<Product> Search(string query);

    /// <summary>
    /// Asynchronously adds a stock batch to a sellable product variant
    /// </summary>
    /// <param name="variantId">The id of the variant to add the stock batch for</param>
    /// <param name="quantity">The quantity of the stock batch</param>
    /// <returns>The created stock batch</returns>
    Task<StockBatch> AddStockBatchAsync(EntityKey<ProductVariant> variantId, uint quantity);

    /// <summary>
    /// Asynchronously removes a sellable product variant stock batch
    /// </summary>
    /// <param name="batchId">The id of the batch to remove</param>
    /// <returns></returns>
    Task RemoveStockBatchAsync(EntityKey<StockBatch> batchId);

    /// <summary>
    /// Asynchrnously removes a reservable product variant reservation window
    /// </summary>
    /// <param name="windowId"></param>
    /// <returns></returns>
    Task RemoveReservationWindowAsync(EntityKey<ReservationWindow> windowId);

    /// <summary>
    /// Asynchrnously adds a reservation window for a reservable variant
    /// </summary>
    /// <param name="variantId">The id of the variant</param>
    /// <param name="weekdays">The weekdays of reservation window</param>
    /// <param name="start">The start of the reservation </param>
    /// <param name="end"></param>
    /// <returns>The created reservation window</returns>
    Task<ReservationWindow> AddReservationWindowAsync(
        EntityKey<ProductVariant> variantId,
        Weekday weekdays,
        TimeOnly start,
        TimeOnly end);

    /// <summary>
    /// Asynchronously calculates the total available quantity of a sellable product
    /// variant. This method will calculate the quantity with regard to available stock batches
    /// </summary>
    /// <param name="variantId">
    /// The id of the product variant to calculate the total quantity for
    /// </param>
    /// <returns>The total available quantity</returns>
    Task<long> TotalAvailableQuantityAsync(EntityKey<ProductVariant> variantId);

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