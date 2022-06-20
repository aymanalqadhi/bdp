using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class LogTagTypeConfiguration : EntityTypeConfiguration<LogTag>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<LogTag> builder)
    {
        base.Configure(builder);
    }
}