using BDP.Domain.Entities;

namespace BDP.Domain.Services;

public interface IFinanceService
{
    /// <summary>
    /// Asynchronously calculates the virtual balance of a user
    /// </summary>
    /// <param name="user">The user to calcluate the balance for</param>
    /// <returns>The virutal balance</returns>
    Task<decimal> CalculateTotalVirtualAsync(User user);

    /// <summary>
    /// Asynchronously calculates the usable balance of a user
    /// </summary>
    /// <param name="user">The user to calcluate the balance for</param>
    /// <returns>The usable balance</returns>
    Task<decimal> CalculateTotalUsableAsync(User user);

    /// <summary>
    /// Asynchronously and atomically calculates the virtual balance of a user
    /// </summary>
    /// <param name="user">The user to calcluate the balance for</param>
    /// <returns>The virutal balance</returns>
    Task<decimal> TotalVirtualAsync(User user);

    /// <summary>
    /// Asynchronously and atomically calculates the usable balance of a user
    /// </summary>
    /// <param name="user">The user to calcluate the balance for</param>
    /// <returns>The usable balance</returns>
    Task<decimal> TotalUsableAsync(User user);

    /// <summary>
    /// Asynchronously creates a pending transaction from a user to another
    /// This method does not write to the database
    /// </summary>
    /// <param name="from">The user to transfer from</param>
    /// <param name="to">The user to transfer to</param>
    /// <param name="amount">The amount to transfer</param>
    /// <returns>The created transaction</returns>
    Task<Transaction> TransferUncomittedAsync(User from, User to, decimal amount);

    /// <summary>
    /// Asynchronously creates a pending transaction from a user to another
    /// </summary>
    /// <param name="from">The user to transfer from</param>
    /// <param name="to">The user to transfer to</param>
    /// <param name="amount">The amount to transfer</param>
    /// <returns>The created transaction</returns>
    Task<Transaction> TransferAsync(User from, User to, decimal amount);

    /// <summary>
    /// Asynchronously adds a deposit financial record
    /// </summary>
    /// <param name="user">The user which to add the record for</param>
    /// <param name="amount">The amount of the deposit</param>
    /// <param name="note">An optional note left by the user</param>
    /// <returns></returns>
    Task<FinancialRecord> DepositAsync(User user, decimal amount, string? note = null);

    /// <summary>
    /// Asynchronously adds a withdrawal financial record
    /// </summary>
    /// <param name="user">The user which to add the record for</param>
    /// <param name="amount">The amount to withdraw</param>
    /// <param name="note">An optional note left by the user</param>
    /// <returns></returns>
    Task<FinancialRecord> WithdrawAsync(User user, decimal amount, string? note = null);
}