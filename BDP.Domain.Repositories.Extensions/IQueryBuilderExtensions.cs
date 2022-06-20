using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions.Exceptions;

namespace BDP.Domain.Repositories.Extensions;

public static class IQueryBuilderExtensions
{
    /// <summary>
    /// Orders the current query ascendingly
    /// </summary>
    /// <returns>The modified query builder</returns>
    public static IQueryBuilder<T> Order<T>(this IQueryBuilder<T> source) where T : AuditableEntity<T>
        => source.OrderBy(x => x.CreatedAt);

    /// <summary>
    /// Orders the current query descendingly
    /// </summary>
    /// <returns>The modified query builder</returns>
    public static IQueryBuilder<T> OrderDescending<T>(this IQueryBuilder<T> source) where T : AuditableEntity<T>
        => source.OrderByDescending(x => x.CreatedAt);

    /// <summary>
    /// Fetches the page with index <see cref="page"/> and size <see cref="pageLength"/>
    /// </summary>
    /// <param name="page">The index of the page (starts at 1)</param>
    /// <param name="pageLength">The length of the page</param>
    /// <returns>The modified query builder</returns>
    /// <exception cref="InvalidPaginationParametersException"></exception>
    public static IQueryBuilder<T> Page<T>(this IQueryBuilder<T> source, int page, int pageLength)
        where T : AuditableEntity<T>
    {
        if (page <= 0 || pageLength <= 0)
            throw new InvalidPaginationParametersException(page, pageLength);

        return source
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
    public static IQueryBuilder<T> PageDescending<T>(this IQueryBuilder<T> source, int page, int pageLength)
        where T : AuditableEntity<T>
    {
        return source
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
        this IQueryBuilder<T> source,
        EntityKey<T> id,
        CancellationToken cancellationToken = default) where T : AuditableEntity<T>
    {
        return source.FirstAsync(i => i.Id == id, cancellationToken);
    }

    /// <summary>
    /// Asynchronously gets an item by id from the backing store. If the item is not
    /// found, null is returned.
    /// </summary>
    /// <param name="id">The id of the item to get</param>
    /// <returns>The matched item if found, null otherwise</returns>
    public static Task<T?> FindOrDefaultAsync<T>(
        this IQueryBuilder<T> source,
        EntityKey<T> id,
        CancellationToken cancellationToken = default) where T : AuditableEntity<T>
    {
        return source.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }
}