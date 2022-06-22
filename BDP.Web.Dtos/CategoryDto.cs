using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A dto for <see cref="Category"/>
/// </summary>
public sealed class CategoryDto : EntityDto<Category>
{
    /// <summary>
    /// Gets or sets the name of the category
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the sub-categories of the category
    /// </summary>
    public IEnumerable<CategoryDto> Children { get; set; } = new List<CategoryDto>();
}