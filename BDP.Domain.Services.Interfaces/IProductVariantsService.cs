using BDP.Domain.Entities;
using BDP.Domain.Repositories;

namespace BDP.Domain.Services;

/// <summary>
/// An interface to be implemented by product variants services
/// </summary>
public interface IProductVariantsService
{
    /// <summary>
    /// Gets product variants
    /// </summary>
    /// <returns></returns>
    IQueryBuilder<ProductVariant> GetVariants();
}