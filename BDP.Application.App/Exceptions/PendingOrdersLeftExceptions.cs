using BDP.Domain.Entities;

namespace BDP.Application.App.Exceptions;

public sealed class PendingOrdersLeftException : Exception
{
    private readonly Product _product;

    /// <summary>
    /// Default constructor
    /// </summary>
    public PendingOrdersLeftException(Product product)
        : base($"product #{product.Id} still has pending orders")
    {
        _product = product;
    }

    /// <summary>
    /// Gets the product associated with this exception
    /// </summary>
    public Product Product => _product;
}