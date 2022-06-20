using BDP.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class UserProfileTypeConfiguration : EntityTypeConfiguration<UserProfile>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        base.Configure(builder);

        // indeces
        builder.HasIndex(p => p.UserId).IsUnique();
    }
}