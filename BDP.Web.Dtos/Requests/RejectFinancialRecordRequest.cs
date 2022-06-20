using BDP.Web.Dtos.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class RejectFinancialRecordRequest
{
    /// <summary>
    /// Gets or sets the id of the record to decline
    /// </summary>
    public int RecordId { get; set; }

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
