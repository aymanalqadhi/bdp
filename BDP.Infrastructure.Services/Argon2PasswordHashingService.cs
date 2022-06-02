using BDP.Domain.Services;
using Isopoh.Cryptography.Argon2;

namespace BDP.Infrastructure.Services;

public sealed class Argon2PasswordHashingService : IPasswordHashingService
{
    /// <inheritdoc/>
    public string Hash(string password)
        => Argon2.Hash(password);

    /// <inheritdoc/>
    public bool Verify(string password, string hash)
        => Argon2.Verify(hash, password);
}