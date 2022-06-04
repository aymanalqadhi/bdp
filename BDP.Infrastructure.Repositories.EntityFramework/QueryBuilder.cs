﻿using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Exceptions;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace BDP.Infrastructure.Repositories.EntityFramework;

public sealed class QueryBuilder<T> : IQueryBuilder<T> where T : AuditableEntity
{
    #region Fields

    private IQueryable<T> _query;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="query">EntityFramework query object</param>
    public QueryBuilder(IQueryable<T> query)
    {
        _query = query;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public Task<bool> AllAsync(Expression<Func<T, bool>> pred, CancellationToken cancellationToken = default)
        => _query.AllAsync(pred, cancellationToken);

    /// <inheritdoc/>
    public Task<bool> AnyAsync(Expression<Func<T, bool>> pred, CancellationToken cancellationToken = default)
        => _query.AnyAsync(pred, cancellationToken);

    /// <inheritdoc/>
    public IAsyncEnumerable<T> AsAsyncEnumerable()
        => _query.AsAsyncEnumerable();

    /// <inheritdoc/>
    public Task<int> CountAsync(CancellationToken cancellationToken = default)
        => _query.CountAsync(cancellationToken);

    /// <inheritdoc/>
    public Task<int> CountAsync(Expression<Func<T, bool>> pred, CancellationToken cancellationToken = default)
        => _query.CountAsync(pred, cancellationToken);

    /// <inheritdoc/>
    public Task<T> FirstAsync(CancellationToken cancellationToken = default)
        => _query.FirstAsync(cancellationToken);

    /// <inheritdoc/>
    public Task<T> FirstAsync(
        Expression<Func<T, bool>> pred,
        CancellationToken cancellationToken = default)
    {
        return _query.FirstAsync(pred, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<T?> FirstOrNullAsync(CancellationToken cancellationToken = default)
        => _query.FirstOrDefaultAsync(cancellationToken);

    /// <inheritdoc/>
    public Task<T?> FirstOrNullAsync(
        Expression<Func<T, bool>> pred,
        CancellationToken cancellationToken = default)
    {
        return _query.FirstOrDefaultAsync(pred, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<T> FindAsync(long id)
        => FirstAsync(i => i.Id == id);

    /// <inheritdoc/>
    public Task<T?> FindOrDefaultAsync(long id)
        => FirstOrNullAsync(i => i.Id == id);

    /// <inheritdoc/>
    public Task<bool> HasItemsAsync(CancellationToken cancellationToken = default)
        => _query.AnyAsync(cancellationToken);

    /// <inheritdoc/>
    public IQueryBuilder<T> Include(Expression<Func<T, object>> expr)
    {
        _query = _query.Include(expr);

        return this;
    }

    /// <inheritdoc/>
    public IQueryBuilder<T> IncludeAll(IEnumerable<Expression<Func<T, object>>> exprs)
    {
        foreach (var expr in exprs)
            _query = _query.Include(expr);

        return this;
    }

    /// <inheritdoc/>
    public IQueryBuilder<T> OrderBy(Expression<Func<T, object>> expr)
    {
        _query = _query.OrderBy(expr);

        return this;
    }

    /// <inheritdoc/>
    public IQueryBuilder<T> OrderByDescending(Expression<Func<T, object>> expr)
    {
        _query = _query.OrderByDescending(expr);

        return this;
    }

    /// <summary>
    /// Fetches the page with index <see cref="page"/> and size <see cref="pageSize"/>
    /// </summary>
    /// <param name="page">The index of the page</param>
    /// <param name="pageSize">The size of the page</param>
    /// <returns>The modified query builder</returns>
    public IQueryBuilder<T> Page(int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0)
            throw new InvalidPaginationParametersException(page, pageSize);

        _query = _query
            .Skip((page - 1) * pageSize)
            .Take(page * pageSize);

        return this;
    }

    /// <inheritdoc/>
    public IQueryBuilder<T> Skip(int count)
    {
        _query = _query.Skip(count);

        return this;
    }

    /// <inheritdoc/>
    public IQueryBuilder<T> Take(int count)
    {
        _query = _query.Take(count);

        return this;
    }

    /// <inheritdoc/>
    public IQueryBuilder<T> Where(Expression<Func<T, bool>> pred)
    {
        _query = _query.Where(pred);

        return this;
    }

    #endregion Public Methods
}