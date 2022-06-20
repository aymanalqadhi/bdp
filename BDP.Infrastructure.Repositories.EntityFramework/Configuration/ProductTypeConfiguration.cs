using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class ProductTypeConfiguration : EntityTypeConfiguration<Product>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        // field precision
        builder
            .Property(s => s.Price)
            .HasPrecision(18, 6);
    }
}