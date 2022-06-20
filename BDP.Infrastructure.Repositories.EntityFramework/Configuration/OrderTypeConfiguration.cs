using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class OrderTypeConfiguration : EntityTypeConfiguration<Order>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        // relationships
        builder
             .HasOne(o => o.Variant)
             .WithMany()
             .OnDelete(DeleteBehavior.Restrict);
    }
}