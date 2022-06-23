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
        [FromRoute] EntityKey<Product> productId,
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
        [FromRoute] EntityKey<Product> productId,
        [FromRoute] EntityKey<ProductVariant> variantId,
        [FromBody] CreateReservationWindowRequest form)
    {
        var batch = await _reservationWindowsSvc.AddAsync(
            variantId,
            (Weekday)form.AvailableDays,
            TimeOnly.FromTimeSpan(form.Start),
            TimeOnly.FromTimeSpan(form.End));

        return Ok(_mapper.Map<StockBatchDto>(batch));
    }

    [HttpDelete("{windowId}")]
    [IsProvider]
    public async Task<IActionResult> Remove(
        [FromRoute] EntityKey<Product> productId,
        [FromRoute] EntityKey<ProductVariant> variantId,
        [FromRoute] EntityKey<ReservationWindow> windowId)
    {
        // TODO:
        // Move ownership verification to services

        var batch = await _reservationWindowsSvc
            .GetReservationWindows(variantId)
            .Include(b => b.Variant)
            .Include(b => b.Variant.Product)
            .Include(b => b.Variant.Product.OfferedBy)
            .FindAsync(windowId);

        if (batch.Variant.Product.OfferedBy.Id != User.GetId())
            return Unauthorized(new { message = "you do not own the product" });

        await _reservationWindowsSvc.RemoveAsync(windowId);

        return Ok();
    }
}