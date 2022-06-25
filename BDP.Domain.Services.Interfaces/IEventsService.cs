using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services.Exceptions;

namespace BDP.Domain.Services;

/// <summary>
/// A service to manage the events of users
/// </summary>
public interface IEventsService
{
    #region Public Methods

    /// <summary>
    /// Asynchronously adds an image to the image
    /// </summary>
    /// <param name="userId">The id of the user which to create the event for</param>
    /// <param name=eventId">The image which to add the image to</param>
    /// <param name="image">The image to be added</param>
    /// <returns></returns>
    /// <exception cref="InsufficientPermissionsException"></exception>
    /// <exception cref="EventPicturesLimitReachedException"></exception>
    Task AddImageAsync(EntityKey<User> userId, EntityKey<Event> eventId, IUploadFile image);

    /// <summary>
    /// Asynchronously creates a new event
    /// </summary>
    /// <param name="userId">The id of the user which to create the event for</param>
    /// <param name="typeId">The id of the type of the event</param>
    /// <param name="title">the title of the event</param>
    /// <param name="description">The description of the event</param>
    /// <param name="takesPlaceAt">The date at which the event takes place</param>
    /// <returns>The created event</returns>
    /// <exception cref="InsufficientPermissionsException"></exception>
    Task<Event> CreateAsync(
        EntityKey<User> userId,
        EntityKey<EventType> typeId,
        string title,
        string description,
        DateTime takesPlaceAt);

    /// <summary>
    /// Gets events for a user, limited with pagination
    /// </summary>
    /// <param name="userId">The id of the user which to get the events for</param>
    /// <returns></returns>
    IQueryBuilder<Event> ForUser(EntityKey<User> userId);

    /// <summary>
    /// Gets all events
    /// </summary>
    /// <returns></returns>
    IQueryBuilder<Event> GetEvents();

    /// <summary>
    /// Gets event types
    /// </summary>
    /// <returns></returns>
    IQueryBuilder<EventType> GetEventTypes();

    /// <summary>
    /// Asynchronously removes an event
    /// </summary>
    /// <param name="userId">The id of the user who owns the event</param>
    /// <param name=eventId">The id of the event to remove</param>
    /// <returns></returns>
    /// <exception cref="InsufficientPermissionsException"></exception>
    Task RemoveAsync(EntityKey<User> userId, EntityKey<Event> eventId);

    /// <summary>
    /// Asynchronsoulsy updates a event
    /// </summary>
    /// <param name="userId">The id of the user who owns the event</param>
    /// <param name=eventId">The id of the event to update</param>
    /// <param name="typeId">The id of the new type for the event</param>
    /// <param name="title">The new title of the event</param>
    /// <param name="description">The new description of the event</param>
    /// <param name="takesPlaceAt">The new taking place date</param>
    /// <returns>The updated event</returns>
    /// <exception cref="InsufficientPermissionsException"></exception>
    Task<Event> UpdateAsync(
        EntityKey<User> userId,
        EntityKey<Event> eventId,
        EntityKey<EventType> typeId,
        string title,
        string description,
        DateTime takesPlaceAt
    );

    /// <summary>
    /// Asynchronously updates the progress of an event
    /// </summary>
    /// <param name="userId">The id of the user who owns the event</param>
    /// <param name=eventId">The id of the event which to update</param>
    /// <param name="progress">The new progress value</param>
    /// <returns></returns>
    /// <exception cref="InvalidRangeException"></exception>
    /// <exception cref="InsufficientPermissionsException"></exception>
    Task UpdateProgressAsync(EntityKey<User> userId, EntityKey<Event> eventId, double progress);

    #endregion Public Methods
}