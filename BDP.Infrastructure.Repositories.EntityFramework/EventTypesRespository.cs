using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="EventType"/>
/// </summary>
public sealed class EventTypesRepository :
    EfRepository<EventType, Validator<EventType>>, IEventTypesRepository
{
    public EventTypesRepository(DbSet<EventType> set) : base(set)
    {
    }
}