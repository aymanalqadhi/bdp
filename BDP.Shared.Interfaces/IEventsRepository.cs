using BDP.Domain.Entities;

namespace BDP.Domain.Repositories;

/// <summary>
/// An interface to represent the functionality of a repository for the 
/// <see cref="Event"/> entity
/// </summary>
public interface IEventsRepository : IRepository<Event>
{
}
