using BDP.Domain.Entities;
using System.Linq.Expressions;

namespace BDP.Domain.Services.Interfaces;

public interface IPurchasesService
{
    /// <summary>
    /// Asynchronously gets a purchase by id
    /// </summary>
    /// <param name="id">The id of the purchase</param>
    /// <returns></returns>
    Task<Purchase> GetById(long id);

    /// <summary>
    /// Asynchronosly gets purchases for a user, limited with pagination
    /// </summary>
    /// <param name="user">The user which to get the purchases for</param>
    /// <param name="page">The page to get</param>
    /// <param name="pageSize">The pages size</param>
    /// <param name="includes">Additional includes</param>
    /// <returns></returns>
    IAsyncEnumerable<Purchase> ForUserAsync(
        int page,
        int pageSize,
        User user,
        Expression<Func<Purchase, object>>[]? includes = null);
}
