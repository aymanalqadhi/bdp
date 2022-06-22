namespace BDP.Domain.Services.Exceptions;

public class InvalidPriceException : Exception
{
    #region Fields

    private readonly decimal _price;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="price">The price that was deemed invalid</param>
    public InvalidPriceException(decimal price) : base($"invalid price: {price}")
    {
        _price = price;
    }

    #endregion Public Constructors

    #region Properties

    /// <summary>
    /// Gets the price associated with this exception
    /// </summary>
    public decimal Price => _price;

    #endregion Properties

    #region Public Methods

    /// <summary>
    /// A static method to validate price values
    /// </summary>
    /// <param name="price">The price to validate</param>
    /// <exception cref="InvalidPriceException"></exception>
    public static void ValidatePrice(decimal price)
    {
        if (price <= 0 || price > 1_000_000)
            throw new InvalidPriceException(price);
    }

    #endregion Public Methods
}