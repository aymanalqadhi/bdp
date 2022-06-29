using BDP.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Repositories.EntityFramework.Configuration;

/// <inheritdoc/>
public class ProductTypeConfiguration : EntityTypeConfiguration<Product>
{
}