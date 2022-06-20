﻿using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
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
    public async Task<decimal> CalculateTotalVirtualAsync(Guid userId)
    {
        var recordsTotal = await _financialRecordsSvc.TotalUsableAsync(userId);
        var transactionsIn = await _transactionsSvc.TotalInAsync(userId, true);
        var transactionsOut = await _transactionsSvc.TotalOutAsync(userId, true);

        return recordsTotal + (transactionsIn - transactionsOut);
    }

    /// <inheritdoc/>
    public async Task<decimal> CalculateTotalUsableAsync(Guid userId)
    {
        var recordsTotal = await _financialRecordsSvc.TotalUsableAsync(userId);
        var transactionsTotal = await _transactionsSvc.TotalUsableAsync(userId);

        return recordsTotal + transactionsTotal;
    }

    /// <inheritdoc/>
    public async Task<decimal> TotalVirtualAsync(Guid userId)
    {
        await using var tx = await _uow.BeginTransactionAsync();
        return await CalculateTotalVirtualAsync(userId);
    }

    /// <inheritdoc/>
    public async Task<decimal> TotalUsableAsync(Guid userId)
    {
        await using var tx = await _uow.BeginTransactionAsync();
        return await CalculateTotalUsableAsync(userId);
    }

    /// <inheritdoc/>
    public async Task<Transaction> TransferUncomittedAsync(Guid fromId, Guid toId, decimal amount)
    {
        if (await CalculateTotalUsableAsync(fromId) < amount)
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
    public async Task<Transaction> TransferAsync(Guid fromId, Guid toId, decimal amount)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        var ret = await TransferUncomittedAsync(fromId, toId, amount);
        await _uow.CommitAsync(tx);

        return ret;
    }

    /// <inheritdoc/>
    public async Task<FinancialRecord> DepositAsync(Guid userId, decimal amount, string? note)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        if (amount <= 0)
            throw new InvalidDepositAmountException(amount);

        return await CreateFinancialRecord(userId, amount, note, tx);
    }

    /// <inheritdoc/>
    public async Task<FinancialRecord> WithdrawAsync(Guid userId, decimal amount, string? note)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        if (await CalculateTotalUsableAsync(userId) < amount)
            throw new InsufficientBalanceException(userId, amount);

        return await CreateFinancialRecord(userId, -amount, note, tx);
    }

    #endregion Public methods

    #region Private methods

    private async Task<FinancialRecord> CreateFinancialRecord(
        Guid userId, decimal amount, string? note, IAsyncDatabaseTransaction tx)
    {
        /// TODO:
        /// Use transactions here

        if (amount == 0)
            throw new InvalidDepositAmountException(amount);

        if (amount < 0 && await _uow.FinancialRecords.Query().AnyAsync(
            r => r.MadeBy.Id == userId && r.Verification == null && r.Amount < 0))
        {
            throw new PendingRequestExistsException("a withdraw request already exists");
        }
        else if (amount > 0 && await _uow.FinancialRecords.Query().AnyAsync
            (r => r.MadeBy.Id == userId && r.Verification == null && r.Amount > 0))
        {
            throw new PendingRequestExistsException("a deposit request already exists");
        }

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