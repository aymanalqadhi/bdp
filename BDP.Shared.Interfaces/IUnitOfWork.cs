namespace BDP.Domain.Repositories;

/// <summary>
/// An interface to represent a unit-of-work functionality
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets the users repository
    /// </summary>
    IUsersRepository Users { get; }

    /// <summary>
    /// Gets the phone numbers repository
    /// </summary>
    IPhoneNumbersRepository PhoneNumbers { get; }

    /// <summary>
    /// Gets the user groups repository
    /// </summary>
    IUserGroupsRepository UserGroups { get; }

    /// <summary>
    /// Gets the refresh tokens repository
    /// </summary>
    IRefreshTokensRepository RefreshTokens { get; }

    /// <summary>
    /// Gets the events repository
    /// </summary>
    IEventsRepository Events { get; }

    /// <summary>
    /// Gets the event types repository
    /// </summary>
    IEventTypesRepository EventTypes { get; }

    /// <summary>
    /// Gets the sellables respository
    /// </summary>
    ISellablesRepository Sellables { get; }

    /// <summary>
    /// Gets the sellable reviews repository
    /// </summary>
    ISellableReviewsRepository SellableReviews { get; }

    /// <summary>
    /// Gets the products repository
    /// </summary>
    IProductsRepository Products { get; }

    /// <summary>
    /// Gets the product orders repository
    /// </summary>
    IProductOrdersRepository ProductOrders { get; }

    /// <summary>
    /// Gets the services repository
    /// </summary>
    IServicesRepository Services { get; }

    /// <summary>
    /// Gets the service reservations repository
    /// </summary>
    IServiceReservationsRepository ServiceReservations { get; }

    /// <summary>
    /// Gets the purchases repository
    /// </summary>
    IPurchasesRepository Purchases { get; }

    /// <summary>
    /// Gets the attachments repository
    /// </summary>
    IAttachmentsRepository Attachments { get; }

    /// <summary>
    /// Gets the transactions repository
    /// </summary>
    ITransactionsRepository Transactions { get; }

    /// <summary>
    /// Gets the transaction confirmations repository
    /// </summary>
    ITransactionConfirmationsRepository TransactionConfirmations { get; }

    /// <summary>
    /// Gets the financial records repository
    /// </summary>
    IFinancialRecordsRepository FinancialRecords { get; }

    /// <summary>
    /// Gets the financial records verifications repository
    /// </summary>
    IFinancialRecordVerificationsRepository FinancialRecordVerficiations { get; }

    /// <summary>
    /// Gets the confirmations repository
    /// </summary>
    IConfirmationsRepository Confirmations { get; }

    /// <summary>
    /// Asynchronously commits changes to data backend
    /// </summary>
    /// <returns>The affected records count</returns>
    Task<int> CommitAsync();

    /// <summary>
    /// Asynchronously commits changes to data backend
    /// </summary>
    /// <returns>The affected records count</returns>
    Task<int> CommitAsync(IDatabaseTransaction transaction, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously begins a transaction
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IDatabaseTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}