using Microsoft.AspNetCore.Authorization;

namespace BDP.Web.Api.Auth.Attributes;

public class IsCustomerAttribute : AuthorizeAttribute
{
    /// <summary>
    /// Default construcotr
    /// </summary>
    public IsCustomerAttribute()
        => Policy = Policies.IsCustomer;
}
