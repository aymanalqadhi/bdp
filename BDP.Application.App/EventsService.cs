using BDP.Application.App.Exceptions;
using BDP.Domain.Repositories;
using BDP.Domain.Entities;
using BDP.Domain.Services;
using BDP.Domain.Repositories.Extensions;

using System.Linq.Expressions;

namespace BDP.Application.App;

public class EventsService : IEventsService
{
    #region Private fields

    private readonly IUnitOfWork _uow;
    private readonly IAttachmentsService _attachmentsSvc;

    #endregion Private fields

    #region Ctors

    public EventsService(IUnitOfWork uow, IAttachmentsService attachmentsSvc)
    {
        _uow = uow;
        _attachmentsSvc = attachmentsSvc;
    }

    #endregion Ctors

    #region Public methods

    /// <inheritdoc/>
    public async Task<Event> GetByIdAsync(EntityKey<Event> id)
    {
        var @event = await _uow.Events.Query()
            .Include(e => e.Purchases)
            .Include(e => e.Pictures)
            .Include(e => e.Type)
            .Include(e => e.CreatedBy)
            .FirstAsync(e => e.Id == id);

        return @event;
    }

    /// <inheritdoc/>
    public Task<EventType> GetTypeByIdAsync(EntityKey<EventType> id)
        => _uow.EventTypes.Query().FirstAsync(e => e.Id == id);

    /// <inheritdoc/>
    public IQueryBuilder<EventType> GetEventTypes()
        => _uow.EventTypes.Query();

    /// <inheritdoc/>
    public IQueryBuilder<Event> ForUserAsync(EntityKey<User> userId)
        => _uow.Events.Query().Where(e => e.CreatedBy.Id == userId);

    /// <inheritdoc/>
    public async Task<Event> CreateAsync(
        EntityKey<User> userId,
        EntityKey<EventType> typeId,
        string title,
        string description,
        DateTime takesPlaceAt)
    {
        var user = await _uow.Users.Query().FindAsync(userId);
        var type = await _uow.EventTypes.Query().FindAsync(typeId);

        var @event = new Event
        {
            CreatedBy = user,
            Type = type,
            Title = title,
            Description = description,
            TakesPlaceAt = takesPlaceAt,
            Progress = 0,
        };

        _uow.Events.Add(@event);
        await _uow.CommitAsync();

        return @event;
    }

    /// <inheritdoc/>
    public async Task<Event> UpdateAsync(
        EntityKey<Event> eventId,
        EntityKey<EventType> typeId,
        string title,
        string description,
        DateTime takesPlaceAt)
    {
        var @event = await _uow.Events.Query().FindAsync(eventId);
        var type = await _uow.EventTypes.Query().FindAsync(typeId);

        @event.Type = type;
        @event.Title = title;
        @event.Description = description;
        @event.TakesPlaceAt = takesPlaceAt;

        _uow.Events.Update(@event);
        await _uow.CommitAsync();

        return @event;
    }

    /// <inheritdoc/>
    public async Task RemoveAsync(EntityKey<Event> eventId)
    {
        var @event = await _uow.Events.Query().FindAsync(eventId);

        _uow.Events.Remove(@event);
        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task AssociatePurchaseAsync(EntityKey<Event> eventId, EntityKey<Purchase> purchaseId)
    {
        var @event = await _uow.Events.Query().FindAsync(eventId);
        var purchase = await _uow.Purchases.Query().FindAsync(purchaseId);

        @event.Purchases.Add(purchase);
        _uow.Events.Update(@event);

        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task AddImageAsync(EntityKey<Event> eventId, IUploadFile image)
    {
        var @event = await _uow.Events.Query().FindAsync(eventId);
        var attachment = await _attachmentsSvc.SaveAsync(image);

        @event.Pictures.Add(attachment);
        _uow.Events.Update(@event);

        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task UpdateProgressAsync(EntityKey<Event> eventId, double progress)
    {
        if (progress > 1 || progress < 0)
            throw new InvalidRangeException(progress, 0, 1);

        var @event = await _uow.Events.Query().FindAsync(eventId);

        @event.Progress = progress;

        _uow.Events.Update(@event);
        await _uow.CommitAsync();
    }

    #endregion Public methods
}