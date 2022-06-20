using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class ServiceTypeConfiguration : EntityTypeConfiguration<Service>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<Service> builder)
    {
        base.Configure(builder);

        // field precision
        builder
            .Property(s => s.Price)
            .HasPrecision(18, 6);
    }
}