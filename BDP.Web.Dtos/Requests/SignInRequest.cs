using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class SignInRequest
{
    /// <summary>
    /// Gets or sets the username of the login request
    /// </summary>
    [Required]
    [RegularExpression(@"^[a-z]\w{3,29}$")]
    public string Username { get; set; } = null!;

    /// <summary>
    /// Gets or sets the password of the login request (plain-text)
    /// </summary>
    [Required]
    public string Password { get; set; } = null!;

    /// <summary>
    /// Gets or sets the device unique identifier
    /// </summary>
    [Required]
    public string UniqueIdentifier { get; set; } = null!;

    /// <summary>
    /// Gets or sets the device name of the device that uses the token
    /// </summary>
    public string? DeviceName { get; set; }

    /// <summary>
    /// Gets or sets the hostname of the device that uses the token
    /// </summary>
    public string? HostName { get; set; }

    /// <summary>
    /// Gets the name of the operating system
    /// </summary>
    public string? OperatingSystemName { get; set; }
}