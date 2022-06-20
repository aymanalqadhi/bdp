using BDP.Domain.Entities;
using BDP.Domain.Repositories;

using System.Linq.Expressions;

namespace BDP.Domain.Services;

public interface IEventsService
{
    /// <summary>
    /// Asynchronously gets an event by id
    /// </summary>
    /// <param name="id">The id of the event</param>
    /// <returns></returns>
    Task<Event> GetByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously gets an event type by id
    /// </summary>
    /// <param name="id">The id of the event type</param>
    /// <returns></returns>
    Task<EventType> GetTypeByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously gets event types
    /// </summary>
    /// <returns></returns>
    IQueryBuilder<EventType> GetEventTypes();

    /// <summary>
    /// Asynchronosly gets events for a user, limited with pagination
    /// </summary>
    /// <param name="userId">The id of the user which to get the events for</param>
    /// <returns></returns>
    IQueryBuilder<Event> ForUserAsync(Guid userId);

    /// <summary>
    /// Asynchronously creates a new event
    /// </summary>
    /// <param name="userId">The id of the user which to create the event for</param>
    /// <param name="typeid">The id of the type of the event</param>
    /// <param name="title">the title of the event</param>
    /// <param name="description">The description of the event</param>
    /// <param name="takesPlaceAt">The date at which the event takes place</param>
    /// <returns>The created event</returns>
    Task<Event> CreateAsync(
        Guid userId,
        Guid typeId,
        string title,
        string description,
        DateTime takesPlaceAt);

    /// <summary>
    /// Asynchronsoulsy updates a event
    /// </summary>
    /// <param name=eventId">The id of the event to update</param>
    /// <param name="typeId">The id of the new type for the event</param>
    /// <param name="title">The new title of the event</param>
    /// <param name="description">The new description of the event</param>
    /// <param name="takesPlaceAt">The new taking place date</param>
    /// <returns></returns>
    Task<Event> UpdateAsync(
        Guid eventId,
        Guid typeType,
        string title,
        string description,
        DateTime takesPlaceAt
    );

    /// <summary>
    /// Asynchronously removes an event
    /// </summary>
    /// <param name=eventId"></param>
    /// <returns></returns>
    Task RemoveAsync(Guid eventId);

    /// <summary>
    /// Asynchronously associates a purchase with the passed event
    /// </summary>
    /// <param name=eventId">The id of the event which to associate the purchase with</param>
    /// <param name="purchaseId">The id of the purchase to be associated</param>
    /// <returns></returns>
    Task AssociatePurchaseAsync(Guid eventId, Guid purchaseId);

    /// <summary>
    /// Asynchronously adds an image to the image
    /// </summary>
    /// <param name=eventId">The image which to add the image to</param>
    /// <param name="image">The image to be added</param>
    /// <returns></returns>
    Task AddImageAsync(Guid eventId, IUploadFile image);

    /// <summary>
    /// Asynchronously updates the progress of an event
    /// </summary>
    /// <param name=eventId">The id of the event which to update</param>
    /// <param name="progress">The new progress value</param>
    /// <returns></returns>
    Task UpdateProgressAsync(Guid eventId, double progress);
}