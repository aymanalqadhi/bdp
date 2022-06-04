using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services;

using System.Linq.Expressions;

namespace BDP.Application.App;

public class FinancialRecordsService : IFinancialRecordsService
{
    #region Private fields

    private readonly IUnitOfWork _uow;
    private readonly IAttachmentsService _attachmentsSvc;

    #endregion Private fields

    #region Ctors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit-of-work of the application</param>
    /// <param name="attachmentsSvc">The attachment managment service of the application</param>
    public FinancialRecordsService(IUnitOfWork uow, IAttachmentsService attachmentsSvc)
    {
        _uow = uow;
        _attachmentsSvc = attachmentsSvc;
    }

    #endregion Ctors

    #region Public mehtods

    /// <inheritdoc/>
    public IAsyncEnumerable<FinancialRecord> ForUserAsync(
        User user,
        Expression<Func<FinancialRecord, object>>[]? includes = null)
    {
        var query = _uow.FinancialRecords.Query();

        if (includes is not null)
            query = query.IncludeAll(includes);

        return query
            .Where(f => f.MadeBy.Id == user.Id)
            .AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<FinancialRecord> ForUserAsync(
        int page,
        int pageSize,
        User user,
        bool descOrder = false,
        Expression<Func<FinancialRecord, object>>[]? includes = null)
    {
        var query = _uow.FinancialRecords.Query();

        if (includes is not null)
            query = query.IncludeAll(includes);

        return query
            .Where(f => f.MadeBy.Id == user.Id)
            .Page(page, pageSize)
            .AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<FinancialRecord> PendingAsync(
        int page,
        int pageSize,
        bool descOrder = false,
        Expression<Func<FinancialRecord, object>>[]? includes = null)
    {
        var query = _uow.FinancialRecords.Query();

        if (includes is not null)
            query = query.IncludeAll(includes);

        return query
            .Where(f => f.Verification == null)
            .Page(page, pageSize)
            .AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public async Task<decimal> TotalUsableAsync(User user)
    {
        decimal total = 0;

        await foreach (var record in ForUserAsync(
            user,
            includes: new Expression<Func<FinancialRecord, object>>[] { r => r.Verification! }))
        {
            if (record.Verification?.Outcome == FinancialRecordVerificationOutcome.Accepted
                || record.Verification is null && record.Amount < 0)
            {
                total += record.Amount;
            }
        }

        return total;
    }

    /// <inheritdoc/>
    public async Task<FinancialRecordVerification> VerifyAsync(
        int recordId,
        User verifiedBy,
        FinancialRecordVerificationOutcome outcome,
        string? notes = null,
        IUploadFile? document = null)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        if (await _uow.FinancialRecords.Query().GetOrNullAsync(recordId) is var record &&
            record is null)
        {
            throw new NotFoundException($"no records were found with the id {recordId}");
        }

        if (await _uow.FinancialRecordVerficiations.Query().AnyAsync(v => v.FinancialRecord.Id == record.Id))
            throw new FinancialRecordAlreadyVerifiedException(record);

        var verification = new FinancialRecordVerification
        {
            Outcome = outcome,
            FinancialRecord = record,
            VerifiedBy = verifiedBy,
            Document = document is not null ? await _attachmentsSvc.SaveAsync(document) : null,
            Notes = notes,
        };

        _uow.FinancialRecordVerficiations.Add(verification);
        await _uow.CommitAsync(tx);

        return verification;
    }

    #endregion Public mehtods
}