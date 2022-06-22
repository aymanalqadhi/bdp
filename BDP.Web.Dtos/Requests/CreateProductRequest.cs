using BDP.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class CreateProductRequest
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
    /// Gets or sets the available quantity of the product
    /// </summary>
    public IEnumerable<EntityKey<Category>> Categories { get; set; }
        = new List<EntityKey<Category>>();
}