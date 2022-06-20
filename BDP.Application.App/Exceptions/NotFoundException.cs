namespace BDP.Application.App.Exceptions;

public class NotFoundException : Exception
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public NotFoundException() : base()
    {
    }

    /// <summary>
    /// A constructor with a message parameter
    /// </summary>
    /// <param name="message">The message of the exception</param>
    public NotFoundException(string message) : base(message)
    {
    }
}
