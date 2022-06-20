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

    private readonly IUsersService _usersSvc;
    private readonly ISellablesService _sellablesSvc;
    private readonly ISellableReviewsService _sellableReviewsSvc;
    private readonly IMapper _mapper;

    #endregion Private fileds

    #region Ctors

    public SellablesController(
        IUsersService usersSvc,
        ISellablesService sellablesSvc,
        ISellableReviewsService sellableReviewsSvc,
        IMapper mapper)
    {
        _sellablesSvc = sellablesSvc;
        _usersSvc = usersSvc;
        _sellableReviewsSvc = sellableReviewsSvc;
        _mapper = mapper;
    }

    #endregion Ctors

    #region Actions

    [HttpGet]
    public async Task<IActionResult> GetByUserPaged([Required] string username, [FromQuery] PagingParameters paging)
    {
        var user = await _usersSvc.GetByUsername(username).FirstAsync();
        var ret = _sellablesSvc.GetForAsync(user)
            .PageDescending(paging.Page, paging.PageLength)
            .Include(p => p.OfferedBy)
            .Include(p => p.Attachments)
            .Map(_mapper, typeof(SellableDto))
            .AsAsyncEnumerable();

        return Ok(ret);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Search([Required] string query, string? username, [FromQuery] PagingParameters paging)
    {
        var searchResult = username != null
            ? _sellablesSvc.SearchForAsync(await _usersSvc.GetByUsername(username).FirstAsync(), query)
            : _sellablesSvc.SearchAsync(query);

        var ret = searchResult
            .Page(paging.Page, paging.PageLength)
            .Map(_mapper, typeof(SellableDto))
            .AsAsyncEnumerable();

        return Ok(ret);
    }

    [HttpGet("{id}/reviews")]
    public async Task<IActionResult> GetReviewsPaged(Guid id, [FromQuery] PagingParameters paging)
    {
        var item = await _sellablesSvc.GetByIdAsync(id);
        var ret = _sellableReviewsSvc.GetForAsync(item)
            .PageDescending(paging.Page, paging.PageLength)
            .Map<SellableReview, SellableReviewDto>(_mapper)
            .AsAsyncEnumerable();

        return Ok(ret);
    }

    [HttpGet("{id}/my-review")]
    [Authorize]
    public async Task<IActionResult> MyReview(Guid id)
    {
        var user = await _usersSvc.GetByUsername(User.GetUsername()).FirstAsync();
        var item = await _sellablesSvc.GetByIdAsync(id);
        var ret = await _sellableReviewsSvc.GetReviewForUser(item, user);

        return Ok(ret is not null ? _mapper.Map<SellableReviewDto>(ret) : null);
    }

    [HttpGet("{id}/review-info")]
    public async Task<IActionResult> ReviewInfo(Guid id)
    {
        var item = await _sellablesSvc.GetByIdAsync(id);
        return Ok(await _sellableReviewsSvc.ReviewInfoForAsync(item));
    }

    [HttpGet("{id}/can-review")]
    [Authorize]
    public async Task<IActionResult> CanReview(Guid id)
    {
        var user = await _usersSvc.GetByUsername(User.GetUsername()).FirstAsync();
        var item = await _sellablesSvc.GetByIdAsync(id);

        return Ok(new
        {
            Result = await _sellableReviewsSvc.CanReviewAsync(item, user)
        });
    }

    [HttpPost("{id}/[action]")]
    [IsCustomer]
    public async Task<IActionResult> Review(Guid id, [FromBody] SellableReviewRequest form)
    {
        var user = await _usersSvc.GetByUsername(User.GetUsername()).FirstAsync();
        var item = await _sellablesSvc.GetByIdAsync(id);
        var ret = await _sellableReviewsSvc.ReviewAsync(item, user, form.Rating, form.Comment);

        return Ok(_mapper.Map<SellableReviewDto>(ret));
    }

    #endregion Actions
}