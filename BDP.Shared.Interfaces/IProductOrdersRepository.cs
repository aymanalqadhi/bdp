using BDP.Domain.Entities;

namespace BDP.Domain.Repositories;

/// <summary>
/// An interface to represent the functionality of a repository for the
/// <see cref="ProductOrder"/> entity
/// </summary>
public interface IProductOrdersRepository : ILegacyRepository<ProductOrder>
{
}