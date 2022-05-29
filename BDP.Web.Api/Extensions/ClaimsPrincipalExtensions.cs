using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BDP.Web.Api.Extensions;

/// <summary>
/// Utility extensions to the <see cref="ClaimsPrincipal"/> class
/// </summary>
public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Gets the id from a claims principal
    /// </summary>
    public static string GetUserId(this ClaimsPrincipal self)
        => GetClaimValue(self, "id");

    /// <summary>
    /// Gets the uesrname from a claims principal
    /// </summary>
    public static string GetUsername(this ClaimsPrincipal self)
        => GetClaimValue(self, ClaimTypes.NameIdentifier);

    /// <summary>
    /// Gets the email from a claims principal
    /// </summary>
    public static string GetEmail(this ClaimsPrincipal self)
        => GetClaimValue(self, ClaimTypes.Email);

    /// <summary>
    /// Gets the roles from a claims principal
    /// </summary>
    public static string GetName(this ClaimsPrincipal self)
        => GetClaimValue(self, ClaimTypes.Name);

    /// <summary>
    /// Gets the roles from a claims principal
    /// </summary>
    public static IEnumerable<string> Roles(this ClaimsPrincipal self)
        => GetClaimValue(self, ClaimTypes.Role).Split(",");

    /// <summary>
    /// Gets a claim value from a claims principal
    /// </summary>
    /// <param name="claims"></param>
    /// <param name="claimKey"></param>
    /// <returns></returns>
    /// <exception cref="SecurityTokenException"></exception>
    private static string GetClaimValue(ClaimsPrincipal claims, string claimKey)
    {
        var value = claims.Claims.FirstOrDefault(c => c.Type == claimKey)?.Value;

        if (value == null)
            throw new SecurityTokenException("invalid token");

        return value;
    }
}
