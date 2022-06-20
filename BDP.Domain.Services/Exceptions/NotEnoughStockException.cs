using BDP.Domain.Entities;

namespace BDP.Domain.Services.Exceptions;

public class NotEnoughStockException : Exception
{
    private readonly Product _product;
    private readonly uint _requestedQuantity;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="product">The product lacking stock</param>
    /// <param name="requestedQuantity">The requested quantity</param>
    public NotEnoughStockException(Product product, uint requestedQuantity)
        : base($"product #{product.Id} does not have {requestedQuantity} stock left")
    {
        _product = product;
        _requestedQuantity = requestedQuantity;
    }

    /// <summary>
    /// Gets the product associated with this exception
    /// </summary>
    public Product Product => _product;

    /// <summary>
    /// Gets the requested quantity
    /// </summary>
    public uint RequestedQuantity => _requestedQuantity;
}
