using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class ConfirmationTypeConfiguration : EntityTypeConfiguration<Confirmation>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<Confirmation> builder)
    {
        base.Configure(builder);
    }
}