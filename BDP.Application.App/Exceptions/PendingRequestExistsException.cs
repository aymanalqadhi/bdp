namespace BDP.Application.App.Exceptions;

public class PendingRequestExistsException : Exception
{
    public PendingRequestExistsException(string message)
        : base(message)
    {
    }
}