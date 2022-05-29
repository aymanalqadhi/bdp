using BDP.Domain.Entities;

namespace BDP.Domain.Services.Interfaces;

public interface ISellablesService
{
    /// <summary>
    /// Gets a sellable item by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Sellable> GetByIdAsync(long id);

    /// <summary>
    /// Asynchronosly gets sellables for a user, limited with pagination
    /// </summary>
    /// <param name="user">The user which to get the products for</param>
    /// <param name="page">The page to get</param>
    /// <param name="pageSize">The pages size</param>
    /// <returns></returns>
    IAsyncEnumerable<Sellable> GetForAsync(User user, int page, int pageSize);

    /// <summary>
    /// Asynchronosly searches sellables for a user, limited with paging
    /// </summary>
    /// <param name="user">The user which to get the products for</param>
    /// <param name="query">The to look for</param>
    /// <param name="page">The page to get</param>
    /// <param name="pageSize">The pages size</param>
    /// <returns></returns>
    IAsyncEnumerable<Sellable> SearchForAsync(User user, string query, int page, int pageSize);

    /// <summary>
    /// Asynchronosly searches sellables gloablly, limited with paging
    /// </summary>
    /// <param name="query">The to look for</param>
    /// <param name="page">The page to get</param>
    /// <param name="pageSize">The pages size</param>
    /// <returns></returns>
    IAsyncEnumerable<Sellable> SearchAsync(string query, int page, int pageSize);
}
