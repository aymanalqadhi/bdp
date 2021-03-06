using BDP.Web.Dtos.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class FinishProfileRequest
{
    /// <summary>
    /// Gets or sets the full name sent in the request
    /// </summary>
    [Required]
    public string FullName { get; set; } = null!;

    /// <summary>
    /// Gets or set the account type sent in the request
    /// </summary>
    [Required]
    public string AccountType { get; set; } = null!;

    /// <summary>
    /// Gets or sets the account profile picture
    /// </summary>
    [MaxFileSize(1024 * 1024 * 8)]
    [AllowedExtensions(".jpg", ".png", ".jpeg")]
    public IFormFile? ProfilePicture { get; set; }
}