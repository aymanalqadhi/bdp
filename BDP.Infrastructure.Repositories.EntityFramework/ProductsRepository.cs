using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="Product"/>
/// </summary>
public sealed class ProductsRepository :
    LegacyRepository<Product, SellableValidator<Product>>, IProductsRepository
{
    public ProductsRepository(DbSet<Product> set) : base(set)
    {
    }
}