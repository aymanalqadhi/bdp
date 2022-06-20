using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="ProductOrder"/>
/// </summary>
public sealed class ProductOrdersRepository :
    EfRepository<ProductOrder, ProductOrderValidator>, IProductOrdersRepository
{
    public ProductOrdersRepository(DbSet<ProductOrder> set) : base(set)
    {
    }
}
