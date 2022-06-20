using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Parameters;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    #region Private fields

    private readonly IUsersService _usersSvc;
    private readonly IMapper _mapper;

    #endregion Private fields

    #region Ctros

    public UsersController(
        IUsersService usersSvc,
        IMapper mapper)
    {
        _usersSvc = usersSvc;
        _mapper = mapper;
    }

    #endregion Ctros

    #region Actions

    [HttpGet("{username}")]
    public async Task<IActionResult> GetByUsernameAsync(string username)
    {
        var ret = await _usersSvc.GetByUsername(username)
            .Map<User, UserDto>(_mapper)
            .FirstAsync();

        return Ok(ret);
    }

    [HttpGet("search")]
    public IAsyncEnumerable<UserDto> Search(string query, [FromQuery] PagingParameters paging)
    {
        return _usersSvc.Search(query)
            .Page(paging.Page, paging.PageLength)
            .Map<User, UserDto>(_mapper)
            .AsAsyncEnumerable();
    }

    #endregion Actions
}