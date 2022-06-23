using BDP.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public sealed class ProductVariantTypeConfiguration : EntityTypeConfiguration<ProductVariant>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        base.Configure(builder);

        // indeces
        builder.HasIndex(v => v.Name);

        // relationships
        builder
            .HasOne(s => s.Product)
            .WithMany(p => p.Variants)
            .OnDelete(DeleteBehavior.Restrict);

        // field precision
        builder
            .Property(s => s.Price)
            .HasPrecision(18, 6);
    }
}