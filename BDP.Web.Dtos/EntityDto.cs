using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A base data transfer object class
/// </summary>
/// <typeparam name="TEntity">The entity type</typeparam>
public abstract class EntityDto<TEntity>
    where TEntity : AuditableEntity<TEntity>
{
    /// <summary>
    /// Gets or sets the id of the entity
    /// </summary>
    public EntityKey<TEntity> Id { get; set; } = new EntityKey<TEntity>(Guid.NewGuid());

    /// <summary>
    /// Gets or sets the creation date of the entity
    /// </summary>
    public DateTime CreatedAt { get; set; }
}