using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class TransactionTypeConfiguration : EntityTypeConfiguration<Transaction>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<Transaction> builder)
    {
        base.Configure(builder);
    }
}
