using BDP.Web.Dtos.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class AddImageToEventRequest
{
    /// <summary>
    /// Gets or sets the image to be added
    /// </summary>
    [Required]
    [MaxFileSize(1024 * 1024 * 8)]
    [AllowedExtensions(".jpg", ".png", ".jpeg")]
    public IFormFile Image { get; set; } = null!;
}