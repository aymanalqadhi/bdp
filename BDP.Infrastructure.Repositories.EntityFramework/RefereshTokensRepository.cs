using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="RefreshToken"/>
/// </summary>
public sealed class RefreshTokensRepository :
    EfRepository<RefreshToken, Validator<RefreshToken>>, IRefreshTokensRepository
{
    public RefreshTokensRepository(DbSet<RefreshToken> set) : base(set)
    {
    }
}
