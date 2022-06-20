using BDP.Domain.Entities;
using System.Linq.Expressions;

namespace BDP.Domain.Services;

public interface IEventsService
{
    /// <summary>
    /// Asynchronously gets an event by id
    /// </summary>
    /// <param name="id">The id of the event</param>
    /// <returns></returns>
    Task<Event> GetByIdAsync(long id);

    /// <summary>
    /// Asynchronously gets an event type by id
    /// </summary>
    /// <param name="id">The id of the event type</param>
    /// <returns></returns>
    Task<EventType> GetTypeByIdAsync(long id);

    /// <summary>
    /// Asynchronously gets event types
    /// </summary>
    /// <returns></returns>
    IAsyncEnumerable<EventType> GetEventTypes();

    /// <summary>
    /// Asynchronosly gets events for a user, limited with pagination
    /// </summary>
    /// <param name="user">The user which to get the events for</param>
    /// <param name="page">The page to get</param>
    /// <param name="pageSize">The pages size</param>
    /// <param name="includes">Additional includes</param>
    /// <returns></returns>
    IAsyncEnumerable<Event> ForUserAsync(
       int page,
       int pageSize,
       User user,
       Expression<Func<Event, object>>[]? includes = null);

    /// <summary>
    /// Asynchronously creates a new event
    /// </summary>
    /// <param name="user">The user which to create the event for</param>
    /// <param name="type">The type of the event</param>
    /// <param name="title">the title of the event</param>
    /// <param name="description">The description of the event</param>
    /// <param name="takesPlaceAt">The date at which the event takes place</param>
    /// <returns>The created event</returns>
    Task<Event> CreateAsync(
        User user,
        EventType type,
        string title,
        string description,
        DateTime takesPlaceAt);

    /// <summary>
    /// Asynchronsoulsy updates a event
    /// </summary>
    /// <param name="event">The event to update</param>
    /// <param name="type">The new type for the event</param>
    /// <param name="title">The new title of the event</param>
    /// <param name="description">The new description of the event</param>
    /// <param name="takesPlaceAt">The new taking place date</param>
    /// <returns></returns>
    Task<Event> UpdateAsync(
        Event @event,
        EventType type,
        string title,
        string description,
        DateTime takesPlaceAt
    );

    /// <summary>
    /// Asynchronously removes an event
    /// </summary>
    /// <param name="event"></param>
    /// <returns></returns>
    Task RemoveAsync(Event @event);

    /// <summary>
    /// Asynchronously associates a purchase with the passed event
    /// </summary>
    /// <param name="event">The event which to associate the purchase with</param>
    /// <param name="purchase">The purchase to be associated</param>
    /// <returns></returns>
    Task AssociatePurchaseAsync(Event @event, Purchase purchase);

    /// <summary>
    /// Asynchronously adds an image to the image
    /// </summary>
    /// <param name="event">The image which to add the image to</param>
    /// <param name="image">The image to be added</param>
    /// <returns></returns>
    Task AddImageAsync(Event @event, IUploadFile image);

    /// <summary>
    /// Asynchronously updates the progress of an event
    /// </summary>
    /// <param name="event">The event which to update</param>
    /// <param name="progress">The new progress value</param>
    /// <returns></returns>
    Task UpdateProgressAsync(Event @event, double progress);
}