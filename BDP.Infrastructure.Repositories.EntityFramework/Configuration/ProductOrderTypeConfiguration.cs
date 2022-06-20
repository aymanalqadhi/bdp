using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class ProductOrderTypeConfiguration : EntityTypeConfiguration<ProductOrder>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<ProductOrder> builder)
    {
        base.Configure(builder);

        // relationships
        builder
             .HasOne(o => o.Item)
             .WithMany()
             .OnDelete(DeleteBehavior.Restrict);
    }
}