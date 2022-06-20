using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class AttachmentTypeConfiguration : EntityTypeConfiguration<Attachment>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<Attachment> builder)
    {
        base.Configure(builder);
    }
}
