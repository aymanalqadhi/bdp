using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class CategoryTypeConfiguration : EntityTypeConfiguration<Category>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        // indeces
        builder.HasIndex(c => c.Name).IsUnique();
    }
}