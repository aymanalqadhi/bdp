using BDP.Web.Dtos.Attributes;
using Microsoft.AspNetCore.Http;

namespace BDP.Web.Dtos.Requests;

public class RejectFinancialRecordRequest
{
    /// <summary>
    /// Gets or sets optional notes to be attached to the declination
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Gets or sets optional file to be attached to the declination
    /// </summary>
    [MaxFileSize(1024 * 1024 * 8)]
    [AllowedExtensions(".jpg", ".png", ".jpeg", ".pdf")]
    public IFormFile? Document { get; set; }
}