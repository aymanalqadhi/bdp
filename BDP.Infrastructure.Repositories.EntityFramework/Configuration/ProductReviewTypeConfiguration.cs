using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class ProductReviewTypeConfiguration : EntityTypeConfiguration<ProductReview>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<ProductReview> builder)
    {
        base.Configure(builder);
    }
}