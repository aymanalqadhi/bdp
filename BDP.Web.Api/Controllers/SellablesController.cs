using AutoMapper;
using BDP.Domain.Services;
using BDP.Web.Api.Auth.Attributes;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SellablesController : ControllerBase
{
    #region Private fileds

    private readonly int _pageSize;

    private readonly IUsersService _usersSvc;
    private readonly ISellablesService _sellablesSvc;
    private readonly ISellableReviewsService _sellableReviewsSvc;
    private readonly IMapper _mapper;

    #endregion Private fileds

    #region Ctors

    public SellablesController(
        IConfigurationService configurationSvc,
        IUsersService usersSvc,
        ISellablesService sellablesSvc,
        ISellableReviewsService sellableReviewsSvc,
        IMapper mapper)
    {
        _sellablesSvc = sellablesSvc;
        _usersSvc = usersSvc;
        _sellableReviewsSvc = sellableReviewsSvc;
        _mapper = mapper;

        _pageSize = configurationSvc.GetInt("QuerySettings:DefaultPageSize");
    }

    #endregion Ctors

    #region Actions

    [HttpGet("page/{page}")]
    public async Task<IActionResult> GetByUserPaged(string username, int page)
    {
        var user = await _usersSvc.GetByUsernameAsync(username);
        var ret = await _sellablesSvc.GetForAsync(user, page, _pageSize).ToListAsync();

        return Ok(ret.Select((s) => _mapper.Map(s, s.GetType(), typeof(SellableDto))));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Search(string query, int page, string? username)
    {
        var ret = username != null
            ? await _sellablesSvc.SearchForAsync(
                 await _usersSvc.GetByUsernameAsync(username),
                 query,
                 page,
                 _pageSize).ToListAsync()
            : await _sellablesSvc.SearchAsync(query, page, _pageSize).ToListAsync();

        return Ok(ret.Select((s) => _mapper.Map(s, s.GetType(), typeof(SellableDto))));
    }

    [HttpGet("{id}/reviews/{page}")]
    public async Task<IActionResult> GetReviewsPaged(int id, int page)
    {
        var item = await _sellablesSvc.GetByIdAsync(id);
        var ret = await _sellableReviewsSvc.GetForAsync(item, page, _pageSize).ToListAsync();

        return Ok(ret.Select(_mapper.Map<SellableReviewDto>));
    }

    [HttpGet("{id}/my-review")]
    [Authorize]
    public async Task<IActionResult> MyReview(int id)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        var item = await _sellablesSvc.GetByIdAsync(id);
        var ret = await _sellableReviewsSvc.GetReviewForUser(item, user);

        return Ok(ret is not null ? _mapper.Map<SellableReviewDto>(ret) : null);
    }

    [HttpGet("{id}/review-info")]
    public async Task<IActionResult> ReviewInfo(int id)
    {
        var item = await _sellablesSvc.GetByIdAsync(id);
        return Ok(await _sellableReviewsSvc.ReviewInfoForAsync(item));
    }

    [HttpGet("{id}/can-review")]
    [Authorize]
    public async Task<IActionResult> CanReview(int id)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        var item = await _sellablesSvc.GetByIdAsync(id);

        return Ok(new
        {
            Result = await _sellableReviewsSvc.CanReviewAsync(item, user)
        });
    }

    [HttpPost("{id}/[action]")]
    [IsCustomer]
    public async Task<IActionResult> Review(int id, [FromBody] SellableReviewRequest form)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        var item = await _sellablesSvc.GetByIdAsync(id);
        var ret = await _sellableReviewsSvc.ReviewAsync(item, user, form.Rating, form.Comment);

        return Ok(_mapper.Map<SellableReviewDto>(ret));
    }

    #endregion Actions
}