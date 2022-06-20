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
    public IAsyncEnumerable<Sellable> GetForAsync(User user, int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0 || pageSize > 1000)
            throw new InvalidPaginationParametersException(page, pageSize);

        return _uow.Sellables
            .Query()
            .Where(s => s.OfferedBy.Id == user.Id)
            .OrderByDescending(s => s.Id)
            .Include(s => s.Attachments)
            .Page(page, pageSize)
            .AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<Sellable> SearchForAsync(User user, string query, int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0 || pageSize > 1000)
            throw new InvalidPaginationParametersException(page, pageSize);

        return _uow.Sellables
            .Query()
            .Where(s => s.OfferedBy.Id == user.Id && s.Title.ToLower().Contains(query.ToLower()))
            .OrderByDescending(s => s.Id)
            .Include(s => s.Attachments)
            .Page(page, pageSize)
            .AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<Sellable> SearchAsync(string query, int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0 || pageSize > 1000)
            throw new InvalidPaginationParametersException(page, pageSize);

        return _uow.Sellables
            .Query()
            .Where(s => s.Title.ToLower().Contains(query.ToLower()))
            .OrderByDescending(s => s.Id)
            .Include(s => s.Attachments)
            .Include(s => s.OfferedBy)
            .Page(page, pageSize)
            .AsAsyncEnumerable();
    }

    #endregion Public methods
}