using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services.Exceptions;
using BDP.Domain.Services.Interfaces;

using System.Linq.Expressions;

namespace BDP.Domain.Services;

public class PurchasesService : IPurchasesService
{
    #region Private fields

    private readonly IUnitOfWork _uow;

    #endregion
    
    #region Ctors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the app</param>
    public PurchasesService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion

    #region Public method

    /// <inheritdoc/>
    public async Task<Purchase> GetById(long id)
    {
        var purchase = await _uow.Purchases.FirstOrDefaultAsync(p => p.Id == id);

        if (purchase is null)
            throw new NotFoundException($"no purchases were found with id #{id}");

        return purchase;
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<Purchase> ForUserAsync(
        int page,
        int pageSize,
        User user,
        Expression<Func<Purchase, object>>[]? includes = null)
    {
        if (page <= 0 || pageSize <= 0 || pageSize > 1000)
            throw new InvalidPaginationParametersException(page, pageSize);

        return _uow.Purchases.FilterAsync(
            page,
            pageSize,
            p => p.Transaction.From.Id == user.Id || p.Transaction.To.Id == user.Id,
            includes: includes,
            descOrder: true
        );
    }

    #endregion
}