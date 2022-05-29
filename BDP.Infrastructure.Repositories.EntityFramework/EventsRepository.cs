using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="Event"/>
/// </summary>
public sealed class EventsRepository :
    EfRepository<Event, Validator<Event>>, IEventsRepository
{
    public EventsRepository(DbSet<Event> set) : base(set)
    {
    }
}
