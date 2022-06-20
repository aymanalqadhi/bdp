using AutoMapper;
using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Web.Api.Auth.Attributes;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Parameters;
using BDP.Web.Dtos.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    #region Private fields

    private readonly IUsersService _usersSvc;
    private readonly IPurchasesService _puchasesSvc;
    private readonly IEventsService _eventsSvc;
    private readonly IMapper _mapper;

    #endregion Private fields

    #region Ctors

    public EventsController(
        IUsersService usersSvc,
        IPurchasesService puchasesSvc,
        IEventsService eventsSvc,
        IMapper mapper)
    {
        _usersSvc = usersSvc;
        _puchasesSvc = puchasesSvc;
        _eventsSvc = eventsSvc;
        _mapper = mapper;
    }

    #endregion Ctors

    #region Actions

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
        => Ok(_mapper.Map<EventDto>(await _eventsSvc.GetByIdAsync(id)));

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
    public IAsyncEnumerable<EventDto> GetEvents([Required] Guid userId, [FromQuery] PagingParameters paging)
    {
        var ret = _eventsSvc.ForUserAsync(userId)
            .PageDescending(paging.Page, paging.PageLength)
            .Include(e => e.Pictures)
            .Include(e => e.Type)
            .Include(e => e.CreatedBy)
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

    [HttpPatch("{id}")]
    [IsCustomer]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEventRequest form)
    {
        var @event = await _eventsSvc.GetByIdAsync(id);

        if (@event.CreatedBy.Id != User.GetId())
            return Unauthorized(new { message = "you do not own the event" });

        var type = await _eventsSvc.GetTypeByIdAsync(form.EventTypeId);
        var ret = await _eventsSvc.UpdateAsync(
            @event.Id,
            type.Id,
            form.Title,
            form.Description,
            form.TakesPlaceAt
        );

        return Ok(_mapper.Map<EventDto>(ret));
    }

    [HttpDelete("{id}")]
    [IsCustomer]
    public async Task<IActionResult> Delete(Guid id)
    {
        // TODO:
        // move ownership logic to the service

        var @event = await _eventsSvc.GetByIdAsync(id);

        if (@event.CreatedBy.Id != User.GetId())
            return Unauthorized(new { message = "you do not own the event" });

        await _eventsSvc.RemoveAsync(@event.Id);

        return Ok();
    }

    [HttpGet("{id}/purchases")]
    [IsCustomer]
    public async Task<IActionResult> GetAssociatePurchase(Guid id)
    {
        var user = await _usersSvc.GetByUsername(User.GetUsername()).FirstAsync();
        var @event = await _eventsSvc.GetByIdAsync(id);

        if (@event.CreatedBy.Id != user.Id)
            return Unauthorized(new { message = "you do not own the event" });

        return Ok(@event.Purchases.Select(_mapper.Map<PurchaseDto>));
    }

    [HttpPost("{id}/purchases")]
    [IsCustomer]
    public async Task<IActionResult> AssociatePurchase(
        Guid id,
        [FromBody] AssociatePurchaseWithEventRequest form)
    {
        // TODO:
        // move ownership logic to the service

        var @event = await _eventsSvc.GetByIdAsync(id);

        if (@event.CreatedBy.Id != User.GetId())
            return Unauthorized(new { message = "you do not own the event" });

        var purchase = await _puchasesSvc.GetById(form.PurchaseId);

        if (purchase.Transaction.From.Id != User.GetId())
            return Unauthorized(new { message = "you did not make the purchase" });

        await _eventsSvc.AssociatePurchaseAsync(@event.Id, purchase.Id);

        return Ok();
    }

    [HttpPost("{id}/images")]
    [IsCustomer]
    public async Task<IActionResult> AddImage(Guid id, [FromForm] AddImageToEventRequest form)
    {
        // TODO:
        // move ownership logic to the service

        var @event = await _eventsSvc.GetByIdAsync(id);

        if (@event.CreatedBy.Id != User.GetId())
            return Unauthorized(new { message = "you do not own the event" });

        if (@event.Pictures.Count >= 12)
            return Unauthorized(new { message = "you have reached the limit of images" });

        await _eventsSvc.AddImageAsync(@event.Id, new WebUploadFile(form.Image));

        return Ok();
    }

    [HttpPut("{id}/progress")]
    [IsCustomer]
    public async Task<IActionResult> UpdateProgress(
        Guid id,
        [FromBody] UpdateEventProgressRequess form)
    {
        // TODO:
        // move ownership logic to the service

        var @event = await _eventsSvc.GetByIdAsync(id);

        if (@event.CreatedBy.Id != User.GetId())
            return Unauthorized(new { message = "you do not own the event" });

        await _eventsSvc.UpdateProgressAsync(@event.Id, form.Progress);

        return Ok();
    }

    #endregion Actions
}