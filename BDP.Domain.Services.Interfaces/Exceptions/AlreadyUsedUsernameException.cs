namespace BDP.Domain.Services.Exceptions;

public sealed class AlreadyUsedUsernameException : Exception
{
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="username">The username the user entered</param>
    public AlreadyUsedUsernameException(string username) : base($"username {username} is already used")
    {
    }
}