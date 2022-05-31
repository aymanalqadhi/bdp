namespace BDP.Web.Api.Auth.Requirements;

public class IsProviderRequirement : HasAllRolesRequirement
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public IsProviderRequirement() : base(UserRoles.Provider)
    {
    }
}