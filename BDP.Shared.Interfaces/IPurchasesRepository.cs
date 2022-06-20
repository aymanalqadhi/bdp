using BDP.Domain.Entities;

namespace BDP.Domain.Repositories;

/// <summary>
/// An interface to represent the functionality of a repository for the
/// <see cref="Purchase"/> entity
/// </summary>
public interface IPurchasesRepository : ILegacyRepository<Purchase>
{
}