using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class FinancialRecordTypeConfiguration : EntityTypeConfiguration<FinancialRecord>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<FinancialRecord> builder)
    {
        base.Configure(builder);

        // field precision
        builder
            .Property(s => s.Amount)
            .HasPrecision(18, 6);

        // relationships
        builder
            .HasOne(t => t.MadeBy)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(r => r.Verification)
            .WithOne(v => v.FinancialRecord)
            .HasForeignKey<FinancialRecordVerification>(v => v.FinancialRecordId);
    }
}