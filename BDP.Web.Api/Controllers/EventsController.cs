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
using System.Linq.Expressions;

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
    public async Task<IActionResult> GetById(int id)
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
    public async Task<IActionResult> GetEvents([Required] string username, [FromQuery] PagingParameters paging)
    {
        var user = await _usersSvc.GetByUsernameAsync(username);
        var ret = _eventsSvc.ForUserAsync(user)
            .PageDescending(paging.Page, paging.PageLength)
            .Include(e => e.Pictures)
            .Include(e => e.Type)
            .Include(e => e.CreatedBy)
            .Map<Event, EventDto>(_mapper)
            .AsAsyncEnumerable();

        return Ok(ret);
    }

    [HttpPost]
    [IsCustomer]
    public async Task<IActionResult> Create([FromBody] CreateEventRequest form)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        var type = await _eventsSvc.GetTypeByIdAsync(form.EventTypeId);
        var ret = await _eventsSvc.CreateAsync(
            user, type, form.Title, form.Description, form.TakesPlaceAt);

        return Ok(_mapper.Map<EventDto>(ret));
    }

    [HttpPatch("{id}")]
    [IsCustomer]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEventRequest form)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        var @event = await _eventsSvc.GetByIdAsync(id);

        if (@event.CreatedBy.Id != user.Id)
            return Unauthorized(new { message = "you do not own the event" });

        var type = await _eventsSvc.GetTypeByIdAsync(form.EventTypeId);
        var ret = await _eventsSvc.UpdateAsync(
            @event,
            type,
            form.Title,
            form.Description,
            form.TakesPlaceAt
        );

        return Ok(_mapper.Map<EventDto>(ret));
    }

    [HttpDelete("{id}")]
    [IsCustomer]
    public async Task<IActionResult> Delete(int id)
    {
        // TODO:
        // move ownership logic to the service

        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        var @event = await _eventsSvc.GetByIdAsync(id);

        if (@event.CreatedBy.Id != user.Id)
            return Unauthorized(new { message = "you do not own the event" });

        await _eventsSvc.RemoveAsync(@event);

        return Ok();
    }

    [HttpGet("{id}/purchases")]
    [IsCustomer]
    public async Task<IActionResult> GetAssociatePurchase(int id)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        var @event = await _eventsSvc.GetByIdAsync(id);

        if (@event.CreatedBy.Id != user.Id)
            return Unauthorized(new { message = "you do not own the event" });

        return Ok(@event.Purchases.Select(_mapper.Map<PurchaseDto>));
    }

    [HttpPost("{id}/purchases")]
    [IsCustomer]
    public async Task<IActionResult> AssociatePurchase(
        int id,
        [FromBody] AssociatePurchaseWithEventRequest form)
    {
        // TODO:
        // move ownership logic to the service

        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        var @event = await _eventsSvc.GetByIdAsync(id);

        if (@event.CreatedBy.Id != user.Id)
            return Unauthorized(new { message = "you do not own the event" });

        var purchase = await _puchasesSvc.GetById(form.PurchaseId);

        if (purchase.Transaction.From.Id != user.Id)
            return Unauthorized(new { message = "you did not make the purchase" });

        await _eventsSvc.AssociatePurchaseAsync(@event, purchase);

        return Ok();
    }

    [HttpPost("{id}/images")]
    [IsCustomer]
    public async Task<IActionResult> AddImage(long id, [FromForm] AddImageToEventRequest form)
    {
        // TODO:
        // move ownership logic to the service

        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        var @event = await _eventsSvc.GetByIdAsync(id);

        if (@event.CreatedBy.Id != user.Id)
            return Unauthorized(new { message = "you do not own the event" });

        if (@event.Pictures.Count >= 12)
            return Unauthorized(new { message = "you have reached the limit of images" });

        await _eventsSvc.AddImageAsync(@event, new WebUploadFile(form.Image));

        return Ok();
    }

    [HttpPut("{id}/progress")]
    [IsCustomer]
    public async Task<IActionResult> UpdateProgress(
        long id,
        [FromBody] UpdateEventProgressRequess form)
    {
        // TODO:
        // move ownership logic to the service

        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        var @event = await _eventsSvc.GetByIdAsync(id);

        if (@event.CreatedBy.Id != user.Id)
            return Unauthorized(new { message = "you do not own the event" });

        await _eventsSvc.UpdateProgressAsync(@event, form.Progress);

        return Ok();
    }

    #endregion Actions
}