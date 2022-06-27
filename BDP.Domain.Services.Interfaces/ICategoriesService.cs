using BDP.Domain.Entities;
using BDP.Domain.Repositories;

namespace BDP.Domain.Services;

/// <summary>
/// An service to manage product categories
/// </summary>
public interface ICategoriesService
{
    /// <summary>
    /// Gets product categories
    /// </summary>
    /// <returns>A query builder for product categories</returns>
    IQueryBuilder<Category> GetCategories();

    /// <summary>
    /// Asynchronously adds a product cateogry
    /// </summary>
    /// <param name="userId">The id of the user to add the category with</param>
    /// <param name="name">the name of the category</param>
    /// <param name="parent">The id of the parent of the category (optional)</param>
    /// <returns>The created category object</returns>
    Task<Category> AddAsync(EntityKey<User> userId, string name, EntityKey<Category>? parent = null);

    /// <summary>
    /// Asynchrnously updates a category
    /// </summary>
    /// <param name="userId">The id of the user to update the category with</param>
    /// <param name="categoryId">The id of the category to update</param>
    /// <param name="name">The new name of the category</param>
    /// <returns>The updated category</returns>
    Task<Category> UpdateAsync(EntityKey<User> userId, EntityKey<Category> categoryId, string name);
}