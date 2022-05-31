using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="FinancialRecordVerification"/>
/// </summary>
public sealed class FinancialRecordVerificationsRepository
    : EfRepository<FinancialRecordVerification, Validator<FinancialRecordVerification>>,
    IFinancialRecordVerificationsRepository
{
    public FinancialRecordVerificationsRepository(
        DbSet<FinancialRecordVerification> set) : base(set)
    {
    }
}