using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services.Exceptions;

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
    /// <param name="userId">The id of the user owning the product variant</param>
    /// <param name="productId">The product to add the variant to</param>
    /// <param name="name">The name of the variant</param>
    /// <param name="description">The description of the variant</param>
    /// <param name="price">The price of the variant</param>
    /// <param name="attachments">The attachments of the variant</param>
    /// <returns>The created variant object</returns>
    /// <exception cref="InvalidPriceException"></exception>
    Task<ProductVariant> AddReservableAsync(
        EntityKey<User> userId,
        EntityKey<Product> productId,
        string name,
        string? description,
        decimal price,
        IEnumerable<IUploadFile>? attachments = null
     );

    /// <summary>
    /// Asynchronously adds a sellable product variant
    /// </summary>
    /// <param name="userId">The id of the user owning the product variant</param>
    /// <param name="productId">The product to add the variant to</param>
    /// <param name="name">The name of the variant</param>
    /// <param name="description">The description of the variant</param>
    /// <param name="price">The price of the variant</param>
    /// <param name="attachments">The attachments of the variant</param>
    /// <returns>The created variant object</returns>
    /// <exception cref="InvalidPriceException"></exception>
    Task<ProductVariant> AddSellableAsync(
        EntityKey<User> userId,
        EntityKey<Product> productId,
        string name,
        string? description,
        decimal price,
        IEnumerable<IUploadFile>? attachments = null
     );

    /// <summary>
    /// Gets variants for a specific product
    /// </summary>
    /// <param name="productId">The id of the product to get its variants</param>
    /// <returns>A query builder for product variants</returns>
    IQueryBuilder<ProductVariant> GetVariants(EntityKey<Product> productId);

    /// <summary>
    /// Asynchronously removes a product variant
    /// </summary>
    /// <param name="userId">The id of the user owning the product variant</param>
    /// <param name="variantid">The id of the product variant to be removed</param>
    /// <returns></returns>
    Task RemoveVariantAsync(EntityKey<User> userId, EntityKey<ProductVariant> variantid);

    /// <summary>
    /// Asynchronously adds a reservable product variant
    /// </summary>
    /// <param name="userId">The id of the user owning the product variant</param>
    /// <param name="name">The name of the variant</param>
    /// <param name="description">The description of the variant</param>
    /// <param name="price">The price of the variant</param>
    /// <returns>The created variant object</returns>
    /// <exception cref="InvalidPriceException"></exception>
    Task<ProductVariant> UpdateAsync(
        EntityKey<User> userId,
        EntityKey<ProductVariant> variantId,
        string name,
        string? description,
        decimal price
     );

    #endregion Public Methods
}