using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public sealed class ReservationWindowTypeConfiguration : EntityTypeConfiguration<ReservationWindow>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<ReservationWindow> builder)
    {
        base.Configure(builder);

        // type conversion
        builder.Property(w => w.Start)
            .HasConversion(s => s.ToTimeSpan(), t => TimeOnly.FromTimeSpan(t));
        builder.Property(w => w.End)
            .HasConversion(s => s.ToTimeSpan(), t => TimeOnly.FromTimeSpan(t));
    }
}