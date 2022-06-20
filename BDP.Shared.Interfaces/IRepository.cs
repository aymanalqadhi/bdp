using BDP.Domain.Entities;
using System.Linq.Expressions;

namespace BDP.Domain.Repositories;

public interface IRepository<T> where T : AuditableEntity
{
    /// <summary>
    /// Asynchronously gets a recrod by id
    /// </summary>
    /// <param name="id">The id by which to get the record</param>
    /// <param name="includes">Additional includes</param>
    /// <returns>The matching record</returns>
    Task<T?> GetAsync(
        long id,
        Expression<Func<T, object>>[]? includes = null);

    /// <summary>
    /// Asynchronously gets all records
    /// </summary>
    /// <param name="includes">Additional includes</param>
    /// <returns></returns>
    IAsyncEnumerable<T> GetAllAsync(Expression<Func<T, object>>[]? includes = null);

    /// <summary>
    /// Asynchronously gets all records, limited by paging
    /// </summary>
    /// <param name="page">The page to fetch</param>
    /// <param name="pageLength">The length of the page</param>
    /// <param name="includes">Additional includes</param>
    /// <returns></returns>
    IAsyncEnumerable<T> GetAllAsync(
        int page,
        int pageLength,
        Expression<Func<T, object>>[]? includes = null);

    /// <summary>
    /// Asynchronously gets all the items in the repository that match the passed
    /// predicate
    /// </summary>
    /// <param name="pred">The predicate used for matching</param>
    /// <param name="includes">Additional includes</param>
    /// <param name="descOrder">Whether to reorder results in a descending order</param>
    /// <returns>A list of all matching items in the repository</returns>
    IAsyncEnumerable<T> FilterAsync(
        Expression<Func<T, bool>> pred,
        Expression<Func<T, object>>[]? includes = null, 
        bool descOrder = false);

    /// <summary>
    /// Asynchronously gets all the items in the repository that match the passed,
    /// limited by paging
    /// </summary>
    /// <param name="page">The page to fetch</param>
    /// <param name="pageLength">The length of the page</param>
    /// <param name="pred">The predicate used for matching</param>
    /// <param name="includes">Additional includes</param>
    /// <param name="descOrder">Whether to reorder results in a descending order</param>
    IAsyncEnumerable<T> FilterAsync(
        int page,
        int pageLength,
        Expression<Func<T, bool>> pred,
        Expression<Func<T, object>>[]? includes = null,
        bool descOrder = false);

    /// <summary>
    /// Asynchoronously gets the first item that matches the passed predicate
    /// if found, or null otherwise
    /// </summary>
    /// <param name="pred">The predicate used for matching</param>
    /// <param name="includes">Additional includes</param>
    /// <returns>The matching item -if found, null otherwise</returns>
    Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> pred,
        Expression<Func<T, object>>[]? includes = null);

    /// <summary>
    /// Adds a record
    /// </summary>
    /// <param name="entity">The entity to add</param>
    void Add(T entity);

    /// <summary>
    /// Updates a record
    /// </summary>
    /// <param name="entity">The entity to update</param>
    void Update(T entity);

    /// <summary>
    /// Removes a record
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <returns>The updated record</returns>
    void Remove(T entity);

    /// <summary>
    /// Asynchrounsly gets the count of all the items in the repository
    /// </summary>
    /// <returns>The count of the items in the repository</returns>
    Task<int> CountAsync();

    /// <summary>
    /// Asynchrounsly gets the count of the items that match the passed
    /// predicate in the repository
    /// </summary>
    /// <param name="pred">The predicate used to match items</param>
    /// <returns>The count of the matching items in the repository</returns>
    Task<int> CountAsync(Expression<Func<T, bool>> pred);

    /// <summary>
    /// Asynchronously gets whether the repository is empty or not
    /// </summary>
    /// <param name="pred"></param>
    /// <returns></returns>
    Task<bool> IsEmptyAsync();

    /// <summary>
    /// Asynchronously gets whether any of the items in the repository match the
    /// passed predicate or not
    /// </summary>
    /// <param name="pred">The predicate used to match items</param>
    /// <returns>True if any of the items in the repository match the predicate, false otherwise</returns>
    Task<bool> AnyAsync(Expression<Func<T, bool>> pred);

    /// <summary>
    /// Asynchronously gets whether all of the items in the repository match the
    /// passed predicate or not
    /// </summary>
    /// <param name="pred">The predicate used to match items</param>
    /// <returns>True if all of the items in the repository match the predicate, false otherwise</returns>
    Task<bool> AllAsync(Expression<Func<T, bool>> pred);
}
