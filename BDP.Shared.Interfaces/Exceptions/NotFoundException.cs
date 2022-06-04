namespace BDP.Domain.Repositories.Exceptions;

public sealed class NotFoundException : Exception
{
    #region Fields

    private readonly Exception? _innerExcpetion;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="message">A message that clarifies the error cause</param>
    public NotFoundException(string message, Exception? innerException = null)
        : base(message)
    {
        _innerExcpetion = innerException;
    }

    #endregion Public Constructors

    #region Properties

    /// <summary>
    /// Gets the inner exception
    /// </summary>
    public Exception? InnerExcpetion => _innerExcpetion;

    #endregion Properties
}