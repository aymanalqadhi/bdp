using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services.Exceptions;

namespace BDP.Domain.Services;

/// <summary>
/// An interface to be implemented by products services
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
        IEnumerable<EntityKey<Category>> categoryIds);

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
    /// <param name="userId">The id of the user owning the product</param>
    /// <param name="productId">The id of the product to remove</param>
    /// <param name="cancelPurchases">If true, cancel all pending purchases of the product</param>
    /// <returns></returns>
    /// <exception cref="PendingPurchasesLeftException"></exception>
    Task RemoveAsync(EntityKey<User> userId, EntityKey<Product> productId, bool cancelPurchases = false);

    /// <summary>
    /// Searches products gloablly
    /// </summary>
    /// <param name="query">The query to look for</param>
    /// <returns></returns>
    IQueryBuilder<Product> Search(string query);

    /// <summary>
    /// Searches products within a user's profile
    /// </summary>
    /// <param name="query">The query to look for</param>
    /// <param name="userId">The id of the user to search in his/her profile</param>
    /// <returns></returns>
    IQueryBuilder<Product> Search(string query, EntityKey<User> userId);

    /// <summary>
    /// Asynchrnonously updates a product
    /// </summary>
    /// <param name="userId">The id of the user owning the product</param>
    /// <param name="productId">The id of the product to update</param>
    /// <param name="title">The title of the product</param>
    /// <param name="description">The description of the product</param>
    /// <returns>The updated product</returns>
    Task<Product> UpdateAsync(
        EntityKey<User> userId,
        EntityKey<Product> productId,
        string title,
        string description);

    #endregion Public Methods
}