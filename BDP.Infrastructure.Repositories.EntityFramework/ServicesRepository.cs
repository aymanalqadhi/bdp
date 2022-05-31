using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="Service"/>
/// </summary>
public sealed class ServicesRepository :
    LegacyRepository<Service, SellableValidator<Service>>, IServicesRepository
{
    public ServicesRepository(DbSet<Service> set) : base(set)
    {
    }
}