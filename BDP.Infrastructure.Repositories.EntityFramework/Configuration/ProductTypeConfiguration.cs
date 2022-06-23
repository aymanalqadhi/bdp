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

        // relationships
        builder
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products);
    }
}