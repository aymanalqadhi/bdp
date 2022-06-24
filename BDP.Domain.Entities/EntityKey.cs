using System.ComponentModel;

namespace BDP.Domain.Entities;

public record EntityKey<TEntity, TValue>(TValue Value)
{
    /// <inheritdoc/>
    public override string? ToString() => Value?.ToString();
}

[TypeConverter(typeof(EntityKeyConverter))]
public record EntityKey<TEntity>(Guid Value) : EntityKey<TEntity, Guid>(Value);