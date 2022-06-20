using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class FinancialRecordVerificationTypeConfiguration : EntityTypeConfiguration<FinancialRecordVerification>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<FinancialRecordVerification> builder)
    {
        base.Configure(builder);
    }
}
