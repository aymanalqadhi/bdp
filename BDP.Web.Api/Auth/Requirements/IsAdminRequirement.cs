namespace BDP.Web.Api.Auth.Requirements;

public class IsAdminRequirement : HasAllRolesRequirement
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public IsAdminRequirement() : base(UserRoles.Admin)
    {
    }
}