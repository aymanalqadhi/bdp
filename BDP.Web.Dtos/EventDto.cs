using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A data-transfer object for <see cref="Event"/>
/// </summary>
public sealed class EventDto : MutableEntityDto<Event>
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
    public ICollection<Uri> Pictures { get; set; } = new List<Uri>();

    /// <summary>
    /// Gets or sets the type of the event
    /// </summary>
    public EventTypeDto Type { get; set; } = null!;
}