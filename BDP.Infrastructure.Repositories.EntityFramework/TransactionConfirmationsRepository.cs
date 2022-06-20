using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="TransactionConfirmation"/>
/// </summary>
public sealed class TransactionsConfirmationRepository
    : EfRepository<TransactionConfirmation, Validator<TransactionConfirmation>>,
    ITransactionConfirmationsRepository
{
    public TransactionsConfirmationRepository(
        DbSet<TransactionConfirmation> set) : base(set)
    {
    }
}
