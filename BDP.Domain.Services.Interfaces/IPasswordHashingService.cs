namespace BDP.Domain.Services;

public interface IPasswordHashingService
{
    /// <summary>
    /// Hashes a password using a password-hashing algorithm (e.g., argn2, bcrypt, ...)
    /// </summary>
    /// <param name="password">The password to hash</param>
    /// <returns>The generated hash</returns>
    string Hash(string password);

    /// <summary>
    /// Verifies that a password matches the passed hash
    /// </summary>
    /// <param name="password">The password to verify</param>
    /// <param name="hash">The hash to verify against</param>
    /// <returns></returns>
    bool Verify(string password, string hash);
}