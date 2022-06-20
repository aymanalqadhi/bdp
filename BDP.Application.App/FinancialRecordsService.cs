using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Domain.Services.Exceptions;

namespace BDP.Application.App;

/// <inheritdoc/>
public class FinancialRecordsService : IFinancialRecordsService
{
    #region Fields

    private readonly IAttachmentsService _attachmentsSvc;
    private readonly IUnitOfWork _uow;

    #endregion Fields

    #region Public Constructors

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

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public Task<FinancialRecordVerification> DeclineAsync(
        EntityKey<User> userId,
        EntityKey<FinancialRecord>
        recordId, string? notes = null,
        IUploadFile? document = null)
    {
        return FinalizeRecordAsync(userId, recordId, false, notes, document);
    }

    /// <inheritdoc/>
    public IQueryBuilder<FinancialRecord> ForUser(EntityKey<User> userId)
        => _uow.FinancialRecords.Query().Where(f => f.MadeBy.Id == userId);

    /// <inheritdoc/>
    public IQueryBuilder<FinancialRecord> Pending()
        => _uow.FinancialRecords.Query().Where(f => f.Verification == null);

    /// <inheritdoc/>
    public async Task<decimal> TotalUsableAsync(EntityKey<User> userId)
    {
        return await ForUser(userId)
            .Include(r => r.Verification!)
            .Where(r => (r.Verification != null && r.Verification!.IsApproved)
                      || r.Verification == null && r.Amount < 0)
            .AsAsyncEnumerable()
            .SumAsync(r => r.Amount);
    }

    /// <inheritdoc/>
    public Task<FinancialRecordVerification> VerifyAsync(
        EntityKey<User> userId,
        EntityKey<FinancialRecord> recordId,
        string? notes = null,
        IUploadFile? document = null)
    {
        return FinalizeRecordAsync(userId, recordId, true, notes, document);
    }

    #endregion Public Methods

    #region Private Methods

    /// <inheritdoc/>
    private async Task<FinancialRecordVerification> FinalizeRecordAsync(
        EntityKey<User> userId,
        EntityKey<FinancialRecord> recordId,
        bool isApproved,
        string? note = null,
        IUploadFile? document = null)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        var user = await _uow.Users.Query().FindAsync(userId);

        if (user.Role != UserRole.Admin)
        {
            throw new InsufficientPermissionsException(
                userId,
                $"user #{userId} is not an admin to confirm financial record #{recordId}");
        }

        var record = await _uow.FinancialRecords.Query()
            .Include(r => r.Verification!)
            .FindAsync(recordId);

        if (record.Verification is not null)
            throw new FinancialRecordAlreadyVerifiedException(record);

        var verification = new FinancialRecordVerification
        {
            IsApproved = isApproved,
            FinancialRecord = record,
            VerifiedBy = user,
            Document = document is not null ? await _attachmentsSvc.SaveAsync(document) : null,
            Note = note,
        };

        record.Verification = verification;

        _uow.FinancialRecordVerficiations.Add(verification);
        _uow.FinancialRecords.Update(record);

        await _uow.CommitAsync(tx);

        return verification;
    }

    #endregion Private Methods
}