using BDP.Domain.Entities;

namespace BDP.Domain.Repositories;

/// <summary>
/// An interface to represent the functionality of a repository for the
/// <see cref="RefreshToken"/> entity
/// </summary>
public interface IRefreshTokensRepository : IRepository<RefreshToken>
{
}