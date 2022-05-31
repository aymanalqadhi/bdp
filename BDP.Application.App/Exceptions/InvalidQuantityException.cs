using BDP.Domain.Entities;

namespace BDP.Application.App.Exceptions;

public class InvalidQuantityException : Exception
{
    private readonly uint _quantity;

    public InvalidQuantityException(uint quantity)
        : base($"invalid quantity: {quantity}")
    {
        _quantity = quantity;
    }

    /// <summary>
    /// Gets the product associated with this exception
    /// </summary>
    public uint Quantity => _quantity;
}
