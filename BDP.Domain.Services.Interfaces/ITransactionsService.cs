﻿using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services.Exceptions;

namespace BDP.Domain.Services;

/// <summary>
/// An interface to be implemented by transaction managment services
/// </summary>
public interface ITransactionsService
{
    #region Public Methods

    /// <summary>
    /// Asynchronously cancels a transaction
    /// </summary>
    /// <param name="receiverId">The id of the receiver of the transaction</param>
    /// <param name="transactionId">The id of the transaction to cancel</param>
    /// <returns>The canceled transaction confirmation</returns>
    /// <exception cref="TransactionAlreadyConfirmedException"></exception>
    Task<TransactionConfirmation> CancelAsync(
        EntityKey<User> receiverId,
        EntityKey<Transaction> transactionId);

    /// <summary>
    /// Asynchronously confirms a transaction
    /// </summary>
    /// <param name="senderId">The id of the sender of the transaction</param>
    /// <param name="transactionId">The id of the transaction to confirm</param>
    /// <param name="confirmationToken">The token to confirm with</param>
    /// <returns>The transaction confirmation</returns>
    /// <exception cref="TransactionAlreadyConfirmedException"></exception>
    Task<TransactionConfirmation> ConfirmAsync(
        EntityKey<User> senderId,
        EntityKey<Transaction> transactionId,
        string confirmationToken);

    /// <summary>
    /// Asynchrnously gets transactions that a user has either sent or recieved
    /// limited by pagination
    /// </summary>
    /// <param name="userId">The id of the user which to get transactions for</param>
    /// <returns></returns>
    IQueryBuilder<Transaction> ForUserAsync(EntityKey<User> userId);

    /// <summary>
    /// Asynchronously gets confirmation token of a transaction
    /// </summary>
    /// <param name="userId">The id of the owner of the transaction (from)</param>
    /// <param name="transactionId">The id of the transaction to get its confirmation</param>
    /// <returns>The confirmation token of the transaction</returns>
    Task<string> GetConfirmationTokenAsync(EntityKey<User> userId, EntityKey<Transaction> transactionId);

    /// <summary>
    /// Asynchronously gets the total input transferred to the user
    /// </summary>
    /// <param name="userId">The id of the user to get the total for</param>
    /// <param name="confirmedOnly">If true, only confirmed transactions will count</param>
    /// <returns>Total account input</returns>
    Task<decimal> TotalInAsync(EntityKey<User> userId, bool confirmedOnly = false);

    /// <summary>
    /// Asynchronously gets the total output transferred by the user
    /// </summary>
    /// <param name="userId">The id of the user to get the total for</param>
    /// <param name="confirmedOnly">If true, only confirmed transactions will count</param>
    /// <returns>Total account output</returns>
    Task<decimal> TotalOutAsync(EntityKey<User> userId, bool confirmedOnly = false);

    /// <summary>
    /// Asynchronously gets the total usable balance for the user
    /// </summary>
    /// <param name="userId">The id of the user to get the total for</param>
    /// <returns>Total usable credit</returns>
    Task<decimal> TotalUsableAsync(EntityKey<User> userId);

    #endregion Public Methods
}