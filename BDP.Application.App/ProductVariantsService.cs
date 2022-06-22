using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services;

namespace BDP.Application.App;

/// <summary>
/// A service to manage product variants
/// </summary>
public sealed class ProductVariantsService : IProductVariantsService
{
    private readonly IUnitOfWork _uow;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the application</param>
    public ProductVariantsService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    /// <inheritdoc/>
    public IQueryBuilder<ProductVariant> GetVariants()
        => _uow.ProductVariants.Query();
}