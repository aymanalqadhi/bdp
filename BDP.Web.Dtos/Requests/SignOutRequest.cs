using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class SignOutRequest
{
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