using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class EventTypeTypeConfiguration : EntityTypeConfiguration<EventType>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<EventType> builder)
    {
        base.Configure(builder);
    }
}
