using BDP.Domain.Entities;
using BDP.Domain.Repositories;

using System.Linq.Expressions;

namespace BDP.Domain.Services;

public interface ITransactionsService
{
    /// <summary>
    /// Asynchronously gets a transaction by id
    /// </summary>
    /// <param name="id">The id of the transaction</param>
    /// <returns>The transaction with the passed id</returns>
    IQueryBuilder<Transaction> GetByIdAsync(Guid id);

    /// <summary>
    /// Asynchrnously gets transactions that a user has either sent or recieved
    /// limited by pagination
    /// </summary>
    /// <param name="userId">The id of the user which to get transactions for</param>
    /// <returns></returns>
    IQueryBuilder<Transaction> ForUserAsync(Guid userId);

    /// <summary>
    /// Asynchronously gets the total input transferred to the user
    /// </summary>
    /// <param name="userId">The id of the user to get the total for</param>
    /// <param name="confirmedOnly">If true, only confirmed transactions will count</param>
    /// <returns>Total account input</returns>
    Task<decimal> TotalInAsync(Guid userId, bool confirmedOnly = false);

    /// <summary>
    /// Asynchronously gets the total output transferred by the user
    /// </summary>
    /// <param name="userId">The id of the user to get the total for</param>
    /// <param name="confirmedOnly">If true, only confirmed transactions will count</param>
    /// <returns>Total account output</returns>
    Task<decimal> TotalOutAsync(Guid userId, bool confirmedOnly = false);

    /// <summary>
    /// Asynchronously gets the total usable balance for the user
    /// </summary>
    /// <param name="userId">The id of the user to get the total for</param>
    /// <returns>Total usable credit</returns>
    Task<decimal> TotalUsableAsync(Guid userId);

    /// <summary>
    /// Asynchronously confirms a transaction
    /// </summary>
    /// <param name="senderId">The id of the sender of the transaction</param>
    /// <param name="confirmationToken">The token to confirm with</param>
    /// <returns>The transaction confirmation</returns>
    Task<TransactionConfirmation> ConfirmAsync(Guid senderId, string confirmationToken);

    /// <summary>
    /// Asynchronously cancels a transaction
    /// </summary>
    /// <param name="receiverId">The id of the receiver of the transaction</param>
    /// <param name="transactionId">The id of the transaction to cancel</param>
    /// <returns>The canceled transaction confirmation</returns>
    Task<TransactionConfirmation> CancelAsync(Guid receiverId, Guid transactionId);
}