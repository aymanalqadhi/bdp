namespace BDP.Domain.Entities;

public record EntityKey<TEntity, TValue>(TValue Value)
{
    /// <inheritdoc/>
    public override string? ToString() => Value?.ToString();
}

public sealed record EntityKey<TEntity>(Guid Id) : EntityKey<TEntity, Guid>(Id);