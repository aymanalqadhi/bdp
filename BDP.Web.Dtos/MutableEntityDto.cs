using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A base data transfer object based on <see cref="EntityDto{TEntity}"/> with an
/// additional modification date
/// </summary>
/// <typeparam name="TEntity">The entity type</typeparam>
public abstract class MutableEntityDto<TEntity> : EntityDto<TEntity>
    where TEntity : AuditableEntity<TEntity>
{
    /// <summary>
    /// Gets or sets the modifying date of the entity
    /// </summary>
    public DateTime ModifiedAt { get; set; }
}