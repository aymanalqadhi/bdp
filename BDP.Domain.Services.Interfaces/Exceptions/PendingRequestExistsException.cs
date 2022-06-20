namespace BDP.Domain.Services.Exceptions;

public class PendingRequestExistsException : Exception
{
    public PendingRequestExistsException(string message)
        : base(message)
    {
    }
}