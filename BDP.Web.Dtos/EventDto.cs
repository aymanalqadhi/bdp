namespace BDP.Web.Dtos;

public class EventDto
{
    /// <summary>
    /// Gets or sets the id of the event
    /// </summary>
    public long Id { get; set; }

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
    /// Gets or sets the collection of picture urls of this event
    /// </summary>
    public IEnumerable<string> Pictures { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the type of the event
    /// </summary>
    public EventTypeDto Type { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who created the event
    /// </summary>
    public UserDto CreatedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the creation date of the event
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the 
    /// </summary>
    public DateTime ModifiedAt { get; set; }
}
