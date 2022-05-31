using BDP.Web.Api.Auth.Requirements;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace BDP.Web.Api.Auth;

internal class PoliciesProvider : IAuthorizationPolicyProvider
{
    private static readonly IDictionary<string, IEnumerable<IAuthorizationRequirement>> _policies
        = new Dictionary<string, IEnumerable<IAuthorizationRequirement>>
        {
            { Policies.IsCustomer,  new[] { new IsCustomerRequirement() } },
            { Policies.IsProvider,  new[] { new IsProviderRequirement() } },
            { Policies.IsAdmin,  new[] { new IsAdminRequirement() } },
            { Policies.IsRoot,  new[] { new IsRootRequirement() } },
        };

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="options">internal authorization options</param>
    public PoliciesProvider(IOptions<AuthorizationOptions> options)
    {
        BackupPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    /// <inheritdoc/>
    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        return Task.FromResult(
            new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser().Build());
    }

    /// <inheritdoc/>
    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        => Task.FromResult<AuthorizationPolicy?>(null);

    /// <inheritdoc/>
    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (!_policies.ContainsKey(policyName))
            return BackupPolicyProvider.GetPolicyAsync(policyName);

        var builder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
        builder.AddRequirements(_policies[policyName].ToArray());

        return Task.FromResult<AuthorizationPolicy?>(builder.Build());
    }

    /// <summary>
    /// Gets the backup policy proivder
    /// </summary>
    private DefaultAuthorizationPolicyProvider BackupPolicyProvider { get; }
}