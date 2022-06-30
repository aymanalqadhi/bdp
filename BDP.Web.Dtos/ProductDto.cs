using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A data-transfer object for <see cref="Product"/>
/// </summary>
public sealed class ProductDto : MutableEntityDto<Product>
{
    /// <summary>
    /// Gets or sets the title of the sellable
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the sellable
    /// </summary
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who listed this sellable
    /// </summary>
    public UserDto OwnedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets whether the sellable is available
    /// </summary>
    public bool IsAvailable { get; set; } = true;

    /// <summary>
    /// Gets or sets the collection of categories the product belongs to
    /// </summary>
    public IEnumerable<string> Categories { get; set; } = new List<string>();
}