using BDP.Domain.Entities;

namespace BDP.Domain.Services.Exceptions;

/// <summary>
/// An exception to be thrown when a user tries to create a profile
/// when he/she has already created one
/// </summary>
public sealed class ExistingProfileFoundException : Exception
{
    #region Fields

    private readonly EntityKey<User> _userId;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="userId">The id of the user who already has a profile</param>
    public ExistingProfileFoundException(EntityKey<User> userId)
        : base($"user #{userId} already has a profile")
    {
        _userId = userId;
    }

    #endregion Public Constructors

    #region Properties

    /// <summary>
    /// Gets the id of the user who already has a profile
    /// </summary>
    public EntityKey<User> UserId => _userId;

    #endregion Properties
}