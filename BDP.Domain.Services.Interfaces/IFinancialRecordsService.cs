using BDP.Domain.Entities;
using System.Linq.Expressions;

namespace BDP.Domain.Services.Interfaces;

public interface IFinancialRecordsService
{
    /// <summary>
    /// Asynchrnously gets all financial records for a user
    /// </summary>
    /// <param name="user">The user to get financial records for</param>
    /// <param name="includes">Additional includes</param>
    /// <returns></returns>
    IAsyncEnumerable<FinancialRecord> ForUserAsync(
        User user,
        Expression<Func<FinancialRecord, object>>[]? includes = null);

    /// <summary>
    /// Asynchrnously gets all financial records for a user, limited by pagination
    /// </summary>
    /// <param name="page">The page to fetch</param>
    /// <param name="pageSize">The size of the page to fetch</param>
    /// <param name="descOrder">Whether to order the rows descendantly or not</param>
    /// <param name="user">The user to get financial records for</param>
    /// <param name="includes">Additional includes</param>
    /// <returns></returns>
    public IAsyncEnumerable<FinancialRecord> ForUserAsync(
        int page,
        int pageSize,
        User user,
        bool descOrder = false,
        Expression<Func<FinancialRecord, object>>[]? includes = null);

    /// <summary>
    /// Asynchronously gets all pending financial records for a specific user
    /// </summary>
    /// <param name="page">The page to fetch</param>
    /// <param name="pageSize">The size of the page to fetch</param>
    /// <param name="descOrder">Whether to order the rows descendantly or not</param>
    /// <param name="user">The user to get financial records for</param>
    /// <param name="includes">Additional includes</param>
    /// <returns></returns>
    IAsyncEnumerable<FinancialRecord> PendingAsync(
        int page,
        int pageSize,
        bool descOrder = false,
        Expression<Func<FinancialRecord, object>>[]? includes = null);

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
        int recordId,
        User verifiedBy,
        FinancialRecordVerificationOutcome outcome,
        string? notes = null,
        IUploadFile? document = null);
}
