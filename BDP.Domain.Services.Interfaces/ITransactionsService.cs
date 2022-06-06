using BDP.Domain.Entities;
using BDP.Domain.Repositories;

using System.Linq.Expressions;

namespace BDP.Domain.Services;

public interface ITransactionsService
{
    /// <summary>
    /// Asynchrnously gets transactions that a user has either sent or recieved
    /// limited by pagination
    /// </summary>
    /// <param name="user">The user which to get transactions for</param>
    /// <returns></returns>
    IQueryBuilder<Transaction> ForUserAsync(User user);

    /// <summary>
    /// Asynchronously gets the total input transferred to the user
    /// </summary>
    /// <param name="user">The user to get the total for</param>
    /// <param name="confirmedOnly">If true, only confirmed transactions will count</param>
    /// <returns>Total account input</returns>
    Task<decimal> TotalInAsync(User user, bool confirmedOnly = false);

    /// <summary>
    /// Asynchronously gets the total output transferred by the user
    /// </summary>
    /// <param name="user">The user to get the total for</param>
    /// <param name="confirmedOnly">If true, only confirmed transactions will count</param>
    /// <returns>Total account output</returns>
    Task<decimal> TotalOutAsync(User user, bool confirmedOnly = false);

    /// <summary>
    /// Asynchronously gets the total usable balance for the user
    /// </summary>
    /// <param name="user">The user to get the total for</param>
    /// <returns>Total usable credit</returns>
    Task<decimal> TotalUsableAsync(User user);

    /// <summary>
    /// Asynchronously confirms a transaction
    /// </summary>
    /// <param name="sender">The sender of the transaction</param>
    /// <param name="confirmationToken">The token to confirm with</param>
    /// <returns>The transaction confirmation</returns>
    Task<TransactionConfirmation> ConfirmAsync(User sender, string confirmationToken);

    /// <summary>
    /// Asynchronously cancels a transaction
    /// </summary>
    /// <param name="receiver">The receiver of the transaction</param>
    /// <param name="transactionId">The id of the transaction to cancel</param>
    /// <returns>The canceled transaction confirmation</returns>
    Task<TransactionConfirmation> CancelAsync(User receiver, long transactionId);
}