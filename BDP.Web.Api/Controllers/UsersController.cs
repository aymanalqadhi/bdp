using AutoMapper;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Parameters;
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
        => Ok(_mapper.Map<UserDto>(
            await _usersSvc.GetByUsernameAsync(
                username,
                includeGroups: true,
                includePhones: true)
            ));

    [HttpGet("search")]
    public async Task<IActionResult> Search(string query, [FromQuery] PagingParameters paging)
    {
        var ret = _usersSvc.SearchAsync(query)
            .Page(paging.Page, paging.PageLength)
            .AsAsyncEnumerable()
            .Select(_mapper.Map<UserDto>);

        return Ok(await ret.ToListAsync());
    }

    #endregion Actions
}