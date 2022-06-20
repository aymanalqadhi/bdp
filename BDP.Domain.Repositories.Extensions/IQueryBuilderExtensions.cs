using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions.Exceptions;

namespace BDP.Domain.Repositories.Extensions;

public static class IQueryBuilderExtensions
{
    /// <summary>
    /// Orders the current query ascendingly
    /// </summary>
    /// <returns>The modified query builder</returns>
    public static IQueryBuilder<T> Order<T>(this IQueryBuilder<T> source) where T : AuditableEntity
        => source.OrderBy(x => x.Id);

    /// <summary>
    /// Orders the current query descendingly
    /// </summary>
    /// <returns>The modified query builder</returns>
    public static IQueryBuilder<T> OrderDescending<T>(this IQueryBuilder<T> source) where T : AuditableEntity
        => source.OrderByDescending(x => x.Id);

    /// <summary>
    /// Fetches the page with index <see cref="page"/> and size <see cref="pageLength"/>
    /// </summary>
    /// <param name="page">The index of the page (starts at 1)</param>
    /// <param name="pageLength">The length of the page</param>
    /// <returns>The modified query builder</returns>
    /// <exception cref="InvalidPaginationParametersException"></exception>
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
    /// Fetches the page with index <see cref="page"/> and size <see cref="pageSize"/>. The
    /// data is sorted descendingly first, then the page is fetched
    /// </summary>
    /// <param name="page">The index of the page (starts at 1)</param>
    /// <param name="pageLength">The length of the page</param>
    /// <returns>The modified query builder</returns>
    /// <exception cref="InvalidPaginationParametersException"></exception>
    public static IQueryBuilder<T> PageDescending<T>(this IQueryBuilder<T> source, int page, int pageLength)
        where T : AuditableEntity
    {
        return source
            .OrderDescending()
            .Page(page, pageLength);
    }
}