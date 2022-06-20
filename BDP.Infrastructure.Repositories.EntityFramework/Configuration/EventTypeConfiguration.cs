using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class EventTypeConfiguration : EntityTypeConfiguration<Event>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<Event> builder)
    {
        base.Configure(builder);
    }
}
