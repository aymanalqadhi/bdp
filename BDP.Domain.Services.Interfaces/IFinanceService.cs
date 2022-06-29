using BDP.Domain.Entities;
using BDP.Domain.Services.Exceptions;

namespace BDP.Domain.Services;

/// <summary>
/// An interface to be implemented by finance managment services
/// </summary>
public interface IFinanceService
{
    /// <summary>
    /// Asynchronously calculates the usable balance of a user. This method does not use
    /// transactions, so it is unsafe to use it outside one
    /// </summary>
    /// <param name="userId">The id of the user to calcluate the balance for</param>
    /// <returns>The usable balance</returns>
    Task<decimal> CalculateUsableBalanceAsync(EntityKey<User> userId);

    /// <summary>
    /// Asynchronously and atomically calculates the usable balance of a user
    /// </summary>
    /// <param name="user">The id of the user to calcluate the balance for</param>
    /// <returns>The usable balance</returns>
    Task<decimal> UsableBalanceAsync(EntityKey<User> user);

    /// <summary>
    /// Asynchronously creates a pending transaction from a user to another
    /// This method does not write to the database nor does it use transactions (unsafe to use
    /// outside a transaction)
    /// </summary>
    /// <param name="fromId">The id of the user to transfer from</param>
    /// <param name="toId">The id of the user to transfer to</param>
    /// <param name="amount">The amount to transfer</param>
    /// <returns>The created transaction</returns>
    Task<Transaction> TransferUncomittedAsync(EntityKey<User> fromId, EntityKey<User> toId, decimal amount);

    /// <summary>
    /// Asynchronously creates a pending transaction from a user to another
    /// </summary>
    /// <param name="fromId">The id of the user to transfer from</param>
    /// <param name="toId">The id of the user to transfer to</param>
    /// <param name="amount">The amount to transfer</param>
    /// <returns>The created transaction</returns>
    Task<Transaction> TransferAsync(EntityKey<User> fromId, EntityKey<User> toId, decimal amount);

    /// <summary>
    /// Asynchronously adds a deposit financial record
    /// </summary>
    /// <param name="user">The id of the user which to add the record for</param>
    /// <param name="amount">The amount of the deposit</param>
    /// <param name="note">An optional note left by the user</param>
    /// <returns></returns>
    /// <exception cref="InvalidDepositAmountException"></exception>
    /// <exception cref="PendingRequestExistsException"></exception>
    Task<FinancialRecord> DepositAsync(EntityKey<User> user, decimal amount, string? note = null);

    /// <summary>
    /// Asynchronously adds a withdrawal financial record
    /// </summary>
    /// <param name="user">The id of the user which to add the record for</param>
    /// <param name="amount">The amount to withdraw</param>
    /// <param name="note">An optional note left by the user</param>
    /// <returns></returns>
    /// <exception cref="InsufficientBalanceException"></exception>
    /// <exception cref="PendingRequestExistsException"></exception>
    Task<FinancialRecord> WithdrawAsync(EntityKey<User> user, decimal amount, string? note = null);
}