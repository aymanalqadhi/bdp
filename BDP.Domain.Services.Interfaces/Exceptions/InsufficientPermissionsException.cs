using BDP.Domain.Entities;

namespace BDP.Domain.Services.Exceptions;

/// <summary>
/// An exception to be thrown when a user with insufficient permissions
/// attempts to do a privleaged operation
/// </summary>
public sealed class InsufficientPermissionsException : Exception
{
    #region Fields

    private readonly EntityKey<User> _userId;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="userId">The id of the user who lacks permissions</param>
    /// <param name="message">(optional) a message to clarify the error</param>
    public InsufficientPermissionsException(EntityKey<User> userId, string? message = null)
        : base(message)
    {
        _userId = userId;
    }

    #endregion Public Constructors

    #region Properties

    /// <summary>
    /// Gets the id of the user who lacks sufficient permissions
    /// </summary>
    public EntityKey<User> UserId => _userId;

    #endregion Properties
}