using BDP.Domain.Entities;

using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class CreateCategoryRequest
{
    /// <summary>
    /// Gets or sets the name of the cateogry
    /// </summary>
    [Required]
    public string Name { get; set; } = null!;

    /// <summary>
    /// An optional parent id
    /// </summary>
    public EntityKey<Category>? ParentId { get; set; }
}