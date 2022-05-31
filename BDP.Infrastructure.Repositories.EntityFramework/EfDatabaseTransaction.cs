using BDP.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace BDP.Infrastructure.Repositories.EntityFramework;

public class EfDatabaseTransaction : IAsyncDatabaseTransaction
{
    private readonly IDbContextTransaction _tx;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="tx">Entity framework transaction object</param>
    public EfDatabaseTransaction(IDbContextTransaction tx)
    {
        _tx = tx;
    }

    /// <inheritdoc/>
    public Task CommitAsync(CancellationToken cancellationToken = default)
        => _tx.CommitAsync(cancellationToken);

    /// <inheritdoc/>
    public Task RollbackAsync(CancellationToken cancellationToken = default)
        => _tx.RollbackAsync(cancellationToken);

    /// <inheritdoc/>
    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return _tx.DisposeAsync();
    }
}