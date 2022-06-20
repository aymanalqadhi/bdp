using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services.Exceptions;
using BDP.Domain.Services.Interfaces;

using System.Linq.Expressions;

namespace BDP.Application.App;

public class UsersService : IUsersService
{
    #region Private fields

    private readonly IUnitOfWork _uow;
    private readonly IAttachmentsService _attachmentsSvc;

    #endregion

    #region Ctors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">the unit of work of the application</param>
    public UsersService(IUnitOfWork uow, IAttachmentsService attachmentsSvc)
    {
        _uow = uow;
        _attachmentsSvc = attachmentsSvc;
    }

    #endregion

    #region Public methods

    /// <inheritdoc/>
    public async Task<User> GetByUsernameAsync(
        string username, bool includePhones = false, bool includeGroups = false)
    {
        var includes = PrepareUserIncludes(includePhones, includeGroups);
        var user = await _uow.Users.FirstOrDefaultAsync(
            _uow => _uow.Username == username, includes: includes.ToArray());

        if (user is null)
            throw new NotFoundException($"no users were found with usrename `{username}'");

        return user;
    }

    /// <inheritdoc/>
    public async Task<User> GetByEmailAsync(
        string email, bool includePhones = false, bool includeGroups = false)
    {
        var includes = PrepareUserIncludes(includePhones, includeGroups);
        var user = await _uow.Users.FirstOrDefaultAsync(
            _uow => _uow.Email == email, includes: includes.ToArray());

        if (user is null)
            throw new NotFoundException($"no users were found with email `{email}'");

        return user;
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<User> SearchAsync(
        string query,
        int page,
        int pageSize,
        bool includePhones = false,
        bool includeGroups = false)
    {
        var includes = PrepareUserIncludes(includePhones, includeGroups);

        return _uow.Users.FilterAsync(
            page, pageSize,
            u =>
                u.FullName != null && u.FullName.ToLower().Contains(query.ToLower())
              || u.Username.ToLower().Contains(query.ToLower())
            ,
            includes: includes.ToArray(),
            descOrder: true);
    }


    /// <inheritdoc/>
    public async Task AddUserToGroupAsync(User user, string groupName)
    {
        var group = await _uow.UserGroups.FirstOrDefaultAsync(g => g.Name == groupName);

        if (group is null)
        {
            group = new UserGroup { Name = groupName };
            _uow.UserGroups.Add(group);
        }
        else if (await _uow.UserGroups.AnyAsync(g => g.Users.Any(u => u.Id == user.Id)))
        {
            return;
        }

        group.Users.Add(user);
        user.Groups.Add(group);
        _uow.Users.Update(user);

        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task<User> UpdateFullNameAsync(User user, string fullName)
    {
        user.FullName = fullName;
        await _uow.CommitAsync();
        return user;
    }

    /// <inheritdoc/>
    public async Task<User> FinishProfileAsync(
        string username,
        string group,
        string fullName,
        IUploadFile? profilePicture = null)
    {
        var user = await GetByUsernameAsync(username, includeGroups: true);

        if (user is null)
            throw new NotFoundException();

        user.FullName = fullName ?? string.Empty;

        if (profilePicture is not null)
            user.ProfilePicture = await _attachmentsSvc.SaveAsync(profilePicture);

        await AddUserToGroupAsync(user, group);

        return user;
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Generates an "includes" array for user queries
    /// </summary>
    /// <param name="phones"></param>
    /// <param name="groups"></param>
    /// <returns></returns>
    private static IEnumerable<Expression<Func<User, object>>> PrepareUserIncludes(bool phones, bool groups)
    {
#pragma warning disable CS8603 // Possible null reference return.
        yield return u => u.ProfilePicture;
        yield return u => u.CoverPicture;
#pragma warning restore CS8603 // Possible null reference return.

        if (phones)
            yield return u => u.PhoneNumbers;

        if (groups)
            yield return u => u.Groups;
    }

    #endregion
}
