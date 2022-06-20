using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class ServiceReviewTypeConfiguration : EntityTypeConfiguration<ServiceReview>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<ServiceReview> builder)
    {
        base.Configure(builder);

        // relationships

        builder
            .HasOne(r => r.LeftBy)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
    }
}