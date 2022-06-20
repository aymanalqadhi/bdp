using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using System.Linq.Expressions;

namespace BDP.Application.App;

public class TransactionsService : ITransactionsService
{
    #region Private fields

    private readonly IUnitOfWork _uow;

    #endregion Private fields

    #region Ctors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the application</param>
    public TransactionsService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion Ctors

    #region Public methods

    /// <inheritdoc/>
    public IQueryBuilder<Transaction> GetById(EntityKey<Transaction> id)
        => _uow.Transactions.Query().Where(t => t.Id == id);

    /// <inheritdoc/>
    public IQueryBuilder<Transaction> ForUserAsync(EntityKey<User> userId)
        => _uow.Transactions.Query().Where(t => t.From.Id == userId || t.To.Id == userId);

    /// <inheritdoc/>
    public Task<decimal> TotalInAsync(EntityKey<User> userId, bool confirmedOnly = false)
        => DoCalculateTotal(userId, TransactionDirection.Incoming, confirmedOnly);

    /// <inheritdoc/>
    public Task<decimal> TotalOutAsync(EntityKey<User> userId, bool confirmedOnly = false)
        => DoCalculateTotal(userId, TransactionDirection.Outgoing, confirmedOnly);

    /// <inheritdoc/>
    public async Task<decimal> TotalUsableAsync(EntityKey<User> userId)
        => await TotalInAsync(userId, true) - await TotalOutAsync(userId, true);

    /// <inheritdoc/>
    public async Task<TransactionConfirmation> ConfirmAsync(EntityKey<User> userId, string confirmationToken)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        var transaction = await _uow.Transactions
            .Query()
            .Include(t => t.Confirmation!)
            .FirstAsync(t => t.ConfirmationToken == confirmationToken && t.To.Id == userId);

        return await DoCreateConfirmation(transaction, TransactionConfirmationOutcome.Confirmed, tx);
    }

    /// <inheritdoc/>
    public async Task<TransactionConfirmation> CancelAsync(EntityKey<User> userId, EntityKey<Transaction> transactionId)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        var transaction = await _uow.Transactions
            .Query()
            .Include(t => t.Confirmation!)
            .FirstAsync(t => t.Id == transactionId && t.To.Id == userId);

        return await DoCreateConfirmation(transaction, TransactionConfirmationOutcome.Declined, tx);
    }

    #endregion Public methods

    #region Private methods

    private async Task<decimal> DoCalculateTotal(
        EntityKey<User> userId,
        TransactionDirection direction,
        bool confirmedOnly = false)
    {
        if (confirmedOnly)
        {
            var res = direction == TransactionDirection.Outgoing
                ? _uow.Transactions
                    .Query()
                    .Where(t => t.Confirmation != null)
                    .Where(t => t.Confirmation!.Outcome == TransactionConfirmationOutcome.Confirmed && t.From.Id == userId)
                : _uow.Transactions
                    .Query()
                    .Where(t => t.Confirmation != null)
                    .Where(t => t.Confirmation!.Outcome == TransactionConfirmationOutcome.Confirmed && t.To.Id == userId);

            return await res.AsAsyncEnumerable().SumAsync(t => t.Amount);
        }

        return await _uow.Transactions
            .Query()
            .Where(direction == TransactionDirection.Outgoing
                 ? t => t.From.Id == userId
                 : t => t.To.Id == userId)
            .AsAsyncEnumerable()
            .SumAsync(t => t.Amount);
    }

    private async Task<TransactionConfirmation> DoCreateConfirmation(
        Transaction transaction,
        TransactionConfirmationOutcome outcome,
        IAsyncDatabaseTransaction tx)
    {
        if (transaction.Confirmation != null)
            throw new TransactionAlreadyConfirmedException(transaction.Confirmation);

        var confirmation = new TransactionConfirmation
        {
            Transaction = transaction,
            Outcome = outcome,
        };

        _uow.TransactionConfirmations.Add(confirmation);
        await _uow.CommitAsync(tx);

        return confirmation;
    }

    #endregion Private methods

    private enum TransactionDirection
    {
        Incoming,
        Outgoing,
    }
}