using BDP.Domain.Entities;

namespace BDP.Web.Api.Auth.Requirements;

public class IsCustomerRequirement : HasAllRolesRequirement
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public IsCustomerRequirement() : base(UserRole.Customer)
    {
    }
}