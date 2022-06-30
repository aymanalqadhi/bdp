﻿using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services.Exceptions;

namespace BDP.Domain.Services;

/// <summary>
/// A service to manage application financial records (deposits & withdraws)
/// </summary>
public interface IFinancialRecordsService
{
    /// <summary>
    /// Gets financial records for a user
    /// </summary>
    /// <param name="userId">The id of the user to get financial records for</param>
    /// <param name="pendingOnly">If true, only pending records will be fetched</param>
    /// <returns></returns>
    IQueryBuilder<FinancialRecord> ForUser(EntityKey<User> userId, bool pendingOnly = false);

    /// <summary>
    /// Asynchronously gets pending financial records for a specific user
    /// </summary>
    /// <param name="userId">The id of the user to get pending records with</param>
    /// <returns></returns>
    Task<IQueryBuilder<FinancialRecord>> PendingAsync(EntityKey<User> userId);

    /// <summary>
    /// Asynchrnounsly gets the total balance from financial records
    /// </summary>
    /// <param name="userId">The id of the user to get the total balance for</param>
    /// <returns></returns>
    Task<decimal> CalculateUsableAsync(EntityKey<User> userId);

    /// <summary>
    /// Asynchronously approves a financial record
    /// </summary>
    /// <param name="userId">The id of the user to verify with</param>
    /// <param name="recordId">The id of the record to verify</param>
    /// <param name="document">The document of the deposit/withdraw</param>
    /// <param name="notes">Addtional notes</param>
    /// <returns>The created verification object</returns>
    /// <exception cref="InsufficientPermissionsException"></exception>
    /// <exception cref="FinancialRecordAlreadyVerifiedException"></exception>
    Task<FinancialRecordVerification> Approve(
        EntityKey<User> userId,
        EntityKey<FinancialRecord> recordId,
        string? notes = null,
        IUploadFile? document = null);

    /// <summary>
    /// Asynchronously rejects a financial record
    /// </summary>
    /// <param name="userId">The id of the user to verify with</param>
    /// <param name="recordId">The id of the record to verify</param>
    /// <param name="document">The document of the deposit/withdraw</param>
    /// <param name="notes">Addtional notes</param>
    /// <returns>The created verification object</returns>
    /// <exception cref="InsufficientPermissionsException"></exception>
    /// <exception cref="FinancialRecordAlreadyVerifiedException"></exception>
    Task<FinancialRecordVerification> RejectAsync(
        EntityKey<User> userId,
        EntityKey<FinancialRecord> recordId,
        string? notes = null,
        IUploadFile? document = null);
}