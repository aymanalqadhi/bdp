namespace BDP.Domain.Services.Exceptions;

public sealed class AlreadyUsedEmailException : Exception
{
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="email">The email the user entered</param>
    public AlreadyUsedEmailException(string email) : base($"email {email} is already used")
    {
    }
}