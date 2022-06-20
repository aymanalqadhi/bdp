﻿using System.Linq.Expressions;

namespace BDP.Domain.Repositories;

public interface IQueryBuilder<T> where T : class
{
    #region Public Methods

    /// <summary>
    /// Asynchronously gets whether all items in the query match the
    /// passed expression
    /// </summary>
    /// <param name="pred">the expression used for filering</param>
    /// <param name="cancellationToken">The task cancellation cancellationToken</param>
    /// <returns>True if all items in the query match, false otherise</returns>
    Task<bool> AllAsync(Expression<Func<T, bool>> pred, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously gets whether any of the items in the query match the
    /// passed expression
    /// </summary>
    /// <param name="pred">the expression used for filering</param>
    /// <param name="cancellationToken">The task cancellation cancellationToken</param>
    /// <returns>True if any of items in the query match, false otherise</returns>
    Task<bool> AnyAsync(Expression<Func<T, bool>> pred, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously gets whether the query results in any items or not
    /// passed expression
    /// </summary>
    /// <param name="cancellationToken">The task cancellation cancellationToken</param>
    /// <returns>True if any of items in the query match, false otherise</returns>
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously gets all items in the query
    /// </summary>
    /// <returns>The IAsyncEnumerable item of the items in query</returns>
    IAsyncEnumerable<T> AsAsyncEnumerable();

    /// <summary>
    /// Asynchronously gets the count of the items in the query
    /// </summary>
    /// <returns>The count of the items</returns>
    Task<int> CountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously gets the count of the matching items in the query
    /// </summary>
    /// <param name="pred">The filting expression</param>
    /// <param name="cancellationToken">The task cancellation cancellationToken</param>
    /// <returns>The count of the matching items</returns>
    Task<int> CountAsync(Expression<Func<T, bool>> pred, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously gets the first item in the query. Throws an exception if
    /// no items were found
    /// </summary>
    /// <param name="cancellationToken">The task cancellation cancellationToken</param>
    /// <returns>the first item in the query</returns>
    Task<T> FirstAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously gets the first item in the query. Throws an exception if
    /// no items were found
    /// </summary>
    /// <param name="pred">The filting expression</param>
    /// <param name="cancellationToken">The task cancellation cancellationToken</param>
    /// <returns>the first item in the query</returns>
    Task<T> FirstAsync(
        Expression<Func<T, bool>> pred,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously gets the first item in the query. A null is returned if
    /// no items were found
    /// </summary>
    /// <returns>the first item in the query if found, null otherwise</returns>
    Task<T?> FirstOrDefaultAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously gets the first item in the query. A null is returned if
    /// no items were found
    /// </summary>
    /// <param name="pred">The filting expression</param>
    /// <returns>the first item in the query if found, null otherwise</returns>
    Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> pred,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds an include to the query
    /// </summary>
    /// <param name="expr">The expression used to specify the include</param>
    /// <returns>The modified query builder</returns>
    IQueryBuilder<T> Include(Expression<Func<T, object>> expr);

    /// <summary>
    /// Orders the current query ascendingly
    /// </summary>
    /// <param name="expr">The expression used for ordering</param>
    /// <returns>The modified query builder</returns>
    IQueryBuilder<T> OrderBy(Expression<Func<T, object>> expr);

    /// <summary>
    /// Orders the current query descendingly
    /// </summary>
    /// <param name="expr">The expression used for ordering</param>
    /// <returns>The modified query builder</returns>
    IQueryBuilder<T> OrderByDescending(Expression<Func<T, object>> expr);

    /// <summary>
    /// Projects into an element in the sequence into a new form
    /// </summary>
    /// <typeparam name="V">The new type of the sequence</typeparam>
    /// <param name="selector">The selector expression used for projection</param>
    /// <returns>A new query builder for the new sequence form</returns>
    IQueryBuilder<V> Select<V>(Expression<Func<T, V>> selector) where V : class;

    /// <summary>
    /// Skips the first <see cref="count"/> items
    /// </summary>
    /// <param name="count">The number of items to skip</param>
    /// <returns>The modified query builder</returns>
    IQueryBuilder<T> Skip(int count);

    /// <summary>
    /// Takes the first <see cref="count"/> items
    /// </summary>
    /// <param name="count">The number of items to take</param>
    /// <returns>The modified query builder</returns>
    IQueryBuilder<T> Take(int count);

    /// <summary>
    /// Adds a filter to the query
    /// </summary>
    /// <param name="pred">The filting expression</param>
    /// <returns>The modified query builder</returns>
    IQueryBuilder<T> Where(Expression<Func<T, bool>> pred);

    #endregion Public Methods
}