using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Domain.Services.Exceptions;

namespace BDP.Application.App;

/// <inheritdoc/>
public class UserProfilesService : IUserProfilesService
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
    public UserProfilesService(IUnitOfWork uow, IAttachmentsService attachmentsSvc)
    {
        _uow = uow;
        _attachmentsSvc = attachmentsSvc;
    }

    #endregion Ctors

    #region Public methods

    /// <inheritdoc/>
    public IQueryBuilder<UserProfile> Search(string query)
    {
        return _uow.UserProfiles
            .Query()
            .Where(u => u.FullName != null &&
                        u.FullName.ToLower().Contains(query.ToLower()) ||
                        u.User.Username.ToLower().Contains(query.ToLower()))
            .Include(p => p.User);
    }

    /// <inheritdoc/>
    public IQueryBuilder<UserProfile> GetByUsername(string username)
    {
        return _uow.UserProfiles.Query()
            .Where(p => p.User.Username == username)
            .Include(p => p.User);
    }

    /// <inheritdoc/>
    public async Task<UserProfile> CreateAsync(
        EntityKey<User> userId,
        UserRole role,
        string fullName,
        IUploadFile? profilePicture = null,
        IUploadFile? coverPicture = null)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        var user = await _uow.Users.Query().FindAsync(userId);

        if (user.Role != UserRole.None ||
            await _uow.UserProfiles.Query().AnyAsync(p => p.UserId == userId))
        {
            throw new ExistingProfileFoundException(userId);
        }

        user.Role = role;

        var profile = new UserProfile
        {
            FullName = fullName,
            UserId = userId,
            User = user,
        };

        if (profilePicture is not null)
            profile.ProfilePicture = await _attachmentsSvc.SaveAsync(profilePicture);

        if (coverPicture is not null)
            profile.CoverPicture = await _attachmentsSvc.SaveAsync(coverPicture);

        _uow.Users.Update(user);
        _uow.UserProfiles.Add(profile);
        await _uow.CommitAsync(tx);

        return profile;
    }

    #endregion Public methods
}