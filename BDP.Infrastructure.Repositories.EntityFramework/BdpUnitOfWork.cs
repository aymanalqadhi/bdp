using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

namespace BDP.Infrastructure.Repositories.EntityFramework;

public sealed class BdpUnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
{
    #region Fields

    private readonly BdpDbContext _ctx;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="ctx">The database context of the application</param>
    public BdpUnitOfWork(BdpDbContext ctx)
    {
        _ctx = ctx;
    }

    #endregion Public Constructors

    #region Properties

    /// <inheritdoc/>
    public IRepository<Attachment> Attachments => RepositoryFactory.Create(_ctx.Attachments);

    /// <inheritdoc/>
    public IRepository<Confirmation> Confirmations
        => new Repository<Confirmation, ConfirmationValidator>(_ctx.Confirmations);

    /// <inheritdoc/>
    public IRepository<Event> Events => new Repository<Event, EventValidator>(_ctx.Events);

    /// <inheritdoc/>
    public IRepository<EventType> EventTypes => RepositoryFactory.Create(_ctx.EventTypes);

    /// <inheritdoc/>
    public IRepository<FinancialRecord> FinancialRecords
        => new Repository<FinancialRecord, FinancialRecordValidator>(_ctx.FinancialRecords);

    /// <inheritdoc/>
    public IRepository<FinancialRecordVerification> FinancialRecordVerficiations
        => RepositoryFactory.Create(_ctx.FinancialRecordVerifications);

    /// <inheritdoc/>
    public IRepository<Log> Logs
        => new Repository<Log, LogValidator>(_ctx.Logs);

    /// <inheritdoc/>
    public IRepository<LogTag> LogTags
        => new Repository<LogTag, LogTagValidator>(_ctx.LogTags);

    /// <inheritdoc/>
    public IRepository<ProductOrder> ProductOrders
        => new Repository<ProductOrder, ProductOrderValidator>(_ctx.ProductOrders);

    /// <inheritdoc/>
    public IRepository<ProductReview> ProductReviews => RepositoryFactory.Create(_ctx.ProductReviews);

    /// <inheritdoc/>
    public IRepository<Product> Products => RepositoryFactory.Create(_ctx.Products);

    /// <inheritdoc/>
    public IRepository<RefreshToken> RefreshTokens => RepositoryFactory.Create(_ctx.RefreshTokens);

    /// <inheritdoc/>
    public IRepository<ServiceReservation> ServiceReservations => RepositoryFactory.Create(_ctx.ServiceReservations);

    /// <inheritdoc/>
    public IRepository<Service> Services => RepositoryFactory.Create(_ctx.Services);

    /// <inheritdoc/>
    public IRepository<ServiceReview> ServicesReviews => RepositoryFactory.Create(_ctx.ServiceReviews);

    /// <inheritdoc/>
    public IRepository<TransactionConfirmation> TransactionConfirmations
        => RepositoryFactory.Create(_ctx.TransactionConfirmations);

    /// <inheritdoc/>
    public IRepository<Transaction> Transactions
        => new Repository<Transaction, TransactionValidator>(_ctx.Transactions);

    /// <inheritdoc/>
    public IRepository<UserProfile> UserProfiles => RepositoryFactory.Create(_ctx.UserProfiles);

    /// <inheritdoc/>
    public IRepository<User> Users => new Repository<User, UserValidator>(_ctx.Users);

    #endregion Properties

    #region Public Methods

    /// <inheritdoc/>
    public async Task<IAsyncDatabaseTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => new AsyncDatabaseTransaction(await _ctx.Database.BeginTransactionAsync(cancellationToken));

    /// <inheritdoc/>
    public int Commit()
        => _ctx.SaveChanges();

    /// <inheritdoc/>
    public Task<int> CommitAsync(CancellationToken cancellationToken = default)
        => _ctx.SaveChangesAsync(cancellationToken);

    /// <inheritdoc/>
    public async Task<int> CommitAsync(
        IAsyncDatabaseTransaction transaction,
        CancellationToken cancellationToken = default)
    {
        var ret = await CommitAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return ret;
    }

    /// <inheritdoc/>
    public void Dispose()
        => _ctx.Dispose();

    /// <inheritdoc/>
    public ValueTask DisposeAsync()
        => _ctx.DisposeAsync();

    #endregion Public Methods
}