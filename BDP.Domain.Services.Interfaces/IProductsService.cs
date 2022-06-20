using BDP.Domain.Entities;
using BDP.Domain.Repositories;

namespace BDP.Domain.Services;

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
    /// <returns>The created product</returns>
    Task<Product> AddAsync(
        EntityKey<User> userId,
        string title,
        string description,
        params EntityKey<Category>[] categoryIds);

    /// <summary>
    /// Asynchronously adds a reservable product variant
    /// </summary>
    /// <param name="productId">The product to add the variant to</param>
    /// <param name="title">The title of the variant</param>
    /// <param name="description">The description of the variant</param>
    /// <param name="price">The price of the variant</param>
    /// <param name="attachments">The attachments of the variant</param>
    /// <returns>The created variant object</returns>
    Task<ProductVariant> AddReservableVariantAsync(
        EntityKey<Product> productId,
        string title,
        string? description,
        decimal price,
        IEnumerable<IUploadFile>? attachments = null
     );

    /// <summary>
    /// Asynchronously adds a sellable product variant
    /// </summary>
    /// <param name="productId">The product to add the variant to</param>
    /// <param name="title">The title of the variant</param>
    /// <param name="description">The description of the variant</param>
    /// <param name="price">The price of the variant</param>
    /// <param name="attachments">The attachments of the variant</param>
    /// <returns>The created variant object</returns>
    Task<ProductVariant> AddSellableVariantAsync(
        EntityKey<Product> productId,
        string title,
        string? description,
        decimal price,
        IEnumerable<IUploadFile>? attachments = null
     );

    /// <summary>
    /// Asynchronously gets the available quantity for a sellable product variant
    /// </summary>
    /// <param name="variantId">The id of the sellable product variant</param>
    /// <returns>The avaiable quantity</returns>
    Task<uint> AvailableSellableVariantQuantityAsync(EntityKey<ProductVariant> variantId);

    /// <summary>
    /// Gets products by category
    /// </summary>
    /// <param name="categoryId">The id of the category to get products in</param>
    /// <returns>A query builder for products in the specified category</returns>
    IQueryBuilder<Product> GetByCategory(EntityKey<Category> categoryId);

    /// <summary>
    /// Gets products
    /// </summary>
    /// <returns>A query builder for products</returns>
    IQueryBuilder<Product> GetProducts();

    /// <summary>
    /// Asynchronously checks if a product variant is available or not
    /// </summary>
    /// <param name="variantId">The id of the product to check for</param>
    /// <returns>True if the product variant is available, false otherwise</returns>
    Task<bool> IsAvailableAsync(EntityKey<ProductVariant> variantId);

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