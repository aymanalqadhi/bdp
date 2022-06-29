using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Domain.Services.Exceptions;

using System.Linq.Expressions;

namespace BDP.Application.App;

public class TransactionsService : ITransactionsService
{
    #region Fields

    private readonly IUnitOfWork _uow;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the application</param>
    public TransactionsService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion Public Constructors

    #region Enums

    private enum TransactionDirection
    {
        Incoming,
        Outgoing,
    }

    #endregion Enums

    #region Public Methods

    /// <inheritdoc/>
    public async Task<TransactionConfirmation> CancelAsync(
        EntityKey<User> userId,
        EntityKey<Transaction> transactionId)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        var transaction = await _uow.Transactions
            .Query()
            .Include(t => t.Confirmation!)
            .Where(t => t.To.Id == userId)
            .FindAsync(transactionId);

        return await DoCreateConfirmation(transaction, false, tx);
    }

    /// <inheritdoc/>
    public async Task<TransactionConfirmation> ConfirmAsync(
        EntityKey<User> userId,
        EntityKey<Transaction> transactionId,
        string confirmationToken)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        var transaction = await ForUser(userId)
            .Where(t => t.ConfirmationToken == confirmationToken)
            .FindAsync(transactionId);

        return await DoCreateConfirmation(transaction, true, tx);
    }

    /// <inheritdoc/>
    public IQueryBuilder<Transaction> ForUser(EntityKey<User> userId)
        => _uow.Transactions.Query().Where(t => t.From.Id == userId || t.To.Id == userId);

    /// <inheritdoc/>
    public async Task<string> GetConfirmationTokenAsync(
        EntityKey<User> userId,
        EntityKey<Transaction> transactionId)
    {
        var transaction = await _uow.Transactions.Query()
            .Include(t => t.From)
            .FindWithOwnershipValidationAsync(userId, transactionId, t => t.From);

        return transaction.ConfirmationToken;
    }

    /// <inheritdoc/>
    public async Task<decimal> CalculateUsableAsync(EntityKey<User> userId)
    {
        var confirmedIn = await _uow.Transactions.Query()
            .Where(t => t.To.Id == userId)
            .Where(t => t.Confirmation != null && t.Confirmation.IsAccepted)
            .AsAsyncEnumerable()
            .SumAsync(t => t.Amount);

        var notDeclinedOut = await _uow.Transactions.Query()
            .Where(t => t.From.Id == userId)
            .Where(t => t.Confirmation == null || t.Confirmation.IsAccepted)
            .AsAsyncEnumerable()
            .SumAsync(t => t.Amount);

        return confirmedIn - notDeclinedOut;
    }

    #endregion Public Methods

    #region Private Methods

    private async Task<TransactionConfirmation> DoCreateConfirmation(
        Transaction transaction,
        bool isAccepted,
        IAsyncDatabaseTransaction tx)
    {
        if (transaction.Confirmation is not null)
            throw new TransactionAlreadyConfirmedException(transaction.Confirmation);

        var confirmation = new TransactionConfirmation
        {
            IsAccepted = isAccepted,
            Transaction = transaction,
        };

        _uow.TransactionConfirmations.Add(confirmation);
        await _uow.CommitAsync(tx);

        return confirmation;
    }

    #endregion Private Methods
}