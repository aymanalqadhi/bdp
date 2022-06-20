using BDP.Domain.Entities;

namespace BDP.Application.App.Exceptions;

public sealed class PendingOrdersLeftException : Exception
{
    private readonly EntityKey<Product> _productId;

    /// <summary>
    /// Default constructor
    /// </summary>
    public PendingOrdersLeftException(EntityKey<Product> productId)
        : base($"product #{productId} still has pending orders")
    {
        _productId = productId;
    }

    /// <summary>
    /// Gets the id of the product associated with this exception
    /// </summary>
    public EntityKey<Product> ProductId => _productId;
}