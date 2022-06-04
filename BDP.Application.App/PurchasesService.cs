using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services;
using System.Linq.Expressions;

namespace BDP.Application.App;

public class PurchasesService : IPurchasesService
{
    #region Private fields

    private readonly IUnitOfWork _uow;

    #endregion Private fields

    #region Ctors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the app</param>
    public PurchasesService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion Ctors

    #region Public method

    /// <inheritdoc/>
    public Task<Purchase> GetById(long id)
        => _uow.Purchases.Query().FindAsync(id);

    /// <inheritdoc/>
    public IQueryBuilder<Purchase> ForUserAsync(User user)
    {
        return _uow.Purchases.Query()
            .Where(p => p.Transaction.From.Id == user.Id || p.Transaction.To.Id == user.Id);
    }

    #endregion Public method
}