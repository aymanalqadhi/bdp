using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="FinancialRecord"/>
/// </summary>
public sealed class FinancialRecordsRepository :
    EfRepository<FinancialRecord, FinancialRecordValidator>, IFinancialRecordsRepository
{
    public FinancialRecordsRepository(DbSet<FinancialRecord> set) : base(set)
    {
    }
}
