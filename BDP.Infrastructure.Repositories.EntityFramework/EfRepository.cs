using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BDP.Infrastructure.Repositories.EntityFramework;

public class EfRepository<T, V> : IRepository<T>
    where T : AuditableEntity
    where V : Validator<T>, new()
{
    private readonly DbSet<T> _set;
    private readonly V _validator;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="set">The DbSet on which the repository operates</param>
    public EfRepository(DbSet<T> set)
    {
        _set = set;
        _validator = new V();
    }

    /// <inheritdoc/>
    public async Task<T?> GetAsync(
        long id,
        Expression<Func<T, object>>[]? includes = null)
    {
        return await WithIncludes(includes).FirstOrDefaultAsync(i => i.Id == id);
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<T> GetAllAsync(Expression<Func<T, object>>[]? includes = null)
        => WithIncludes(includes).AsAsyncEnumerable();

    /// <inheritdoc/>
    public IAsyncEnumerable<T> GetAllAsync(
        int page,
        int pageLength,
        Expression<Func<T, object>>[]? includes = null)
    {
        return WithIncludes(includes)
            .Skip((page - 1) * pageLength)
            .Take(page * pageLength)
            .AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<T> FilterAsync(
        Expression<Func<T, bool>> pred,
        Expression<Func<T, object>>[]? includes = null,
        bool descOrder = false)
    {
        var query = WithIncludes(includes);

        if (descOrder)
            query = query.OrderByDescending(i => i.Id);

        return query.Where(pred).AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<T> FilterAsync(
        int page,
        int pageLength,
        Expression<Func<T, bool>> pred,
        Expression<Func<T, object>>[]? includes = null,
        bool descOrder = false)
    {
        var query = WithIncludes(includes).Where(pred);

        if (descOrder)
            query = query.OrderByDescending(i => i.Id);

        return query
            .Skip((page - 1) * pageLength)
            .Take(page * pageLength)
            .AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> pred,
        Expression<Func<T, object>>[]? includes = null)
    {
        return WithIncludes(includes).FirstOrDefaultAsync(pred);
    }

    /// <inheritdoc/>
    public void Add(T entity)
    {
        var res = _validator.Validate(entity);

        if (!res.IsValid)
            throw new ValidationAggregateException(res.Errors);

        entity.CreatedAt = DateTime.Now;
        entity.ModifiedAt = DateTime.Now;

        _set.Add(entity);
    }

    /// <inheritdoc/>
    public void Update(T entity)
    {
        var res = _validator.Validate(entity);

        if (!res.IsValid)
            throw new ValidationAggregateException(res.Errors);

        entity.ModifiedAt = DateTime.Now;
        _set.Update(entity);
    }

    /// <inheritdoc/>
    public void Remove(T entity)
        => _set.Remove(entity);

    /// <inheritdoc/>
    public Task<int> CountAsync()
        => _set.CountAsync();

    /// <inheritdoc/>
    public Task<int> CountAsync(Expression<Func<T, bool>> pred)
        => _set.CountAsync(pred);

    /// <inheritdoc/>
    public async Task<bool> IsEmptyAsync()
        => await CountAsync() == 0;

    /// <inheritdoc/>
    public Task<bool> AnyAsync(Expression<Func<T, bool>> pred)
        => _set.AnyAsync(pred);

    /// <inheritdoc/>
    public Task<bool> AllAsync(Expression<Func<T, bool>> pred)
        => _set.AllAsync(pred);

    /// <summary>
    /// Gets a queryable object with the passed includes
    /// </summary>
    /// <param name="includes">The includes to add</param>
    /// <returns></returns>
    private IQueryable<T> WithIncludes(Expression<Func<T, object>>[]? includes)
    {
        var query = _set.AsQueryable();

        if (includes?.Any() ?? false)
        {
            foreach (var include in includes)
                query = query.Include(include);
        }

        return query;
    }
}