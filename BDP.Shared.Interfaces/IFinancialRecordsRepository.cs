using BDP.Domain.Entities;

namespace BDP.Domain.Repositories;

/// <summary>
/// An interface to represent the functionality of a repository for the
/// <see cref="FinancialRecord"/> entity
/// </summary>
public interface IFinancialRecordsRepository : IRepository<FinancialRecord>
{
}