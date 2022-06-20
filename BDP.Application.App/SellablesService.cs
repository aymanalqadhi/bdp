using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;

using System.Linq.Expressions;

namespace BDP.Application.App;

public class SellablesService : ISellablesService
{
    #region Private fields

    private readonly IUnitOfWork _uow;

    #endregion Private fields

    #region Ctors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the app</param>
    public SellablesService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion Ctors

    #region Public methods

    /// <inheritdoc/>
    public Task<Sellable> GetByIdAsync(EntityKey<Sellable> id)
        => _uow.Sellables.Query().FindAsync(id);

    /// <inheritdoc/>
    public IQueryBuilder<Sellable> GetForAsync(EntityKey<User> userId)
        => _uow.Sellables.Query().Where(s => s.OfferedBy.Id == userId);

    /// <inheritdoc/>
    public IQueryBuilder<Sellable> SearchForAsync(EntityKey<User> userId, string query)
        => _uow.Sellables.Query().Where(s => s.OfferedBy.Id == userId && s.Title.ToLower().Contains(query.ToLower()));

    /// <inheritdoc/>
    public IQueryBuilder<Sellable> SearchAsync(string query)
        => _uow.Sellables.Query().Where(s => s.Title.ToLower().Contains(query.ToLower()));

    #endregion Public methods
}