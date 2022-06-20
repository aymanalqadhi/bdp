using BDP.Domain.Entities;

namespace BDP.Domain.Repositories;

/// <summary>
/// An interface to represent the functionality of a repository for the
/// <see cref="Confirmation"/> entity
/// </summary>
public interface IConfirmationsRepository : ILegacyRepository<Confirmation>
{
}