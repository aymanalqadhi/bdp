using BDP.Domain.Entities;
using BDP.Domain.Repositories;

namespace BDP.Domain.Services;

public interface IUsersService
{
    /// <summary>
    /// Asynchronously gets a user by username
    /// </summary>
    /// <param name="username">The username to look for</param>
    /// <returns>The matching user</returns>
    IQueryBuilder<User> GetByUsername(string username);

    /// <summary>
    /// Asynchronsously searches for users
    /// </summary>
    /// <param name="query">The query to search for</param>
    /// <returns></returns>
    IQueryBuilder<User> Search(string query);

    /// <summary>
    /// Asynchronously adds a user to a specific group
    /// </summary>
    /// <param name="userId">The id of the user which to be added to the group</param>
    /// <param name="groupName">The group which to add the user to</param>
    /// <returns></returns>
    Task AddUserToGroupAsync(EntityKey<User> userId, string groupName);

    /// <summary>
    /// Asynchronously sets reqiured fileds for a profile to be complete
    /// </summary>
    /// <param name="username">The username of the user which to finish his/her profile</param>
    /// <param name="group">The group that the user has chosen</param>
    /// <param name="fullName">The full name of the user</param>
    /// <param name="profilePicture">The user's profile picture</param>
    /// <returns></returns>
    Task<User> FinishProfileAsync(string username, string group, string fullName, IUploadFile? profilePicture = null);
}