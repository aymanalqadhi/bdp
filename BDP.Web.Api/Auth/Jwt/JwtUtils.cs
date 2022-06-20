using BDP.Domain.Entities;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BDP.Web.Api.Auth.Jwt;

public static class JwtUtils
{
    /// <summary>
    /// Generates an access token
    /// </summary>
    /// <param name="user">The user to generate the token for</param>
    /// <param name="groups">The groups the user is enrolled in</param>
    /// <param name="settings">Jwt settings</param>
    /// <returns>The generated access token</returns>
    public static string GenerateAccessToekn(User user, IEnumerable<UserGroup> groups, JwtSettings settings)
    {
        var claims = new List<Claim>()
        {
            new Claim(CustomClaims.Id, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Name, user.FullName ?? ""),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, string.Join(",",  groups.Select(g => g.Name))),
        };

        return GenerateToken(
            settings.AccessTokenSecret,
            settings.Issuer,
            settings.Audience,
            settings.AccessTokenExpirationMinutes,
            claims
         );
    }

    /// <summary>
    /// Generates a refresh token
    /// </summary>
    /// <param name="settings">Jwt settings</param>
    /// <returns>The generated refresh token</returns>
    public static string GenerateRefereshToken(JwtSettings settings)
        => GenerateToken(
            settings.RefreshTokenSecret,
            settings.Issuer,
            settings.Audience,
            settings.RefreshTokenExpirationMinutes);

    /// <summary>
    /// Validates a token
    /// </summary>
    /// <param name="token">The token to validate</param>
    /// <param name="issuer">The issuer of the token</param>
    /// <param name="audience">The audience of the token</param>
    /// <param name="key">The key to use for validation</param>
    /// <param name="validateLifeTime">If true, validate lifetime</param>
    /// <returns></returns>
    public static bool ValidateToken(
        string token, string issuer, string audience, string key, bool validateLifeTime = true)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = validateLifeTime,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidIssuer = issuer,
            ValidAudience = audience,
            ClockSkew = TimeSpan.Zero
        };

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        try
        {
            jwtSecurityTokenHandler.ValidateToken(
                token,
                validationParameters,
                out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Validates a refresh token
    /// </summary>
    /// <param name="token">The token to validate</param>
    /// <param name="settings">Jwt settings</param>
    /// <returns>True if the validation succeeds, false otherwise</returns>
    public static bool ValidateRefreshToken(string token, JwtSettings settings)
        => ValidateToken(token, settings.Issuer, settings.Audience, settings.RefreshTokenSecret);

    /// <summary>
    /// Gets principal from expired tokens
    /// </summary>
    /// <param name="token"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    /// <exception cref="SecurityTokenException"></exception>
    public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token, JwtSettings settings)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = settings.Issuer,
            ValidAudience = settings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.AccessTokenSecret)),
            ValidateLifetime = false,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(
            token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("invalid token");

        return principal;
    }

    /// <summary>
    /// Generates a JWT token
    /// </summary>
    /// <param name="secretKey">The secret key used for signing</param>
    /// <param name="issuer">The issuer of the token</param>
    /// <param name="audience">The audience of the token</param>
    /// <param name="expires">The expirey period in mintues</param>
    /// <param name="claims">Additional claims</param>
    /// <returns>The genreated token</returns>
    private static string GenerateToken(
        string secretKey,
        string issuer,
        string audience,
        double expires,
        IEnumerable<Claim>? claims = null)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(expires),
            credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}