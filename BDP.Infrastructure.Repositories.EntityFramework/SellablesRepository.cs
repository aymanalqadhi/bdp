using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="Sellable"/>
/// </summary>
public sealed class SellablesRepository :
    EfRepository<Sellable, SellableValidator>, ISellablesRepository
{
    public SellablesRepository(DbSet<Sellable> set) : base(set)
    {
    }
}
