using BDP.Domain.Repositories;

using AutoMapper;

namespace BDP.Web.Api.Extensions;

public static class IQueryBuilderExtensions
{
    /// <summary>
    /// A utility method to map a <see cref="IQueryBuilder{T}"/> using AutoMapper
    /// </summary>
    /// <typeparam name="T">The source type</typeparam>
    /// <typeparam name="V">The destination type</typeparam>
    /// <param name="source">The source query builder</param>
    /// <param name="mapper">The mapper object</param>
    /// <returns>The modified query builder object</returns>
    public static IQueryBuilder<V> Map<T, V>(this IQueryBuilder<T> source, IMapper mapper)
        where T : class
        where V : class
    {
        return source.Select(i => mapper.Map<V>(i));
    }
}