using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="Confirmation"/>
/// </summary>
public sealed class ConfirmationsRepository :
    LegacyRepository<Confirmation, ConfirmationValidator>, IConfirmationsRepository
{
    public ConfirmationsRepository(DbSet<Confirmation> set) : base(set)
    {
    }
}