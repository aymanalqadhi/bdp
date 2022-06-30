using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Parameters;
using BDP.Web.Dtos.Requests;
using BDP.Web.Api.Auth;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfilesController : ControllerBase
{
    #region Private fields

    private readonly IUserProfilesService _userProfilesSvc;
    private readonly IMapper _mapper;

    #endregion Private fields

    #region Ctros

    public ProfilesController(
        IUserProfilesService userProfilesSvc,
        IMapper mapper)
    {
        _userProfilesSvc = userProfilesSvc;
        _mapper = mapper;
    }

    #endregion Ctros

    #region Actions

    [HttpGet("{username}")]
    public async Task<IActionResult> GetByUsernameAsync(string username)
    {
        var ret = await _userProfilesSvc.GetByUsername(username)
            .Include(u => u.User)
            .Map<UserProfile, UserProfileDto>(_mapper)
            .FirstAsync();

        return Ok(ret);
    }

    [HttpGet("[action]")]
    public IAsyncEnumerable<UserProfileDto> Search(string query, [FromQuery] PagingParameters paging)
    {
        return _userProfilesSvc.Search(query)
            .Page(paging.Page, paging.PageLength)
            .Include(u => u.User)
            .Map<UserProfile, UserProfileDto>(_mapper)
            .AsAsyncEnumerable();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromForm] CreateProfileRequest form)
    {
        var role = UserRoleConverter.Parse(form.ProfileType);

        if (!role.HasFlag(UserRole.Customer) && !role.HasFlag(UserRole.Provider))
            return BadRequest(new { message = "invalid profile type" });

        var ret = await _userProfilesSvc.CreateAsync(
            User.GetId(),
            role,
            form.FullName,
            form.ProfilePicture is not null
                ? new WebUploadFile(form.ProfilePicture)
                : null
        );

        return Ok(_mapper.Map<UserProfileDto>(ret));
    }

    #endregion Actions
}