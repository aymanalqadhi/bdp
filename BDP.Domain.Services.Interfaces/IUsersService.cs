using BDP.Domain.Entities;
using BDP.Domain.Repositories;

namespace BDP.Domain.Services;

/// A service to manage users on the application
public interface IUsersService
{
    /// <summary>
    /// Gets a user by username
    /// </summary>
    /// <param name="username">The username to look for</param>
    /// <returns>The matching user</returns>
    IQueryBuilder<User> GetByUsername(string username);
}