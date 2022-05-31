using BDP.Web.Dtos.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class CreateServiceRequest
{
    /// <summary>
    /// Gets or sets the title of the product
    /// </summary>
    [Required]
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the product
    /// </summary>
    [Required]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the price of the product
    /// </summary>
    [Required]
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the availability begining time of the service
    /// (to be changed with <see cref="TimeOnly"/>)
    /// </summary>
    [Required]
    public DateTime AvailableBegin { get; set; }

    /// <summary>
    /// Gets or sets the availability begining time of the service
    /// (to be changed with <see cref="TimeOnly"/>)
    /// </summary>
    [Required]
    public DateTime AvailableEnd { get; set; }

    /// <summary>
    /// Gets or sets the collection of images associated with the product
    /// </summary>
    [MaxFileSize(1024 * 1024 * 8)]
    [AllowedExtensions(".jpg", ".png", ".jpeg")]
    public IList<IFormFile>? Attachments { get; set; }
}