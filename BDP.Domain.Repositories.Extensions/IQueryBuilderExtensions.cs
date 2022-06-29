using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions.Exceptions;

using System.Linq.Expressions;

namespace BDP.Domain.Repositories.Extensions;

public static class IQueryBuilderExtensions
{
    /// <summary>
    /// Orders the current query ascendingly
    /// </summary>
    /// <returns>The modified query builder</returns>
    public static IQueryBuilder<T> Order<T>(this IQueryBuilder<T> self) where T : AuditableEntity<T>
        => self.OrderBy(x => x.CreatedAt);

    /// <summary>
    /// Orders the current query descendingly
    /// </summary>
    /// <returns>The modified query builder</returns>
    public static IQueryBuilder<T> OrderDescending<T>(this IQueryBuilder<T> self) where T : AuditableEntity<T>
        => self.OrderByDescending(x => x.CreatedAt);

    /// <summary>
    /// Fetches the page with index <see cref="page"/> and size <see cref="pageLength"/>
    /// </summary>
    /// <param name="page">The index of the page (starts at 1)</param>
    /// <param name="pageLength">The length of the page</param>
    /// <returns>The modified query builder</returns>
    /// <exception cref="InvalidPaginationParametersException"></exception>
    public static IQueryBuilder<T> Page<T>(this IQueryBuilder<T> self, int page, int pageLength)
        where T : AuditableEntity<T>
    {
        if (page <= 0 || pageLength <= 0)
            throw new InvalidPaginationParametersException(page, pageLength);

        return self
            .Skip((page - 1) * pageLength)
            .Take(page * pageLength);
    }

    /// <summary>
    /// Fetches the page with index <see cref="page"/> and size <see cref="pageSize"/>. The
    /// data is sorted descendingly first, then the page is fetched
    /// </summary>
    /// <param name="page">The index of the page (starts at 1)</param>
    /// <param name="pageLength">The length of the page</param>
    /// <returns>The modified query builder</returns>
    /// <exception cref="InvalidPaginationParametersException"></exception>
    public static IQueryBuilder<T> PageDescending<T>(this IQueryBuilder<T> self, int page, int pageLength)
        where T : AuditableEntity<T>
    {
        return self
            .OrderDescending()
            .Page(page, pageLength);
    }

    /// <summary>
    /// Asynchronously gets an item by id from the backing store. An exception is
    /// thrown if the item is not found
    /// </summary>
    /// <param name="id">The id of the item to get</param>
    /// <returns>The matched item</returns>
    public static Task<T> FindAsync<T>(
        this IQueryBuilder<T> self,
        EntityKey<T> id,
        CancellationToken cancellationToken = default) where T : AuditableEntity<T>
    {
        return self.FirstAsync(i => i.Id == id, cancellationToken);
    }

    /// <summary>
    /// Asynchronously gets an item by id from the backing store. If the item is not
    /// found, null is returned.
    /// </summary>
    /// <param name="id">The id of the item to get</param>
    /// <returns>The matched item if found, null otherwise</returns>
    public static Task<T?> FindOrDefaultAsync<T>(
        this IQueryBuilder<T> self,
        EntityKey<T> id,
        CancellationToken cancellationToken = default) where T : AuditableEntity<T>
    {
        return self.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    /// <summary>
    /// Applies a filter if the passed condition is true
    /// found, null is returned.
    /// </summary>
    /// <param name="expr">The predicate expression to be applied as a filter</param>
    /// <param name="cond">The condition upon which to apply the filter</param>
    /// <returns>
    /// A filtered query builder if <paramref name="cond"/> is true, unmodified otheriwse
    /// </returns>
    public static IQueryBuilder<T> WhereIf<T>(
        this IQueryBuilder<T> self,
        Expression<Func<T, bool>> expr,
        bool cond) where T : AuditableEntity<T>
    {
        return cond ? self.Where(expr) : self;
    }
}