using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class FinancialRecordTypeConfiguration : EntityTypeConfiguration<FinancialRecord>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<FinancialRecord> builder)
    {
        base.Configure(builder);
    }
}