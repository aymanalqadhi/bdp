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
public class SellablesController : ControllerBase
{
    #region Private fileds

    private readonly ISellablesService _sellablesSvc;
    private readonly ISellableReviewsService _sellableReviewsSvc;
    private readonly IMapper _mapper;

    #endregion Private fileds

    #region Ctors

    public SellablesController(
        ISellablesService sellablesSvc,
        ISellableReviewsService sellableReviewsSvc,
        IMapper mapper)
    {
        _sellablesSvc = sellablesSvc;
        _sellableReviewsSvc = sellableReviewsSvc;
        _mapper = mapper;
    }

    #endregion Ctors

    #region Actions

    [HttpGet]
    public IAsyncEnumerable<object> GetByUserPaged([Required] Guid userId, [FromQuery] PagingParameters paging)
    {
        var ret = _sellablesSvc.GetForAsync(userId)
            .PageDescending(paging.Page, paging.PageLength)
            .Include(p => p.OfferedBy)
            .Include(p => p.Attachments)
            .Map(_mapper, typeof(SellableDto))
            .AsAsyncEnumerable();

        return ret;
    }

    [HttpGet("[action]")]
    public IAsyncEnumerable<object> Search([Required] string query, string? username, [FromQuery] PagingParameters paging)
    {
        var searchResult = username != null
            ? _sellablesSvc.SearchForAsync(User.GetId(), query)
            : _sellablesSvc.SearchAsync(query);

        var ret = searchResult
            .Page(paging.Page, paging.PageLength)
            .Map(_mapper, typeof(SellableDto))
            .AsAsyncEnumerable();

        return ret;
    }

    [HttpGet("{id}/reviews")]
    public IAsyncEnumerable<SellableReviewDto> GetReviewsPaged(Guid id, [FromQuery] PagingParameters paging)
    {
        var ret = _sellableReviewsSvc.GetForAsync(id)
            .PageDescending(paging.Page, paging.PageLength)
            .Map<SellableReview, SellableReviewDto>(_mapper)
            .AsAsyncEnumerable();

        return ret;
    }

    [HttpGet("{id}/my-review")]
    [Authorize]
    public async Task<IActionResult> MyReview(Guid id)
    {
        var ret = await _sellableReviewsSvc.GetReviewForUserAsync(id, User.GetId());

        return Ok(ret is not null ? _mapper.Map<SellableReviewDto>(ret) : null);
    }

    [HttpGet("{id}/review-info")]
    public async Task<IActionResult> ReviewInfo(Guid id)
        => Ok(await _sellableReviewsSvc.ReviewInfoForAsync(id));

    [HttpGet("{id}/can-review")]
    [Authorize]
    public async Task<IActionResult> CanReview(Guid id)
    {
        var ret = await _sellableReviewsSvc.CanReviewAsync(User.GetId(), id);
        return Ok(new { Result = ret });
    }

    [HttpPost("{id}/[action]")]
    [IsCustomer]
    public async Task<IActionResult> Review(Guid id, [FromBody] SellableReviewRequest form)
    {
        var ret = await _sellableReviewsSvc.ReviewAsync(User.GetId(), id, form.Rating, form.Comment);

        return Ok(_mapper.Map<SellableReviewDto>(ret));
    }

    #endregion Actions
}