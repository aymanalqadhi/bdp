namespace BDP.Domain.Entities;

public class EventType : AuditableEntity
{
    /// <summary>
    /// Gets or sets the name of the event type
    /// </summary>
    public string Name { get; set; } = null!;
}
