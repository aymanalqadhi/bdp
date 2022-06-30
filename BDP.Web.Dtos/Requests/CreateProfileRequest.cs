using BDP.Web.Dtos.Attributes;

using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class CreateProfileRequest
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
    public string ProfileType { get; set; } = null!;

    /// <summary>
    /// Gets or sets the profile profile picture
    /// </summary>
    [MaxFileSize(1024 * 1024 * 8)]
    [AllowedExtensions(".jpg", ".png", ".jpeg")]
    public IFormFile? ProfilePicture { get; set; }

    /// <summary>
    /// Gets or sets the profile cover picture
    /// </summary>
    [MaxFileSize(1024 * 1024 * 8)]
    [AllowedExtensions(".jpg", ".png", ".jpeg")]
    public IFormFile? CoverPicture { get; set; }
}