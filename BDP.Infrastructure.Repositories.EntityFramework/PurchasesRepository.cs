using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="Purchase"/>
/// </summary>
public sealed class PurchasesRepository :
    LegacyRepository<Purchase, Validator<Purchase>>, IPurchasesRepository
{
    public PurchasesRepository(DbSet<Purchase> set) : base(set)
    {
    }
}