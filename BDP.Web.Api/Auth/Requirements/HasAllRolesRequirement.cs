using BDP.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BDP.Web.Api.Auth.Requirements;

public class HasAllRolesRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="roles">The allowed roles</param>
    public HasAllRolesRequirement(params UserRole[] roles)
        => Roles = roles;

    /// <summary>
    /// Gets or sets the required roles
    /// </summary>
    public UserRole[] Roles { get; init; }
}