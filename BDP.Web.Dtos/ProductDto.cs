namespace BDP.Web.Dtos;

public class ProductDto : SellableDto
{
    /// <summary>
    /// Gets or sets the available quantity of the product
    /// </summary>
    public uint Quantity { get; set; }
}
