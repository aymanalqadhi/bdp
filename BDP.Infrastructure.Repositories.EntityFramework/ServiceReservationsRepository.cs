using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="ServiceReservation"/>
/// </summary>
public sealed class ServiceReservationsRepository :
    LegacyRepository<ServiceReservation, Validator<ServiceReservation>>, IServiceReservationsRepository
{
    public ServiceReservationsRepository(DbSet<ServiceReservation> set) : base(set)
    {
    }
}