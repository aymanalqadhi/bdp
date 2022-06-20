using BDP.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class ReservationTypeConfiguration : EntityTypeConfiguration<Reservation>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<Reservation> builder)
    {
        base.Configure(builder);

        // relationships
        builder
            .HasOne(s => s.Variant)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}