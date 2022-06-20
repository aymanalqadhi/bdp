using BDP.Domain.Entities;

namespace BDP.Domain.Repositories;

/// <summary>
/// An interface to represent the functionality of a repository for the 
/// <see cref="TransactionConfirmation"/> entity
/// </summary>
public interface ITransactionConfirmationsRepository : IRepository<TransactionConfirmation>
{
}
