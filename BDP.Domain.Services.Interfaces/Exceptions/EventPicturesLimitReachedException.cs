using BDP.Domain.Entities;

namespace BDP.Domain.Services.Exceptions;

/// <summary>
/// An exception to be thrown when an event reaches pictures count limit
/// </summary>
public class EventPicturesLimitReachedException : Exception
{
    #region Fields

    private readonly EntityKey<Event> _eventId;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="eventId">The id of the event reaching pictures limit</param>
    public EventPicturesLimitReachedException(EntityKey<Event> eventId)
        : base($"event #{eventId} reached maximum pictures limit")
    {
        _eventId = eventId;
    }

    #endregion Public Constructors

    #region Properties

    /// <summary>
    /// Gets the id of the event reaching pictures limit
    /// </summary>
    public EntityKey<Event> EventId => _eventId;

    #endregion Properties
}