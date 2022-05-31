namespace BDP.Application.App.Exceptions;

public sealed class InvalidRangeException : Exception
{
    #region Private fields

    private readonly double _value;
    private readonly double _min;
    private readonly double _max;

    #endregion

    #region Ctors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="value">The value</param>
    /// <param name="min">the minimum value</param>
    /// <param name="max">The maximum value</param>
    public InvalidRangeException(double value, double min, double max)
        : base($"invalid range value `{value}' (allowed range: {min}-{max}")
    {
        _value = value;
        _min = min;
        _max = max;
    }

    #endregion

    #region Public properties

    /// <summary>
    /// Gets the errnous value
    /// </summary>
    public double Progress => _value;

    /// <summary>
    /// Gets the minimum boundary value
    /// </summary>
    public double Min => _min;

    /// <summary>
    /// Gets the maximum boundary value
    /// </summary>
    public double Max => _max;

    #endregion
}
