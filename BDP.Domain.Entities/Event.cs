namespace BDP.Domain.Entities;

public class Event : AuditableEntity
{
    /// <summary>
    /// Gets or sets the event title
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the event description
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the progress of the event
    /// </summary>
    public double Progress { get; set; }

    /// <summary>
    /// Gets or sets the date/time at which the event takes place
    /// </summary>
    public DateTime TakesPlaceAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of pictures of this event
    /// </summary>
    public ICollection<Attachment> Pictures { get; set; } = new List<Attachment>();

    /// <summary>
    /// Gets or sets the collection of purchases associated with this event
    /// </summary>
    public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    /// <summary>
    /// Gets or sets the type of the event
    /// </summary>
    public EventType Type { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who created the event
    /// </summary>
    public User CreatedBy { get; set; } = null!;
}