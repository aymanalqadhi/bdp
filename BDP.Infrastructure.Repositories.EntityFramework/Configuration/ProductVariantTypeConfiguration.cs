﻿using BDP.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public abstract class ProductVariantTypeConfiguration<TVariant> : EntityTypeConfiguration<TVariant>
    where TVariant : ProductVariant<TVariant>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<TVariant> builder)
    {
        base.Configure(builder);

        // indeces
        builder.HasIndex(v => v.Name);

        // relationships
        builder
            .HasOne(s => s.Product)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        // field precision
        builder
            .Property(s => s.Price)
            .HasPrecision(18, 6);
    }
}
