using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class SignUpRequest
{
    /// <summary>
    /// Gets or sets the username of the register request
    /// </summary>
    [Required]
    [RegularExpression(@"^[a-z]\w{3,29}$")]
    public string Username { get; set; } = null!;

    /// <summary>
    /// Gets or sets the email of the register request
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    /// <summary>
    /// Gets or sets the password of the register request (plain-text)
    /// </summary>
    [Required]
    public string Password { get; set; } = null!;
}