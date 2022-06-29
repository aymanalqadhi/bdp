﻿using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BDP.Web.Dtos.Parameters;
using BDP.Domain.Entities;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PurchasesController : ControllerBase
{
    #region Private fields

    private readonly IMapper _mapper;
    private readonly IPurchasesService _purchasesSvc;

    #endregion Private fields

    #region Ctors

    public PurchasesController(
        IPurchasesService purchasesSvc,
        IMapper mapper)
    {
        _purchasesSvc = purchasesSvc;
        _mapper = mapper;
    }

    #endregion Ctors

    #region Actions

    [HttpGet("orders")]
    [Authorize]
    public IAsyncEnumerable<OrderDto> GetOrders([FromQuery] PagingParameters paging, bool pending = false)
    {
        var ret = _purchasesSvc.GetOrdersFor(User.GetId(), pending)
            .PageDescending(paging.Page, paging.PageLength)
            .Include(p => p.Payment)
            .Map<Order, OrderDto>(_mapper)
            .AsAsyncEnumerable();

        return ret;
    }

    [HttpGet("reservations")]
    [Authorize]
    public IAsyncEnumerable<ReservationDto> GetReservations([FromQuery] PagingParameters paging, bool pending = false)
    {
        var ret = _purchasesSvc.GetReservationsFor(User.GetId(), pending)
            .PageDescending(paging.Page, paging.PageLength)
            .Include(p => p.Payment)
            .Map<Reservation, ReservationDto>(_mapper)
            .AsAsyncEnumerable();

        return ret;
    }

    [HttpPost("{orderId}/[action]")]
    public async Task<IActionResult> EarlyOrderAccept(EntityKey<Order> orderId)
        => Ok(_mapper.Map<OrderDto>(await _purchasesSvc.EarlyOrderConfirmAsync(User.GetId(), orderId, true)));

    [HttpPost("{orderId}/[action]")]
    public async Task<IActionResult> EarlyOrderDecline(EntityKey<Order> orderId)
    => Ok(_mapper.Map<OrderDto>(await _purchasesSvc.EarlyOrderConfirmAsync(User.GetId(), orderId, false)));

    [HttpPost("{reservationId}/[action]")]
    public async Task<IActionResult> EarlyReservationAccept(EntityKey<Reservation> reservationId)
        => Ok(_mapper.Map<OrderDto>(await _purchasesSvc.EarlyResrvationConfirmAsync(User.GetId(), reservationId, true)));

    [HttpPost("{reservationId}/[action]")]
    public async Task<IActionResult> EarlyReservationDecline(EntityKey<Reservation> reservationId)
        => Ok(_mapper.Map<OrderDto>(await _purchasesSvc.EarlyResrvationConfirmAsync(User.GetId(), reservationId, false)));

    #endregion Actions
}