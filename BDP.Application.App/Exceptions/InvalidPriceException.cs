namespace BDP.Application.App.Exceptions;

public class InvalidPriceException : Exception
{
    private readonly decimal _price;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="price">The price that was deemed invalid</param>
    public InvalidPriceException(decimal price) : base($"invalid price: {price}")
    {
        _price = price;
    }

    /// <summary>
    /// Gets the price associated with this exception
    /// </summary>
    public decimal Price => _price;
}