using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class TransactionTypeConfiguration : EntityTypeConfiguration<Transaction>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<Transaction> builder)
    {
        base.Configure(builder);

        // indeces
        builder.HasIndex(t => t.ConfirmationToken);

        // field precision
        builder
            .Property(s => s.Amount)
            .HasPrecision(18, 6);

        // relationships
        builder
            .HasOne(t => t.From)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(t => t.To)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(t => t.Confirmation)
            .WithOne(c => c.Transaction)
            .HasForeignKey<TransactionConfirmation>(t => t.TransactionId);
    }
}