namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent an event type
/// </summary>
public class EventType : AuditableEntity<EventType>
{
    /// <summary>
    /// Gets or sets the name of the event type
    /// </summary>
    public string Name { get; set; } = null!;
}