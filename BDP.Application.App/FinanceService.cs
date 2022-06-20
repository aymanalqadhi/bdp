using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services;

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
    /// <param name="attachmentsSvc">The attachments managment service</param>
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
    public async Task<decimal> CalculateTotalVirtualAsync(User user)
    {
        var recordsTotal = await _financialRecordsSvc.TotalUsableAsync(user);
        var transactionsIn = await _transactionsSvc.TotalInAsync(user, true);
        var transactionsOut = await _transactionsSvc.TotalOutAsync(user, true);

        return recordsTotal + (transactionsIn - transactionsOut);
    }

    /// <inheritdoc/>
    public async Task<decimal> CalculateTotalUsableAsync(User user)
    {
        var recordsTotal = await _financialRecordsSvc.TotalUsableAsync(user);
        var transactionsTotal = await _transactionsSvc.TotalUsableAsync(user);

        return recordsTotal + transactionsTotal;
    }

    /// <inheritdoc/>
    public async Task<decimal> TotalVirtualAsync(User user)
    {
        await using var tx = await _uow.BeginTransactionAsync();
        return await CalculateTotalVirtualAsync(user);
    }

    /// <inheritdoc/>
    public async Task<decimal> TotalUsableAsync(User user)
    {
        await using var tx = await _uow.BeginTransactionAsync();
        return await CalculateTotalUsableAsync(user);
    }

    /// <inheritdoc/>
    public async Task<Transaction> TransferUncomittedAsync(User from, User to, decimal amount)
    {
        if (await CalculateTotalUsableAsync(from) < amount)
            throw new InsufficientBalanceException(from, amount);

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
    public async Task<Transaction> TransferAsync(User from, User to, decimal amount)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        var ret = await TransferUncomittedAsync(from, to, amount);
        await _uow.CommitAsync(tx);

        return ret;
    }

    /// <inheritdoc/>
    public async Task<FinancialRecord> DepositAsync(User user, decimal amount, string? note)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        if (amount <= 0)
            throw new InvalidDepositAmountException(amount);

        return await CreateFinancialRecord(user, amount, note, tx);
    }

    /// <inheritdoc/>
    public async Task<FinancialRecord> WithdrawAsync(User user, decimal amount, string? note)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        if (await CalculateTotalUsableAsync(user) < amount)
            throw new InsufficientBalanceException(user, amount);

        return await CreateFinancialRecord(user, -amount, note, tx);
    }

    #endregion Public methods

    #region Private methods

    private async Task<FinancialRecord> CreateFinancialRecord(
        User user, decimal amount, string? note, IAsyncDatabaseTransaction tx)
    {
        if (amount == 0)
            throw new InvalidDepositAmountException(amount);

        if (amount < 0 && await _uow.FinancialRecords.Query().AnyAsync(
            r => r.MadeBy.Id == user.Id && r.Verification == null && r.Amount < 0))
        {
            throw new PendingRequestExistsException("a withdraw request already exists");
        }
        else if (amount > 0 && await _uow.FinancialRecords.Query().AnyAsync
            (r => r.MadeBy.Id == user.Id && r.Verification == null && r.Amount > 0))
        {
            throw new PendingRequestExistsException("a deposit request already exists");
        }

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