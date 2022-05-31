namespace BDP.Web.Api.Auth.Jwt;

public class JwtSettings
{
    /// <summary>
    /// Gets or sets the access token secret string
    /// </summary>
    public string AccessTokenSecret { get; set; } = null!;

    /// <summary>
    /// Gets or sets the refresh token secret string
    /// </summary>
    public string RefreshTokenSecret { get; set; } = null!;

    /// <summary>
    /// Gets or sets the access token expiration mintues
    /// </summary>
    public double AccessTokenExpirationMinutes { get; set; } = 5;

    /// <summary>
    /// Gets or sets the refresh token expiration minutes
    /// </summary>
    public double RefreshTokenExpirationMinutes { get; set; } = 60 * 24 * 3;

    /// <summary>
    /// Gets or sets the token's issuer
    /// </summary>
    public string Issuer { get; set; } = null!;

    /// <summary>
    /// Gets or sets the token's audience
    /// </summary>
    public string Audience { get; set; } = null!;
}