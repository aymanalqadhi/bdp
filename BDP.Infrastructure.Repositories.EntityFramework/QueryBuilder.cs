using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Exceptions;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace BDP.Infrastructure.Repositories.EntityFramework;

public sealed class QueryBuilder<T> : IQueryBuilder<T> where T : class
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
        try
        {
            return _query.FirstAsync(pred, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            throw new NotFoundException("no items were found", ex);
        }
    }

    /// <inheritdoc/>
    public Task<T?> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
        => _query.FirstOrDefaultAsync(cancellationToken);

    /// <inheritdoc/>
    public Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> pred,
        CancellationToken cancellationToken = default)
    {
        return _query.FirstOrDefaultAsync(pred, cancellationToken);
    }

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

    /// <inheritdoc/>
    public IQueryBuilder<V> Select<V>(Expression<Func<T, V>> selector) where V : class
        => new QueryBuilder<V>(_query.Select(selector));

    /// <inheritdoc/>
    public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        => _query.AnyAsync(cancellationToken);

    #endregion Public Methods
}