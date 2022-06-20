using AutoMapper;
using BDP.Domain.Services;
using BDP.Web.Api.Auth.Attributes;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetById(Guid id)
        => Ok(_mapper.Map<ServiceDto>(await _servicesService.GetByIdAsync(id)));

    [HttpPost]
    [IsProvider]
    public async Task<IActionResult> Create([FromForm] CreateServiceRequest form)
    {
        var service = await _servicesService.ListAsync(
            User.GetId(),
            form.Title,
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
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateServiceRequest form)
    {
        // TODO:
        // Move ownership logic to services

        var service = await _servicesService.GetByIdAsync(id);

        if (service.OfferedBy.Id != service.Id)
            return Unauthorized(new { message = "you do not own the service" });

        await _servicesService.UpdateAsync(
            id,
            form.Title,
            form.Description,
            form.Price,
            form.AvailableBegin,
            form.AvailableEnd);

        return Ok();
    }

    [HttpDelete("{id}")]
    [IsProvider]
    public async Task<IActionResult> Delete(Guid id)
    {
        // TODO:
        // Move ownership logic to services

        var service = await _servicesService.GetByIdAsync(id);

        if (service.OfferedBy.Id != User.GetId())
            return Unauthorized(new { message = "you do not own the service" });

        await _servicesService.UnlistAsync(id);
        return Ok();
    }

    [HttpPost("{id}/[action]")]
    [IsCustomer]
    public async Task<IActionResult> Reserve(Guid id)
    {
        var order = await _servicesService.ReserveAsync(User.GetId(), id);

        return Ok(_mapper.Map<ReservationDto>(order));
    }

    #endregion Actions
}