namespace BDP.Web.Dtos.Requests;

public sealed class UpdateProductVariantRequest
{
    /// <summary>
    /// Gets or sets the name of the variant
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the variant
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the price of the variant
    /// </summary>
    public decimal Price { get; set; }
}