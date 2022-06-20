namespace BDP.Domain.Entities;

public record EntityKey<TEntity, TValue>(TValue Id)
{
    /// <inheritdoc/>
    public override string? ToString() => Id?.ToString();
}

public sealed record EntityKey<TEntity>(Guid Id) : EntityKey<TEntity, Guid>(Id);