using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions.Exceptions;

namespace BDP.Domain.Repositories.Extensions;

public static class IQueryBuilderExtensions
{
    /// <summary>
    /// Fetches the page with index <see cref="page"/> and size <see cref="pageSize"/>
    /// </summary>
    /// <param name="page">The index of the page (starts at 1)</param>
    /// <param name="pageLength">The length of the page</param>
    /// <returns>The modified query builder</returns>
    /// <exception cref="Exceptions.InvalidPaginationParametersException"></exception>
    public static IQueryBuilder<T> Page<T>(this IQueryBuilder<T> source, int page, int pageLength)
        where T : AuditableEntity
    {
        if (page <= 0 || pageLength <= 0)
            throw new InvalidPaginationParametersException(page, pageLength);

        return source
            .Skip((page - 1) * pageLength)
            .Take(page * pageLength);
    }

    /// <summary>
    /// Asynchronously gets an item by id from the backing store. An exception is
    /// thrown if the item is not found
    /// </summary>
    /// <param name="id">The id of the item to get</param>
    /// <returns>The matched item</returns>
    public static Task<T> FindAsync<T>(
        this IQueryBuilder<T> source,
        long id,
        CancellationToken cancellationToken = default) where T : AuditableEntity
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
        long id,
        CancellationToken cancellationToken = default) where T : AuditableEntity
    {
        return source.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }
}