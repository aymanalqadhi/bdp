using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Domain.Services.Exceptions;

namespace BDP.Application.App;

public class FinanceService : IFinanceService
{
    #region Private fields

    private readonly IUnitOfWork _uow;
    private readonly IConfigurationService _configSvc;
    private readonly IRandomGeneratorService _rngSvc;
    private readonly IFinancialRecordsService _financialRecordsSvc;
    private readonly ITransactionsService _transactionsSvc;

    #endregion Private fields

    #region Ctors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit-of-work of the application</param>
    /// <param name="configSvc">The configuration service of the application</param>
    /// <param name="rngSvc">The random generator service of the application</param>
    /// <param name="financialRecordsSvc">The financial documents service</param>
    /// <param name="transactionsSvc">The transactions service</param>
    public FinanceService(
        IUnitOfWork uow,
        IConfigurationService configSvc,
        IRandomGeneratorService rngSvc,
        IFinancialRecordsService financialRecordsSvc,
        ITransactionsService transactionsSvc)
    {
        _uow = uow;
        _configSvc = configSvc;
        _rngSvc = rngSvc;
        _financialRecordsSvc = financialRecordsSvc;
        _transactionsSvc = transactionsSvc;
    }

    #endregion Ctors

    #region Public methods

    /// <inheritdoc/>
    public async Task<decimal> CalculateBalanceAsync(EntityKey<User> userId)
    {
        var recordsTotal = await _financialRecordsSvc.CalculateUsableAsync(userId);
        var transactionsTotal = await _transactionsSvc.CalculateUsableAsync(userId);

        return recordsTotal + transactionsTotal;
    }

    /// <inheritdoc/>
    public async Task<decimal> BalanceAsync(EntityKey<User> userId)
    {
        await using var tx = await _uow.BeginTransactionAsync();
        return await CalculateBalanceAsync(userId);
    }

    /// <inheritdoc/>
    public async Task<Transaction> TransferUncomittedAsync(EntityKey<User> fromId, EntityKey<User> toId, decimal amount)
    {
        if (await CalculateBalanceAsync(fromId) < amount)
            throw new InsufficientBalanceException(fromId, amount);

        var from = await _uow.Users.Query().FindAsync(fromId);
        var to = await _uow.Users.Query().FindAsync(toId);

        var transaction = new Transaction
        {
            From = from,
            To = to,
            Amount = amount,
            ConfirmationToken = _rngSvc.RandomString(
                _configSvc.GetInt("Auth:Confirmation:TokenLength", 64), RandomStringKind.AlphaNum)
        };

        _uow.Transactions.Add(transaction);

        return transaction;
    }

    /// <inheritdoc/>
    public async Task<Transaction> TransferAsync(EntityKey<User> fromId, EntityKey<User> toId, decimal amount)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        var ret = await TransferUncomittedAsync(fromId, toId, amount);
        await _uow.CommitAsync(tx);

        return ret;
    }

    /// <inheritdoc/>
    public async Task<FinancialRecord> DepositAsync(EntityKey<User> userId, decimal amount, string? note)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        if (amount <= 0)
            throw new InvalidDepositAmountException(amount);

        if (await _financialRecordsSvc.ForUser(userId, true).AnyAsync(r => r.Amount > 0))
            throw new PendingRequestExistsException("a deposit request already exists");

        return await CreateFinancialRecord(userId, amount, note, tx);
    }

    /// <inheritdoc/>
    public async Task<FinancialRecord> WithdrawAsync(EntityKey<User> userId, decimal amount, string? note)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        if (await CalculateBalanceAsync(userId) < amount)
            throw new InsufficientBalanceException(userId, amount);

        if (await _financialRecordsSvc.ForUser(userId, true).AnyAsync(r => r.Amount < 0))
            throw new PendingRequestExistsException("a withdraw request already exists");

        return await CreateFinancialRecord(userId, -amount, note, tx);
    }

    #endregion Public methods

    #region Private methods

    private async Task<FinancialRecord> CreateFinancialRecord(
        EntityKey<User> userId,
        decimal amount,
        string? note,
        IAsyncDatabaseTransaction tx)
    {
        var user = await _uow.Users.Query().FindAsync(userId);

        var record = new FinancialRecord
        {
            MadeBy = user,
            Amount = amount,
            Note = note,
        };

        _uow.FinancialRecords.Add(record);
        await _uow.CommitAsync(tx);

        return record;
    }

    #endregion Private methods
}