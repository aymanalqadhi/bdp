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
    /// <param name="name">the name of the category</param>
    /// <param name="parent">The parent of the category (optional)</param>
    /// <returns>The created category object</returns>
    Task<Category> AddAsync(string name, Category? parent = null);
}