using BDP.Domain.Entities;
using BDP.Domain.Repositories;

using System.Linq.Expressions;

namespace BDP.Domain.Services;

public interface IPurchasesService
{
    /// <summary>
    /// Asynchronously gets a purchase by id
    /// </summary>
    /// <param name="id">The id of the purchase</param>
    /// <returns></returns>
    Task<Purchase> GetById(EntityKey<Purchase> id);

    /// <summary>
    /// Asynchronosly gets purchases for a user, limited with pagination
    /// </summary>
    /// <param name="userId">The id of the user which to get the purchases for</param>
    /// <returns></returns>
    IQueryBuilder<Purchase> ForUserAsync(EntityKey<User> userId);
}