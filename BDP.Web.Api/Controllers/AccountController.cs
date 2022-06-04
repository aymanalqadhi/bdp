using BDP.Domain.Services;
using BDP.Web.Api.Auth;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos.Requests;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AccountController : ControllerBase
{
    #region Private fields

    private readonly IUsersService _usersSvc;

    #endregion Private fields

    #region Ctors

    public AccountController(IUsersService usersSvc)
    {
        _usersSvc = usersSvc;
    }

    #endregion Ctors

    #region Actions

    [HttpPost("finish-profile")]
    public async Task<IActionResult> FinishProfileAsync([FromForm] FinishProfileRequest form)
    {
        if (form.AccountType != UserRoles.Customer && form.AccountType != UserRoles.Provider)
            return BadRequest(new { message = "invalid account type" });

        await _usersSvc.FinishProfileAsync(
            User.GetUsername(),
            form.AccountType,
            form.FullName,
            form.ProfilePicture is not null ?
            new WebUploadFile(form.ProfilePicture) : null
        );

        return Ok();
    }

    [HttpGet("is-profile-complete")]
    public async Task<IActionResult> IsProfileCompleteAsync()
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername(), includeGroups: true);

        return Ok(new
        {
            value = user.Groups.Count > 0 && user.FullName != null,
        });
    }

    #endregion Actions
}