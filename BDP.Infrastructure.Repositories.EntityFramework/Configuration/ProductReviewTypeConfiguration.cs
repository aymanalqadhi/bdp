using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class ProductReviewTypeConfiguration : EntityTypeConfiguration<ProductReview>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<ProductReview> builder)
    {
        base.Configure(builder);

        // relationships
        builder
            .HasOne(r => r.LeftBy)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .OnDelete(DeleteBehavior.Cascade);
    }
}