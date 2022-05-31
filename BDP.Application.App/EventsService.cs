using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services;

using System.Linq.Expressions;

namespace BDP.Application.App;

public class EventsService : IEventsService
{
    #region Private fields

    private readonly ILegacyUnitOfWork _uow;
    private readonly IAttachmentsService _attachmentsSvc;

    #endregion Private fields

    #region Ctors

    public EventsService(ILegacyUnitOfWork uow, IAttachmentsService attachmentsSvc)
    {
        _uow = uow;
        _attachmentsSvc = attachmentsSvc;
    }

    #endregion Ctors

    #region Public methods

    /// <inheritdoc/>
    public async Task<Event> GetByIdAsync(long id)
    {
        var ret = await _uow.Events.FirstOrDefaultAsync(
            e => e.Id == id,
            includes: new Expression<Func<Event, object>>[]
            {
                e => e.Purchases,
                e => e.Pictures,
                e => e.Type,
                e => e.CreatedBy,
            }
        );

        if (ret == null)
            throw new NotFoundException($"no events were found with id #{id}");

        return ret;
    }

    /// <inheritdoc/>
    public async Task<EventType> GetTypeByIdAsync(long id)
    {
        var ret = await _uow.EventTypes.FirstOrDefaultAsync(e => e.Id == id);

        if (ret == null)
            throw new NotFoundException($"no event types were found with id #{id}");

        return ret;
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<EventType> GetEventTypes()
        => _uow.EventTypes.GetAllAsync();

    /// <inheritdoc/>
    public IAsyncEnumerable<Event> ForUserAsync(
        int page,
        int pageSize,
        User user,
        Expression<Func<Event, object>>[]? includes = null)
    {
        if (page <= 0 || pageSize <= 0 || pageSize > 1000)
            throw new InvalidPaginationParametersException(page, pageSize);

        return _uow.Events.FilterAsync(
            page,
            pageSize,
            e => e.CreatedBy.Id == user.Id,
            includes: includes,
            descOrder: true
        );
    }

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