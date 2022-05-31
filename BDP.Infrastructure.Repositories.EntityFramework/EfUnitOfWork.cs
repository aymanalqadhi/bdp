using BDP.Domain.Repositories;

namespace BDP.Infrastructure.Repositories.EntityFramework;

public sealed class EfUnitOfWork : ILegacyUnitOfWork, IDisposable, IAsyncDisposable
{
    private readonly BdpDbContext _ctx;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="ctx">The application database context</param>
    public EfUnitOfWork(BdpDbContext ctx)
    {
        _ctx = ctx;
    }

    /// <inheritdoc/>
    public IUsersRepository Users => new UsersRepository(_ctx.Users);

    /// <inheritdoc/>
    public IPhoneNumbersRepository PhoneNumbers => new PhoneNumbersRepository(_ctx.PhoneNumbers);

    /// <inheritdoc/>
    public IUserGroupsRepository UserGroups => new UserGroupsRepository(_ctx.UserGroups);

    /// <inheritdoc/>
    public IRefreshTokensRepository RefreshTokens => new RefreshTokensRepository(_ctx.RefreshTokens);

    /// <inheritdoc/>
    public IEventsRepository Events => new EventsRepository(_ctx.Events);

    /// <inheritdoc/>
    public IEventTypesRepository EventTypes => new EventTypesRepository(_ctx.EventTypes);

    /// <inheritdoc/>
    public ISellablesRepository Sellables => new SellablesRepository(_ctx.Sellables);

    /// <inheritdoc/>
    public ISellableReviewsRepository SellableReviews => new SellableReviewsRepository(_ctx.SellableReviews);

    /// <inheritdoc/>
    public IProductsRepository Products => new ProductsRepository(_ctx.Products);

    /// <inheritdoc/>
    public IProductOrdersRepository ProductOrders => new ProductOrdersRepository(_ctx.ProductOrders);

    /// <inheritdoc/>
    public IServicesRepository Services => new ServicesRepository(_ctx.Services);

    /// <inheritdoc/>
    public IServiceReservationsRepository ServiceReservations
        => new ServiceReservationsRepository(_ctx.ServiceReservations);

    /// <inheritdoc/>
    public IPurchasesRepository Purchases => new PurchasesRepository(_ctx.Purchases);

    /// <inheritdoc/>
    public IAttachmentsRepository Attachments => new AttachmentsRepository(_ctx.Attachments);

    /// <inheritdoc/>
    public ITransactionsRepository Transactions => new TransactionsRepository(_ctx.Transactions);

    /// <inheritdoc/>
    public ITransactionConfirmationsRepository TransactionConfirmations
        => new TransactionsConfirmationRepository(_ctx.TransactionConfirmations);

    /// <inheritdoc/>
    public IFinancialRecordsRepository FinancialRecords
        => new FinancialRecordsRepository(_ctx.FinancialRecords);

    /// <inheritdoc/>
    public IFinancialRecordVerificationsRepository FinancialRecordVerficiations
        => new FinancialRecordVerificationsRepository(_ctx.FinancialRecordVerifications);

    /// <inheritdoc/>
    public IConfirmationsRepository Confirmations
        => new ConfirmationsRepository(_ctx.Confirmations);

    /// <inheritdoc/>
    public Task<int> CommitAsync()
        => _ctx.SaveChangesAsync();

    /// <inheritdoc/>
    public async Task<int> CommitAsync(IDatabaseTransaction transaction, CancellationToken cancellationToken = default)
    {
        var ret = await CommitAsync();

        await transaction.CommitAsync(cancellationToken);

        return ret;
    }

    /// <inheritdoc/>
    public async Task<IDatabaseTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => new EfDatabaseTransaction(await _ctx.Database.BeginTransactionAsync(cancellationToken));

    /// <inheritdoc/>
    public void Dispose()
        => _ctx.Dispose();

    /// <inheritdoc/>
    public ValueTask DisposeAsync()
        => _ctx.DisposeAsync();
}