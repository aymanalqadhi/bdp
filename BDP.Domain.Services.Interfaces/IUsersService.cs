using BDP.Domain.Entities;

namespace BDP.Domain.Services;

public interface IUsersService
{
    /// <summary>
    /// Asynchronously gets a user by username
    /// </summary>
    /// <param name="username">The username to look for</param>
    /// <param name="includeGroups">whether to include groups or not</param>
    /// <param name="includePhones">whether to include phone numbers or not</param>
    /// <returns>The matching user</returns>
    Task<User> GetByUsernameAsync(string username, bool includePhones = false, bool includeGroups = false);

    /// <summary>
    /// Asynchronously gets a user by email
    /// </summary>
    /// <param name="email">The email to look for</param>
    /// <param name="includeGroups">whether to include groups or not</param>
    /// <param name="includePhones">whether to include phone numbers or not</param>
    /// <returns>The matching user</returns>
    Task<User> GetByEmailAsync(string email, bool includePhones = false, bool includeGroups = false);

    /// <summary>
    /// Asynchronsously searches for users
    /// </summary>
    /// <param name="query">The query to search for</param>
    /// <param name="includeGroups">Whether to include groups or not</param>
    /// <param name="includePhones">Whether to include phone numbers or not</param>
    /// <returns></returns>
    IAsyncEnumerable<User> SearchAsync(
        string query,
        int page,
        int pageSize,
        bool includePhones = false,
        bool includeGroups = false);

    /// <summary>
    /// Asynchronously adds a user to a specific group
    /// </summary>
    /// <param name="user">The user which to be added to the group</param>
    /// <param name="groupName">The group which to add the user to</param>
    /// <returns></returns>
    Task AddUserToGroupAsync(User user, string groupName);

    /// <summary>
    /// Asynchronously updates a user's full name
    /// </summary>
    /// <param name="user">The user which to update his/her name</param>
    /// <param name="fullName">The new full name</param>
    /// <returns></returns>
    Task<User> UpdateFullNameAsync(User user, string fullName);

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