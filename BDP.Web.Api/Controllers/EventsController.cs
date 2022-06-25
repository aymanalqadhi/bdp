using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Web.Api.Auth.Attributes;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Parameters;
using BDP.Web.Dtos.Requests;

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    #region Private fields

    private readonly IEventsService _eventsSvc;
    private readonly IMapper _mapper;

    #endregion Private fields

    #region Ctors

    public EventsController(
        IEventsService eventsSvc,
        IMapper mapper)
    {
        _eventsSvc = eventsSvc;
        _mapper = mapper;
    }

    #endregion Ctors

    #region Actions

    [HttpGet("{eventId}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] EntityKey<Event> eventId)
        => Ok(_mapper.Map<EventDto>(await _eventsSvc.GetEvents().FindAsync(eventId)));

    [HttpGet("types")]
    [Authorize]
    public IAsyncEnumerable<EventTypeDto> GetEventTypes()
    {
        var ret = _eventsSvc.GetEventTypes()
            .Map<EventType, EventTypeDto>(_mapper)
            .AsAsyncEnumerable();

        return ret;
    }

    [HttpGet]
    [Authorize]
    public IAsyncEnumerable<EventDto> GetEvents(
        [Required][FromQuery] EntityKey<User> userId,
        [FromQuery] PagingParameters paging)
    {
        var ret = _eventsSvc.ForUser(userId)
            .PageDescending(paging.Page, paging.PageLength)
            .Include(e => e.Pictures)
            .Include(e => e.Type)
            .Include(e => e.OwnedBy)
            .Map<Event, EventDto>(_mapper)
            .AsAsyncEnumerable();

        return ret;
    }

    [HttpPost]
    [IsCustomer]
    public async Task<IActionResult> Create([FromBody] CreateEventRequest form)
    {
        var ret = await _eventsSvc.CreateAsync(
            User.GetId(),
            form.EventTypeId,
            form.Title,
            form.Description,
            form.TakesPlaceAt);

        return Ok(_mapper.Map<EventDto>(ret));
    }

    [HttpPatch("{eventId}")]
    [IsCustomer]
    public async Task<IActionResult> Update(
        [FromRoute] EntityKey<Event> eventId,
        [FromBody] UpdateEventRequest form)
    {
        var type = await _eventsSvc.GetEventTypes().FindAsync(form.EventTypeId);
        var ret = await _eventsSvc.UpdateAsync(
            User.GetId(),
            eventId,
            type.Id,
            form.Title,
            form.Description,
            form.TakesPlaceAt
        );

        return Ok(_mapper.Map<EventDto>(ret));
    }

    [HttpDelete("{eventId}")]
    [IsCustomer]
    public async Task<IActionResult> Delete([FromRoute] EntityKey<Event> eventId)
    {
        await _eventsSvc.RemoveAsync(User.GetId(), eventId);

        return Ok();
    }

    [HttpPost("{eventId}/images")]
    [IsCustomer]
    public async Task<IActionResult> AddImage(
        [FromRoute] EntityKey<Event> eventId,
        [FromForm] AddImageToEventRequest form)
    {
        await _eventsSvc.AddImageAsync(User.GetId(), eventId, new WebUploadFile(form.Image));

        return Ok();
    }

    [HttpPut("{eventId}/progress")]
    [IsCustomer]
    public async Task<IActionResult> UpdateProgress(
        [FromRoute] EntityKey<Event> eventId,
        [FromBody] UpdateEventProgressRequess form)
    {
        await _eventsSvc.UpdateProgressAsync(User.GetId(), eventId, form.Progress);

        return Ok();
    }

    #endregion Actions
}