using BDP.Domain.Entities;
using BDP.Domain.Repositories;

using System.Linq.Expressions;

namespace BDP.Domain.Services;

public interface IFinancialRecordsService
{
    /// <summary>
    /// Asynchrnously gets financial records for a user
    /// </summary>
    /// <param name="userId">The id of the user to get financial records for</param>
    /// <returns></returns>
    IQueryBuilder<FinancialRecord> ForUserAsync(EntityKey<User> userId);

    /// <summary>
    /// Asynchronously gets pending financial records for a specific user
    /// </summary>
    /// <returns></returns>
    IQueryBuilder<FinancialRecord> PendingAsync();

    /// <summary>
    /// Asynchrnounsly gets the total balance from financial records
    /// </summary>
    /// <param name="userId">The id of the user to get the total balance for</param>
    /// <returns></returns>
    Task<decimal> TotalUsableAsync(EntityKey<User> userId);

    /// <summary>
    /// Asynchronously verifies a financial record
    /// </summary>
    /// <param name="userId">The id of the user to verify with</param>
    /// <param name="recordId">The id of the record to verify</param>
    /// <param name="document">The document of the deposit/withdraw</param>
    /// <param name="notes">Addtional notes</param>
    /// <returns>The created verification object</returns>
    Task<FinancialRecordVerification> VerifyAsync(
        EntityKey<User> userId,
        EntityKey<FinancialRecord> recordId,
        string? notes = null,
        IUploadFile? document = null);

    /// <summary>
    /// Asynchronously declines a financial record
    /// </summary>
    /// <param name="userId">The id of the user to verify with</param>
    /// <param name="recordId">The id of the record to verify</param>
    /// <param name="document">The document of the deposit/withdraw</param>
    /// <param name="notes">Addtional notes</param>
    /// <returns>The created verification object</returns>
    Task<FinancialRecordVerification> DeclineAsync(
        EntityKey<User> userId,
        EntityKey<FinancialRecord> recordId,
        string? notes = null,
        IUploadFile? document = null);
}