namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a domain entity with autition
/// </summary>
public abstract class AuditableEntity<T> where T : class
{
    /// <summary>
    /// Gets or sets the id of the entity
    /// </summary>
    public EntityKey<T> Id { get; set; } = new EntityKey<T>(Guid.NewGuid());

    /// <summary>
    /// Gets or sets the creation date of the entity
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the modifying date of the entity
    /// </summary>
    public DateTime ModifiedAt { get; set; }
}