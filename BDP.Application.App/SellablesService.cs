using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
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
    public Task<Sellable> GetByIdAsync(long id)
        => _uow.Sellables.Query().FindAsync(id);

    /// <inheritdoc/>
    public IQueryBuilder<Sellable> GetForAsync(User user)
        => _uow.Sellables.Query().Where(s => s.OfferedBy.Id == user.Id);

    /// <inheritdoc/>
    public IQueryBuilder<Sellable> SearchForAsync(User user, string query)
        => _uow.Sellables.Query().Where(s => s.OfferedBy.Id == user.Id && s.Title.ToLower().Contains(query.ToLower()));

    /// <inheritdoc/>
    public IQueryBuilder<Sellable> SearchAsync(string query)
        => _uow.Sellables.Query().Where(s => s.Title.ToLower().Contains(query.ToLower()));

    #endregion Public methods
}