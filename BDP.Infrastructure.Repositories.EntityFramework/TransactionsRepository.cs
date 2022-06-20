using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="Transaction"/>
/// </summary>
public sealed class TransactionsRepository :
    EfRepository<Transaction, TransactionValidator>, ITransactionsRepository
{
    public TransactionsRepository(DbSet<Transaction> set) : base(set)
    {
    }
}