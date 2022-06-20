using BDP.Domain.Entities;

namespace BDP.Web.Dtos;

/// <summary>
/// A data-transfer object for <see cref="EventType"/>
/// </summary>
public sealed class EventTypeDto : EntityDto<EventType>
{
    /// <summary>
    /// Gets or sets the name of the event type
    /// </summary>
    public string Name { get; set; } = null!;
}