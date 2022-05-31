using BDP.Web.Api.Auth.Attributes;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Requests;

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BDP.Domain.Services;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    #region Private fileds

    private readonly IConfigurationService _configurationSvc;
    private readonly IUsersService _usersSvc;
    private readonly IServicesService _servicesService;
    private readonly IMapper _mapper;

    #endregion Private fileds

    #region Ctors

    public ServicesController(
        IConfigurationService configurationSvc,
        IUsersService usersSvc,
        IServicesService productsSvc,
        IMapper mapper)
    {
        _configurationSvc = configurationSvc;
        _servicesService = productsSvc;
        _usersSvc = usersSvc;
        _mapper = mapper;
    }

    #endregion Ctors

    #region Actions

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
        => Ok(_mapper.Map<ServiceDto>(await _servicesService.GetByIdAsync(id)));

    [HttpPost]
    [IsProvider]
    public async Task<IActionResult> Create([FromForm] CreateServiceRequest form)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername())!;
        var service = await _servicesService.ListAsync(
            user, form.Title,
            form.Description,
            form.Price,
            form.AvailableBegin,
            form.AvailableEnd,
            form.Attachments?.Select(a => new WebUploadFile(a))
        );

        return CreatedAtAction(
            nameof(GetById),
            new { id = service.Id }, _mapper.Map<ServiceDto>(service)
        );
    }

    [HttpPatch("{id}")]
    [IsProvider]
    public async Task<IActionResult> Update([FromBody] UpdateServiceRequest form, int id)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername())!;
        var service = await _servicesService.GetByIdAsync(id);

        if (user.Id != service.OfferedBy.Id)
            return Unauthorized(new { message = "you do not own the service" });

        await _servicesService.UpdateAsync(
            service,
            form.Title,
            form.Description,
            form.Price,
            form.AvailableBegin,
            form.AvailableEnd);

        return Ok();
    }

    [HttpDelete("{id}")]
    [IsProvider]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername())!;
        var service = await _servicesService.GetByIdAsync(id);

        if (user.Id != service.OfferedBy.Id)
            return Unauthorized(new { message = "you do not own the service" });

        await _servicesService.UnlistAsync(service);
        return Ok();
    }

    [HttpPost("{id}/[action]")]
    [IsCustomer]
    public async Task<IActionResult> Reserve(int id)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername())!;
        var service = await _servicesService.GetByIdAsync(id);
        var order = await _servicesService.ReserveAsync(user, service);

        return Ok(_mapper.Map<ReservationDto>(order));
    }

    #endregion Actions
}