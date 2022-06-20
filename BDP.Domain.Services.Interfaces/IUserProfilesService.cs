using BDP.Domain.Entities;
using BDP.Domain.Repositories;

namespace BDP.Domain.Services;

/// <summary>
/// A service to manage users' profiles
/// </summary>
public interface IUserProfilesService
{
    /// <summary>
    /// Searches for users
    /// </summary>
    /// <param name="query">The query to search for</param>
    /// <returns></returns>
    IQueryBuilder<UserProfile> Search(string query);

    /// <summary>
    /// Gets a user profile by username
    /// </summary>
    /// <param name="username">The username to look for</param>
    /// <returns>The matching user</returns>
    IQueryBuilder<UserProfile> GetByUsername(string username);

    /// <summary>
    /// Asynchrnously creates a profile for a user
    /// </summary>
    /// <param name="userId">The id of the user to create the profile for</param>
    /// <param name="role">The role to set for the user</param>
    /// <param name="fullName">The full name of the user</param>
    /// <param name="profilePicture">(optional) profile picture file</param>
    /// <returns>The created user profile</returns>
    Task<UserProfile> CreateAsync(
        EntityKey<User> userId,
        UserRole role,
        string fullName,
        IUploadFile? profilePicture = null);
}