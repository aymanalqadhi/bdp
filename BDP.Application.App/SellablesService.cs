using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services;

using System.Linq.Expressions;

namespace BDP.Application.App;

public class SellablesService : ISellablesService
{
    #region Private fields

    private readonly ILegacyUnitOfWork _uow;

    #endregion Private fields

    #region Ctors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the app</param>
    public SellablesService(ILegacyUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion Ctors

    #region Public methods

    /// <inheritdoc/>
    public async Task<Sellable> GetByIdAsync(long id)
    {
        var ret = await _uow.Sellables.FirstOrDefaultAsync(s => s.Id == id);

        if (ret is null)
            throw new NotFoundException($"no sellables were found with id #{id}");

        return ret;
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<Sellable> GetForAsync(User user, int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0 || pageSize > 1000)
            throw new InvalidPaginationParametersException(page, pageSize);

        return _uow.Sellables.FilterAsync(
            page, pageSize, s => s.OfferedBy.Id == user.Id,
            includes: new Expression<Func<Sellable, object>>[] { s => s.Attachments },
            descOrder: true
        );
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<Sellable> SearchForAsync(User user, string query, int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0 || pageSize > 1000)
            throw new InvalidPaginationParametersException(page, pageSize);

        return _uow.Sellables.FilterAsync(
            page, pageSize,
            s => s.OfferedBy.Id == user.Id && s.Title.ToLower().Contains(query.ToLower()),
            includes: new Expression<Func<Sellable, object>>[] { s => s.Attachments },
            descOrder: true
        );
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<Sellable> SearchAsync(string query, int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0 || pageSize > 1000)
            throw new InvalidPaginationParametersException(page, pageSize);

        return _uow.Sellables.FilterAsync(
            page, pageSize,
            s => s.Title.ToLower().Contains(query.ToLower()),
            includes: new Expression<Func<Sellable, object>>[]
            {
                s => s.Attachments,
                s => s.OfferedBy
            },
            descOrder: true
        );
    }

    #endregion Public methods
}