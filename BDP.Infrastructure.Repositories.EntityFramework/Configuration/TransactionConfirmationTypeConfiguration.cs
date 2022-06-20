using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class TransactionConfirmationTypeConfiguration : EntityTypeConfiguration<TransactionConfirmation>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<TransactionConfirmation> builder)
    {
        base.Configure(builder);
    }
}