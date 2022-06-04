using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
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
    public IAsyncEnumerable<Transaction> ForUserAsync(
        int page,
        int pageSize,
        User user,
        Expression<Func<Transaction, object>>[]? includes = null)
    {
        var query = _uow.Transactions.Query();

        if (includes is not null)
            query = query.IncludeAll(includes);

        return query
            .Where(t => t.From.Id == user.Id || t.To.Id == user.Id)
            .OrderByDescending(t => t.Id)
            .Page(page, pageSize)
            .AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<Transaction> SentByAsync(User user,
        Expression<Func<Transaction, object>>[]? includes = null)
    {
        var query = _uow.Transactions.Query();

        if (includes is not null)
            query = query.IncludeAll(includes);

        return query.Where(t => t.From.Id == user.Id).AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<Transaction> ReceivedByAsync(User user,
        Expression<Func<Transaction, object>>[]? includes = null)
    {
        var query = _uow.Transactions.Query();

        if (includes is not null)
            query = query.IncludeAll(includes);

        return query.Where(t => t.To.Id == user.Id).AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public Task<decimal> TotalInAsync(User user, bool confirmedOnly = false)
        => DoCalculateTotal(user, TransactionDirection.Incoming, confirmedOnly);

    /// <inheritdoc/>
    public Task<decimal> TotalOutAsync(User user, bool confirmedOnly = false)
        => DoCalculateTotal(user, TransactionDirection.Outgoing, confirmedOnly);

    /// <inheritdoc/>
    public async Task<decimal> TotalUsableAsync(User user)
        => await TotalInAsync(user, true) - await TotalOutAsync(user, true);

    /// <inheritdoc/>
    public async Task<TransactionConfirmation> ConfirmAsync(User receiver, string confirmationToken)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        var transaction = await _uow.Transactions
            .Query()
            .Include(t => t.Confirmation!)
            .FirstAsync(t => t.ConfirmationToken == confirmationToken && t.To.Id == receiver.Id);

        return await DoCreateConfirmation(transaction, TransactionConfirmationOutcome.Confirmed, tx);
    }

    /// <inheritdoc/>
    public async Task<TransactionConfirmation> CancelAsync(User receiver, long transactionId)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        var transaction = await _uow.Transactions
            .Query()
            .Include(t => t.Confirmation!)
            .FirstAsync(t => t.Id == transactionId && t.To.Id == receiver.Id);

        return await DoCreateConfirmation(transaction, TransactionConfirmationOutcome.Declined, tx);
    }

    #endregion Public methods

    #region Private methods

    private async Task<decimal> DoCalculateTotal(
        User user,
        TransactionDirection direction,
        bool confirmedOnly = false)
    {
        if (confirmedOnly)
        {
            var res = direction == TransactionDirection.Outgoing
                ? _uow.Transactions
                    .Query()
                    .Where(t => t.Confirmation != null)
                    .Where(t => t.Confirmation!.Outcome == TransactionConfirmationOutcome.Confirmed && t.From.Id == user.Id)
                : _uow.Transactions
                    .Query()
                    .Where(t => t.Confirmation != null)
                    .Where(t => t.Confirmation!.Outcome == TransactionConfirmationOutcome.Confirmed && t.To.Id == user.Id);

            return await res.AsAsyncEnumerable().SumAsync(t => t.Amount);
        }

        return await _uow.Transactions
            .Query()
            .Where(direction == TransactionDirection.Outgoing
                 ? t => t.From.Id == user.Id
                 : t => t.To.Id == user.Id)
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