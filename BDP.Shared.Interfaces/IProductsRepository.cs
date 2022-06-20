using BDP.Domain.Entities;

namespace BDP.Domain.Repositories;

/// <summary>
/// An interface to represent the functionality of a repository for the
/// <see cref="Product"/> entity
/// </summary>
public interface IProductsRepository : IRepository<Product>
{
}