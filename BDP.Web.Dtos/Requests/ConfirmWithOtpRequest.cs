using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class ConfirmWithOtpRequest
{
    /// <summary>
    /// Gets or sets the one-time password filed of the request
    /// </summary>
    [Required]
    [Display(Name = "One-time Password")]
    public string Otp { get; set; } = null!;
}
