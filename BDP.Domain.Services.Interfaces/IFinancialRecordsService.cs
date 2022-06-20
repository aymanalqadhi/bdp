using BDP.Domain.Entities;
using BDP.Domain.Repositories;

using System.Linq.Expressions;

namespace BDP.Domain.Services;

public interface IFinancialRecordsService
{
    /// <summary>
    /// Asynchrnously gets financial records for a user
    /// </summary>
    /// <param name="user">The user to get financial records for</param>
    /// <returns></returns>
    IQueryBuilder<FinancialRecord> ForUserAsync(User user);

    /// <summary>
    /// Asynchronously gets pending financial records for a specific user
    /// </summary>
    /// <returns></returns>
    IQueryBuilder<FinancialRecord> PendingAsync();

    /// <summary>
    /// Asynchrnounsly gets the total balance from financial records
    /// </summary>
    /// <param name="user">The user to get the total balance for</param>
    /// <returns></returns>
    Task<decimal> TotalUsableAsync(User user);

    /// <summary>
    /// Asynchronously verifies a financial record
    /// </summary>
    /// <param name="record">The record to verify</param>
    /// <param name="outcome">The outcome of the verification</param>
    /// <param name="verifiedBy">The user to verify with</param>
    /// <param name="document">The document of the deposit/withdraw</param>
    /// <param name="notes">Addtional notes</param>
    /// <returns></returns>
    Task<FinancialRecordVerification> VerifyAsync(
        Guid recordId,
        User verifiedBy,
        FinancialRecordVerificationOutcome outcome,
        string? notes = null,
        IUploadFile? document = null);
}