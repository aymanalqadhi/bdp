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
    public IRepository<Attachment> Attachments
        => new Repository<Attachment, Validator<Attachment>>(_ctx.Attachments);

    /// <inheritdoc/>
    public IRepository<Confirmation> Confirmations
        => new Repository<Confirmation, ConfirmationValidator>(_ctx.Confirmations);

    /// <inheritdoc/>
    public IRepository<Event> Events => new Repository<Event, EventValidator>(_ctx.Events);

    /// <inheritdoc/>
    public IRepository<EventType> EventTypes => new Repository<EventType, Validator<EventType>>(_ctx.EventTypes);

    /// <inheritdoc/>
    public IRepository<FinancialRecord> FinancialRecords
        => new Repository<FinancialRecord, FinancialRecordValidator>(_ctx.FinancialRecords);

    /// <inheritdoc/>
    public IRepository<FinancialRecordVerification> FinancialRecordVerficiations
        => new Repository<FinancialRecordVerification, Validator<FinancialRecordVerification>>(_ctx.FinancialRecordVerifications);

    /// <inheritdoc/>
    public IRepository<PhoneNumber> PhoneNumbers
        => new Repository<PhoneNumber, Validator<PhoneNumber>>(_ctx.PhoneNumbers);

    /// <inheritdoc/>
    public IRepository<ProductOrder> ProductOrders
        => new Repository<ProductOrder, ProductOrderValidator>(_ctx.ProductOrders);

    /// <inheritdoc/>
    public IRepository<Product> Products
        => new Repository<Product, Validator<Product>>(_ctx.Products);

    /// <inheritdoc/>
    public IRepository<Purchase> Purchases
        => new Repository<Purchase, Validator<Purchase>>(_ctx.Purchases);

    /// <inheritdoc/>
    public IRepository<RefreshToken> RefreshTokens
        => new Repository<RefreshToken, Validator<RefreshToken>>(_ctx.RefreshTokens);

    /// <inheritdoc/>
    public IRepository<SellableReview> SellableReviews
        => new Repository<SellableReview, SellableReviewValidator>(_ctx.SellableReviews);

    /// <inheritdoc/>
    public IRepository<Sellable> Sellables
        => new Repository<Sellable, SellableValidator>(_ctx.Sellables);

    /// <inheritdoc/>
    public IRepository<ServiceReservation> ServiceReservations
        => new Repository<ServiceReservation, Validator<ServiceReservation>>(_ctx.ServiceReservations);

    /// <inheritdoc/>
    public IRepository<Service> Services
        => new Repository<Service, Validator<Service>>(_ctx.Services);

    /// <inheritdoc/>
    public IRepository<TransactionConfirmation> TransactionConfirmations
        => new Repository<TransactionConfirmation, Validator<TransactionConfirmation>>(_ctx.TransactionConfirmations);

    /// <inheritdoc/>
    public IRepository<Transaction> Transactions
        => new Repository<Transaction, TransactionValidator>(_ctx.Transactions);

    /// <inheritdoc/>
    public IRepository<UserGroup> UserGroups
        => new Repository<UserGroup, UserGroupValidator>(_ctx.UserGroups);

    /// <inheritdoc/>
    public IRepository<User> Users
        => new Repository<User, UserValidator>(_ctx.Users);

    /// <inheritdoc/>
    public IRepository<Log> Logs
        => new Repository<Log, LogValidator>(_ctx.Logs);

    /// <inheritdoc/>
    public IRepository<LogTag> LogTags
        => new Repository<LogTag, LogTagValidator>(_ctx.LogTags);

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