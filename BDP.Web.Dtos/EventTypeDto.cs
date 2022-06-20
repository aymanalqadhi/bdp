namespace BDP.Web.Dtos;

public class EventTypeDto
{
    /// <summary>
    /// Gets or sets the id of the event type
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the event type
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the creation date of the event type
    /// </summary>
    public DateTime CreatedAt { get; set; }
}