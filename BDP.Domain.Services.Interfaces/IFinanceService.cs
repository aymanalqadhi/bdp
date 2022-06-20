using BDP.Domain.Entities;

namespace BDP.Domain.Services;

public interface IFinanceService
{
    /// <summary>
    /// Asynchronously calculates the virtual balance of a user
    /// </summary>
    /// <param name="userId">The id of the user to calcluate the balance for</param>
    /// <returns>The virutal balance</returns>
    Task<decimal> CalculateTotalVirtualAsync(Guid userId);

    /// <summary>
    /// Asynchronously calculates the usable balance of a user
    /// </summary>
    /// <param name="userId">The id of the user to calcluate the balance for</param>
    /// <returns>The usable balance</returns>
    Task<decimal> CalculateTotalUsableAsync(Guid userId);

    /// <summary>
    /// Asynchronously and atomically calculates the virtual balance of a user
    /// </summary>
    /// <param name="userId">The id of the user to calcluate the balance for</param>
    /// <returns>The virutal balance</returns>
    Task<decimal> TotalVirtualAsync(Guid user);

    /// <summary>
    /// Asynchronously and atomically calculates the usable balance of a user
    /// </summary>
    /// <param name="user">The id of the user to calcluate the balance for</param>
    /// <returns>The usable balance</returns>
    Task<decimal> TotalUsableAsync(Guid user);

    /// <summary>
    /// Asynchronously creates a pending transaction from a user to another
    /// This method does not write to the database
    /// </summary>
    /// <param name="fromId">The id of the user to transfer from</param>
    /// <param name="toId">The id of the user to transfer to</param>
    /// <param name="amount">The amount to transfer</param>
    /// <returns>The created transaction</returns>
    Task<Transaction> TransferUncomittedAsync(Guid fromId, Guid toId, decimal amount);

    /// <summary>
    /// Asynchronously creates a pending transaction from a user to another
    /// </summary>
    /// <param name="fromId">The id of the user to transfer from</param>
    /// <param name="toId">The id of the user to transfer to</param>
    /// <param name="amount">The amount to transfer</param>
    /// <returns>The created transaction</returns>
    Task<Transaction> TransferAsync(Guid fromId, Guid toId, decimal amount);

    /// <summary>
    /// Asynchronously adds a deposit financial record
    /// </summary>
    /// <param name="user">The id of the user which to add the record for</param>
    /// <param name="amount">The amount of the deposit</param>
    /// <param name="note">An optional note left by the user</param>
    /// <returns></returns>
    Task<FinancialRecord> DepositAsync(Guid user, decimal amount, string? note = null);

    /// <summary>
    /// Asynchronously adds a withdrawal financial record
    /// </summary>
    /// <param name="user">The id of the user which to add the record for</param>
    /// <param name="amount">The amount to withdraw</param>
    /// <param name="note">An optional note left by the user</param>
    /// <returns></returns>
    Task<FinancialRecord> WithdrawAsync(Guid user, decimal amount, string? note = null);
}