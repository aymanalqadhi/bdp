using Microsoft.AspNetCore.Authorization;

namespace BDP.Web.Api.Auth.Attributes;

public class IsAdminAttribute : AuthorizeAttribute
{
    /// <summary>
    /// Default construcotr
    /// </summary>
    public IsAdminAttribute()
        => Policy = Policies.IsAdmin;
}
