using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Web.Api.Auth.Attributes;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Requests;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

namespace BDP.Web.Api.Controllers;

[Route("api/products/{productId}/variants/{variantId}/reservationWindows")]
[ApiController]
public class ReservationWindowsController : ControllerBase
{
    #region Fields

    private readonly IMapper _mapper;
    private readonly IReservationWindowsService _reservationWindowsSvc;

    #endregion Fields

    #region Public Constructors

    public ReservationWindowsController(
        IMapper mapper,
        IReservationWindowsService reservationWindowsSvc)
    {
        _mapper = mapper;
        _reservationWindowsSvc = reservationWindowsSvc;
    }

    #endregion Public Constructors

    [HttpGet]
    [IsProvider]
    public IAsyncEnumerable<ReservationWindowDto> GetReservationWindows(
        [FromRoute] EntityKey<Product> _,
        [FromRoute] EntityKey<ProductVariant> variantId)
    {
        return _reservationWindowsSvc.GetReservationWindows(variantId)
            .OrderDescending()
            .Map<ReservationWindow, ReservationWindowDto>(_mapper)
            .AsAsyncEnumerable();
    }

    [HttpPost]
    [IsProvider]
    public async Task<IActionResult> Create(
        [FromRoute] EntityKey<Product> _,
        [FromRoute] EntityKey<ProductVariant> variantId,
        [FromBody] CreateReservationWindowRequest form)
    {
        var batch = await _reservationWindowsSvc.AddAsync(
            User.GetId(),
            variantId,
            (Weekday)form.AvailableDays,
            TimeOnly.FromTimeSpan(form.Start),
            TimeOnly.FromTimeSpan(form.End));

        return Ok(_mapper.Map<StockBatchDto>(batch));
    }

    [HttpDelete("{windowId}")]
    [IsProvider]
    public async Task<IActionResult> Remove(
        [FromRoute] EntityKey<Product> _,
        [FromRoute] EntityKey<ReservationWindow> windowId)
    {
        await _reservationWindowsSvc.RemoveAsync(User.GetId(), windowId);

        return Ok();
    }
}