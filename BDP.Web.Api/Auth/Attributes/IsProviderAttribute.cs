using Microsoft.AspNetCore.Authorization;

namespace BDP.Web.Api.Auth.Attributes;

public class IsProviderAttribute : AuthorizeAttribute
{
    /// <summary>
    /// Default construcotr
    /// </summary>
    public IsProviderAttribute()
        => Policy = Policies.IsProvider;
}