using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services;

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
    public async Task<Event> GetByIdAsync(Guid id)
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
    public Task<EventType> GetTypeByIdAsync(Guid id)
        => _uow.EventTypes.Query().FirstAsync(e => e.Id == id);

    /// <inheritdoc/>
    public IQueryBuilder<EventType> GetEventTypes()
        => _uow.EventTypes.Query();

    /// <inheritdoc/>
    public IQueryBuilder<Event> ForUserAsync(User user)
        => _uow.Events.Query().Where(e => e.CreatedBy.Id == user.Id);

    /// <inheritdoc/>
    public async Task<Event> CreateAsync(
        User user,
        EventType type,
        string title,
        string description,
        DateTime takesPlaceAt)
    {
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
        Event @event,
        EventType type,
        string title,
        string description,
        DateTime takesPlaceAt)
    {
        @event.Type = type;
        @event.Title = title;
        @event.Description = description;
        @event.TakesPlaceAt = takesPlaceAt;

        _uow.Events.Update(@event);
        await _uow.CommitAsync();

        return @event;
    }

    /// <inheritdoc/>
    public async Task RemoveAsync(Event e)
    {
        _uow.Events.Remove(e);
        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task AssociatePurchaseAsync(Event @event, Purchase purchase)
    {
        @event.Purchases.Add(purchase);
        _uow.Events.Update(@event);

        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task AddImageAsync(Event @event, IUploadFile image)
    {
        var attachment = await _attachmentsSvc.SaveAsync(image);

        @event.Pictures.Add(attachment);
        _uow.Events.Update(@event);

        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task UpdateProgressAsync(Event @event, double progress)
    {
        if (progress > 1 || progress < 0)
            throw new InvalidRangeException(progress, 0, 1);

        @event.Progress = progress;

        _uow.Events.Update(@event);
        await _uow.CommitAsync();
    }

    #endregion Public methods
}