﻿using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class ServiceReservationTypeConfiguration : EntityTypeConfiguration<ServiceReservation>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<ServiceReservation> builder)
    {
        base.Configure(builder);
    }
}