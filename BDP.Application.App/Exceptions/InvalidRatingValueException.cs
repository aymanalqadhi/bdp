namespace BDP.Application.App.Exceptions;

public class InvalidRatingValueException : Exception
{
    #region Private fields

    private readonly double _value;

    #endregion Private fields

    #region Ctors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="value">The value of the rating</param>
    public InvalidRatingValueException(double value) : base($"invalid rating vlaue {value}")
        => _value = value;

    #endregion Ctors

    #region Public properties

    /// <summary>
    /// Gets the rating value
    /// </summary>
    public double Value => _value;

    #endregion Public properties
}