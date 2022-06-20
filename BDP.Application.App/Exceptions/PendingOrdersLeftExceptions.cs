using BDP.Domain.Entities;

namespace BDP.Application.App.Exceptions;

public sealed class PendingOrdersLeftException : Exception
{
    private readonly Guid _productId;

    /// <summary>
    /// Default constructor
    /// </summary>
    public PendingOrdersLeftException(Guid productId)
        : base($"product #{productId} still has pending orders")
    {
        _productId = productId;
    }

    /// <summary>
    /// Gets the id of the product associated with this exception
    /// </summary>
    public Guid ProductId => _productId;
}