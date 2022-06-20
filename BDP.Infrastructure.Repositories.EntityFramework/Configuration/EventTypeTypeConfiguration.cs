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

        builder.HasData(
            new() { Name = "Wedding" },
            new() { Name = "Birth Day" },
            new() { Name = "Engagement Party" },
            new() { Name = "Graduation Ceremony" },
            new() { Name = "Graduation Party" },
            new() { Name = "Other" }
        );
    }
}