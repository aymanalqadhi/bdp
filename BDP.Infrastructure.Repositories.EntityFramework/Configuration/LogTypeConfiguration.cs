using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class LogTypeConfiguration : EntityTypeConfiguration<Log>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<Log> builder)
    {
        base.Configure(builder);
    }
}
