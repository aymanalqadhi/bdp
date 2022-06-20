using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;

using System.Linq.Expressions;

namespace BDP.Application.App;

public class UsersService : IUsersService
{
    #region Private fields

    private readonly IUnitOfWork _uow;
    private readonly IAttachmentsService _attachmentsSvc;

    #endregion Private fields

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

    #endregion Ctors

    #region Public methods

    /// <inheritdoc/>
    public IQueryBuilder<User> GetByUsername(string username)
        => _uow.Users.Query().Where(u => u.Username == username);

    /// <inheritdoc/>
    public IQueryBuilder<User> Search(string query)
    {
        return _uow.Users
            .Query()
            .Where(u => u.FullName != null &&
                        u.FullName.ToLower().Contains(query.ToLower()) ||
                        u.Username.ToLower().Contains(query.ToLower()));
    }

    /// <inheritdoc/>
    public async Task AddUserToGroupAsync(EntityKey<User> userId, string groupName)
    {
        // TODO:
        // Fix the logic here

        var user = await _uow.Users.Query().FindAsync(userId);
        var group = await _uow.UserGroups.Query().FirstOrDefaultAsync(g => g.Name == groupName);

        if (group is null)
        {
            group = new UserGroup { Name = groupName };
            _uow.UserGroups.Add(group);
        }
        else if (await _uow.UserGroups.Query().AnyAsync(g => g.Users.Any(u => u.Id == user.Id)))
        {
            return;
        }

        group.Users.Add(user);
        user.Groups.Add(group);
        _uow.Users.Update(user);

        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task<User> FinishProfileAsync(
        string username,
        string group,
        string fullName,
        IUploadFile? profilePicture = null)
    {
        var user = await GetByUsername(username)
            .Include(u => u.Groups)
            .FirstAsync();

        user.FullName = fullName ?? string.Empty;

        if (profilePicture is not null)
            user.ProfilePicture = await _attachmentsSvc.SaveAsync(profilePicture);

        await AddUserToGroupAsync(user.Id, group);

        return user;
    }

    #endregion Public methods
}