using BDP.Domain.Entities;

namespace BDP.Web.Api.Auth;

public static class UserRoleConverter
{
    /// <summary>
    /// Converts a role value to string
    /// </summary>
    /// <param name="role">The role value</param>
    /// <returns></returns>
    public static string FromRole(UserRole role)
    {
        return role switch
        {
            UserRole.Root => "root",
            UserRole.Admin => "admin",
            UserRole.Customer => "customer",
            UserRole.Provider => "provider",

            _ => string.Empty,
        };
    }
}