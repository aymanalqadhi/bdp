using BDP.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BDP.Web.Api.Auth.Requirements.Handlers;

public class HasAllRolesRequirementHandler : AuthorizationHandler<HasAllRolesRequirement>
{
    private readonly IConfigurationService _configSvc;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="configSvc">Application configuration service</param>
    public HasAllRolesRequirementHandler(IConfigurationService configSvc)
    {
        _configSvc = configSvc;
    }

    /// <inheritdoc/>
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        HasAllRolesRequirement requirement)
    {
        if (context.User.IsInRole(UserRoles.Root) && _configSvc.GetBool("EnableRoot", false))
            context.Succeed(requirement);
        else if (requirement.Roles.All(context.User.IsInRole))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
