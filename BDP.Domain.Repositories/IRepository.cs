using BDP.Domain.Entities;

namespace BDP.Domain.Repositories;

/// <summary>
/// A generic repositroy
/// </summary>
/// <typeparam name="T">The type the repository works on</typeparam>
public interface IRepository<T> where T : AuditableEntity<T>
{
    #region Public Methods

    /// <summary>
    /// Adds an item to the repository
    /// </summary>
    /// <param name="entities">The items to add</param>
    void Add(params T[] entities);

    /// <summary>
    /// Creates a query builder for <see cref="T"/>
    /// </summary>
    /// <returns>The query builder object</returns>
    IQueryBuilder<T> Query();

    /// <summary>
    /// Deletes an item from the repositry
    /// </summary>
    /// <param name="entity">The item to delete</param>
    void Remove(T entity);

    /// <summary>
    /// Updates an item in the repository
    /// </summary>
    /// <param name="entity">The updated item</param>
    void Update(T entity);

    #endregion Public Methods
}