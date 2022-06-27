using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Domain.Services.Exceptions;

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
            .FirstAsync(t => t.Id == transactionId && t.To.Id == userId);

        return await DoCreateConfirmation(transaction, false, tx);
    }

    /// <inheritdoc/>
    public async Task<TransactionConfirmation> ConfirmAsync(
        EntityKey<User> userId,
        EntityKey<Transaction> transactionId,
        string confirmationToken)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        var transaction = await _uow.Transactions
            .Query()
            .Include(t => t.Confirmation!)
            .Where(t => t.From.Id == userId || t.To.Id == userId)
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
    public Task<decimal> TotalInAsync(EntityKey<User> userId, bool confirmedOnly = false)
        => DoCalculateTotal(userId, TransactionDirection.Incoming, confirmedOnly);

    /// <inheritdoc/>
    public Task<decimal> TotalOutAsync(EntityKey<User> userId, bool confirmedOnly = false)
        => DoCalculateTotal(userId, TransactionDirection.Outgoing, confirmedOnly);

    /// <inheritdoc/>
    public async Task<decimal> TotalUsableAsync(EntityKey<User> userId)
        => await TotalInAsync(userId, true) - await TotalOutAsync(userId, true);

    #endregion Public Methods

    #region Private Methods

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
                    .Where(t => t.Confirmation!.IsAccepted && t.From.Id == userId)
                : _uow.Transactions
                    .Query()
                    .Where(t => t.Confirmation != null)
                    .Where(t => t.Confirmation!.IsAccepted && t.To.Id == userId);

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
        bool isAccepted,
        IAsyncDatabaseTransaction tx)
    {
        if (transaction.Confirmation is not null)
            throw new TransactionAlreadyConfirmedException(transaction.Confirmation);

        var confirmation = new TransactionConfirmation
        {
            Transaction = transaction,
            IsAccepted = isAccepted,
        };

        _uow.TransactionConfirmations.Add(confirmation);
        await _uow.CommitAsync(tx);

        return confirmation;
    }

    #endregion Private Methods
}