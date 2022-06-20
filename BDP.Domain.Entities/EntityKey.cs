namespace BDP.Domain.Entities;

public sealed record EntityKey<TEntity>(Guid Id)
{
    /// <inheritdoc/>
    public override string ToString() => Id.ToString();
}