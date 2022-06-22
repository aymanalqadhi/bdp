using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class UpdateCategoryRequest
{
    /// <summary>
    /// Gets or sets the name of the cateogry
    /// </summary>
    [Required]
    public string Name { get; set; } = null!;
}