using BDP.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Requests;

public class CreateEventRequest
{
    /// <summary>
    /// Gets or sets the title of the event
    /// </summary>
    [Required]
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets description of the event
    /// </summary>
    [Required]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date at which the event takes place
    /// </summary>
    [Required]
    public DateTime TakesPlaceAt { get; set; }

    /// <summary>
    /// The id of the event type
    /// </summary>
    [Required]
    public EntityKey<EventType> EventTypeId { get; set; } = null!;
}