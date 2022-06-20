using BDP.Domain.Entities;
using BDP.Web.Api.Exceptions;

namespace BDP.Web.Api.Auth;

public static class UserRoleConverter
{
    #region Fields

    private const string _adminRole = "admin";
    private const string _customerRole = "customer";
    private const string _providerRole = "provider";
    private const string _rootRole = "root";

    #endregion Fields

    #region Public Methods

    /// <summary>
    /// Converts a role value to string
    /// </summary>
    /// <param name="role">The role value</param>
    /// <returns></returns>
    public static string FromRole(UserRole role)
        => role switch
        {
            UserRole.Root => _rootRole,
            UserRole.Admin => _adminRole,
            UserRole.Customer => _customerRole,
            UserRole.Provider => _providerRole,

            _ => string.Empty,
        };

    /// <summary>
    /// Parses a role from string representation
    /// </summary>
    /// <param name="role">the role to parse</param>
    /// <returns></returns>
    /// <exception cref="InvalidRoleStringException">
    /// Thrown when an invalid role string value is used
    /// </exception>
    public static UserRole Parse(string role)
        => role switch
        {
            _rootRole => UserRole.Root,
            _adminRole => UserRole.Admin,
            _customerRole => UserRole.Customer,
            _providerRole => UserRole.Provider,

            _ => throw new InvalidRoleStringException(role),
        };

    #endregion Public Methods
}