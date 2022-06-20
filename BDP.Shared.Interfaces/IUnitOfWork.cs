using BDP.Domain.Entities;

namespace BDP.Domain.Repositories;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    #region Properties

    /// <summary>
    /// Gets the attachments repository
    /// </summary>
    IRepository<Attachment> Attachments { get; }

    /// <summary>
    /// Gets the confirmations repository
    /// </summary>
    IRepository<Confirmation> Confirmations { get; }

    /// <summary>
    /// Gets the events repository
    /// </summary>
    IRepository<Event> Events { get; }

    /// <summary>
    /// Gets the event types repository
    /// </summary>
    IRepository<EventType> EventTypes { get; }

    /// <summary>
    /// Gets the financial records repository
    /// </summary>
    IRepository<FinancialRecord> FinancialRecords { get; }

    /// <summary>
    /// Gets the financial records verifications repository
    /// </summary>
    IRepository<FinancialRecordVerification> FinancialRecordVerficiations { get; }

    /// <summary>
    /// Gets the phone numbers repository
    /// </summary>
    IRepository<PhoneNumber> PhoneNumbers { get; }

    /// <summary>
    /// Gets the product orders repository
    /// </summary>
    IRepository<ProductOrder> ProductOrders { get; }

    /// <summary>
    /// Gets the products repository
    /// </summary>
    IRepository<Product> Products { get; }

    /// <summary>
    /// Gets the purchases repository
    /// </summary>
    IRepository<Purchase> Purchases { get; }

    /// <summary>
    /// Gets the refresh tokens repository
    /// </summary>
    IRepository<RefreshToken> RefreshTokens { get; }

    /// <summary>
    /// Gets the sellable reviews repository
    /// </summary>
    IRepository<SellableReview> SellableReviews { get; }

    /// <summary>
    /// Gets the sellables respository
    /// </summary>
    IRepository<Sellable> Sellables { get; }

    /// <summary>
    /// Gets the service reservations repository
    /// </summary>
    IRepository<ServiceReservation> ServiceReservations { get; }

    /// <summary>
    /// Gets the services repository
    /// </summary>
    IRepository<Service> Services { get; }

    /// <summary>
    /// Gets the transaction confirmations repository
    /// </summary>
    IRepository<TransactionConfirmation> TransactionConfirmations { get; }

    /// <summary>
    /// Gets the transactions repository
    /// </summary>
    IRepository<Transaction> Transactions { get; }

    /// <summary>
    /// Gets the user groups repository
    /// </summary>
    IRepository<UserGroup> UserGroups { get; }

    /// <summary>
    /// Gets the users repository
    /// </summary>
    IRepository<User> Users { get; }

    /// <summary>
    /// Gets the logs repository
    /// </summary>
    IRepository<Log> Logs { get; }

    /// <summary>
    /// Gets the log tags repository
    /// </summary>
    IRepository<LogTag> LogTags { get; }

    #endregion Properties

    #region Public Methods

    /// <summary>
    /// Asynchronously begins a transaction
    /// </summary>
    /// <param name="cancellationToken">The task cancellation token</param>
    /// <returns></returns>
    Task<IAsyncDatabaseTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits changes to the database
    /// </summary>
    /// <returns></returns>
    int Commit();

    /// <summary>
    /// Asynchronously commits changes to data backend
    /// </summary>
    /// <param name="cancellationToken">The task cancellation token</param>
    /// <returns>The affected records count</returns>
    Task<int> CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously commits changes to data backend
    /// </summary>
    /// <param name="transaction">A database transaction to be used for commit</param>
    /// <param name="cancellationToken">The task cancellation token</param>
    /// <returns>The affected records count</returns>
    Task<int> CommitAsync(
        IAsyncDatabaseTransaction transaction,
        CancellationToken cancellationToken = default);

    #endregion Public Methods
}