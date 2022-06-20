using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class UserTypeConfiguration : EntityTypeConfiguration<User>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        // Indeces
        builder.HasIndex(u => u.Username).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
    }
}