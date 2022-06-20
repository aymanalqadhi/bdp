using BDP.Domain.Repositories;

using System;

namespace BDP.Infrastructure.Repositories.EntityFramework;

[Obsolete]
public sealed class LegacyUnitOfWork : ILegacyUnitOfWork, IDisposable, IAsyncDisposable
{
    #region Fields

    private readonly BdpDbContext _ctx;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="ctx">The application database context</param>
    public LegacyUnitOfWork(BdpDbContext ctx)
    {
        _ctx = ctx;
    }

    #endregion Public Constructors

    #region Properties

    /// <inheritdoc/>
    public IAttachmentsRepository Attachments => new AttachmentsRepository(_ctx.Attachments);

    /// <inheritdoc/>
    public IConfirmationsRepository Confirmations
        => new ConfirmationsRepository(_ctx.Confirmations);

    /// <inheritdoc/>
    public IEventsRepository Events => new EventsRepository(_ctx.Events);

    /// <inheritdoc/>
    public IEventTypesRepository EventTypes => new EventTypesRepository(_ctx.EventTypes);

    /// <inheritdoc/>
    public IFinancialRecordsRepository FinancialRecords
        => new FinancialRecordsRepository(_ctx.FinancialRecords);

    /// <inheritdoc/>
    public IFinancialRecordVerificationsRepository FinancialRecordVerficiations
        => new FinancialRecordVerificationsRepository(_ctx.FinancialRecordVerifications);

    /// <inheritdoc/>
    public IPhoneNumbersRepository PhoneNumbers => new PhoneNumbersRepository(_ctx.PhoneNumbers);

    /// <inheritdoc/>
    public IProductOrdersRepository ProductOrders => new ProductOrdersRepository(_ctx.ProductOrders);

    /// <inheritdoc/>
    public IProductsRepository Products => new ProductsRepository(_ctx.Products);

    /// <inheritdoc/>
    public IPurchasesRepository Purchases => new PurchasesRepository(_ctx.Purchases);

    /// <inheritdoc/>
    public IRefreshTokensRepository RefreshTokens => new RefreshTokensRepository(_ctx.RefreshTokens);

    /// <inheritdoc/>
    public ISellableReviewsRepository SellableReviews => new SellableReviewsRepository(_ctx.SellableReviews);

    /// <inheritdoc/>
    public ISellablesRepository Sellables => new SellablesRepository(_ctx.Sellables);

    /// <inheritdoc/>
    public IServiceReservationsRepository ServiceReservations
        => new ServiceReservationsRepository(_ctx.ServiceReservations);

    /// <inheritdoc/>
    public IServicesRepository Services => new ServicesRepository(_ctx.Services);

    /// <inheritdoc/>
    public ITransactionConfirmationsRepository TransactionConfirmations
        => new TransactionsConfirmationRepository(_ctx.TransactionConfirmations);

    /// <inheritdoc/>
    public ITransactionsRepository Transactions => new TransactionsRepository(_ctx.Transactions);

    /// <inheritdoc/>
    public IUserGroupsRepository UserGroups => new UserGroupsRepository(_ctx.UserGroups);

    /// <inheritdoc/>
    public IUsersRepository Users => new UsersRepository(_ctx.Users);

    #endregion Properties

    #region Public Methods

    /// <inheritdoc/>
    public async Task<IAsyncDatabaseTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => new AsyncDatabaseTransaction(await _ctx.Database.BeginTransactionAsync(cancellationToken));

    /// <inheritdoc/>
    public Task<int> CommitAsync()
        => _ctx.SaveChangesAsync();

    /// <inheritdoc/>
    public async Task<int> CommitAsync(IAsyncDatabaseTransaction transaction, CancellationToken cancellationToken = default)
    {
        var ret = await CommitAsync();

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