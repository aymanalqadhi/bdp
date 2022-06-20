using Microsoft.AspNetCore.Authorization;

namespace BDP.Web.Api.Auth.Attributes;

public class IsRootAttribute : AuthorizeAttribute
{
    /// <summary>
    /// Default construcotr
    /// </summary>
    public IsRootAttribute()
        => Policy = Policies.IsRoot;
}
