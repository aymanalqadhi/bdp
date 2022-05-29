using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class RefreshTokenRequest
{
    /// <summary>
    /// Gets or sets the expired access token
    /// </summary>
    [Required]
    public string AccessToken { get; set; } = null!;

    /// <summary>
    /// Gets or sets the refresh token
    /// </summary>
    [Required]
    public string RefreshToken { get; set; } = null!;

    /// <summary>
    /// Gets or sets the unique device identifier
    /// </summary>
    [Required]
    public string UniqueIdentifier { get; set; } = null!;
}