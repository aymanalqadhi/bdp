namespace BDP.Domain.Entities;

/// <summary>
/// A recrod to represent keys for the <see cref="EventType"/> entity
/// </summary>
/// <param name="Id">The id field of the key</param>
public sealed record EventTypeKey(Guid Id) : EntityKey<EventType>(Id);

/// <summary>
/// A class to represent an event type
/// </summary>
public class EventType : AuditableEntity
{
    /// <summary>
    /// Gets or sets the id of the event type
    /// </summary>
    public EventTypeKey Id { get; set; } = new EventTypeKey(Guid.NewGuid());

    /// <summary>
    /// Gets or sets the name of the event type
    /// </summary>
    public string Name { get; set; } = null!;
}