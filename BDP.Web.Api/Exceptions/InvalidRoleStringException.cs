namespace BDP.Web.Api.Exceptions;

/// <summary>
/// An exception to be thrown when an invalid role string representaion is used
/// </summary>
public sealed class InvalidRoleStringException : Exception
{
    #region Fields

    private readonly string _roleString;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="roleString">The invalid role string value</param>
    public InvalidRoleStringException(string roleString)
        : base($"invalid role string: {roleString}")
    {
        _roleString = roleString;
    }

    #endregion Public Constructors

    #region Properties

    /// <summary>
    /// Gets the invalid role string valu
    /// </summary>
    public string RoleString => _roleString;

    #endregion Properties
}