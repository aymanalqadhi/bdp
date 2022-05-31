using AutoMapper;
using BDP.Domain.Services;

using BDP.Web.Api.Auth.Jwt;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Requests;
using BDP.Web.Dtos.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    #region Private fields

    private readonly IAuthService _authSvc;
    private readonly IUsersService _usersSvc;
    private readonly IMapper _mapper;
    private readonly JwtSettings _jwt = new();

    #endregion Private fields

    #region Ctors

    public AuthController(IAuthService authSvc, IUsersService usersSvc, IMapper mapper, IConfigurationService configSvc)
    {
        _authSvc = authSvc;
        _usersSvc = usersSvc;
        _mapper = mapper;

        configSvc.Bind(nameof(JwtSettings), _jwt);
    }

    #endregion Ctors

    #region Actions

    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> SignInAsync([FromBody] SignInRequest form)
    {
        var deviceInfo = new LoginDeviceInfo
        {
            UniqueIdentifier = form.UniqueIdentifier,
            DeviceName = form.DeviceName,
            HostName = form.HostName,
            LastIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown"
        };

        var (user, refreshToken) = await _authSvc.SignInAsync(
            form.Username,
            form.Password,
            () => JwtUtils.GenerateRefereshToken(_jwt),
            deviceInfo);

        var accessToken = JwtUtils.GenerateAccessToekn(user, user.Groups, _jwt);

        return Ok(new SignInResponse(accessToken, refreshToken.Token));
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> SignUpAsync([FromBody] SignUpRequest form)
    {
        var user = await _authSvc.SignUpAsync(form.Username, form.Email, form.Password);

        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> SignOutAsync([FromBody] SignOutRequest form)
    {
        if (!JwtUtils.ValidateRefreshToken(form.RefreshToken, _jwt))
            throw new SecurityTokenException("invalid refresh token");

        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        await _authSvc.InvalidateTokenAsync(user, form.RefreshToken, form.UniqueIdentifier);

        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest form)
    {
        if (!JwtUtils.ValidateRefreshToken(form.RefreshToken, _jwt) ||
            !JwtUtils.ValidateToken(form.AccessToken, _jwt.Issuer, _jwt.Audience, _jwt.AccessTokenSecret, false))
            throw new SecurityTokenException("invalid refresh and/or access token");

        var accessPrincipal = JwtUtils.GetPrincipalFromExpiredToken(form.AccessToken, _jwt);
        var user = await _usersSvc.GetByUsernameAsync(accessPrincipal.GetUsername(), includeGroups: true);

        if (!await _authSvc.IsTokenValidAsync(user, form.RefreshToken, form.UniqueIdentifier))
            throw new SecurityTokenException("invalid refresh token");

        return Ok(new
        {
            accessToken = JwtUtils.GenerateAccessToekn(user, user.Groups, _jwt)
        });
    }

    [AllowAnonymous]
    [HttpGet("confirm/{token}")]
    public async Task<IActionResult> ConfirmWithTokenAsync(string token)
    {
        await _authSvc.ConfirmWithToken(token);

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmWithOtpAsync([FromBody] ConfirmWithOtpRequest form)
    {
        await _authSvc.ConfirmWithOtp(form.Otp);

        return Ok();
    }

    #endregion Actions
}