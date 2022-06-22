using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Web.Api.Auth.Attributes;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Parameters;
using BDP.Web.Dtos.Requests;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinancialRecordsController : ControllerBase
{
    #region Private fields

    private readonly IFinancialRecordsService _financialRecordsSvc;
    private readonly IMapper _mapper;

    #endregion Private fields

    #region Ctors

    public FinancialRecordsController(
        IFinancialRecordsService financialRecordsSvc,
        IMapper mapper)
    {
        _mapper = mapper;
        _financialRecordsSvc = financialRecordsSvc;
    }

    #endregion Ctors

    #region Actions

    [HttpGet("[action]")]
    [IsAdmin]
    public IAsyncEnumerable<FinancialRecordDto> Pending([FromQuery] PagingParameters paging)
    {
        var ret = _financialRecordsSvc.Pending()
            .PageDescending(paging.Page, paging.PageLength)
            .Include(f => f.MadeBy)
            .Include(f => f.MadeBy!.Profile)
            .Include(f => f.MadeBy!.Profile.ProfilePicture!)
            .Map<FinancialRecord, FinancialRecordDto>(_mapper)
            .AsAsyncEnumerable();

        return ret;
    }

    [HttpPost("{recordId}/[action]")]
    [IsAdmin]
    public async Task<IActionResult> Verify(
        EntityKey<FinancialRecord> recordId,
        [FromForm] VerifyFinancialRecordRequest form)
    {
        var ret = await _financialRecordsSvc.VerifyAsync(
            User.GetId(),
            recordId,
            form.Note,
            form.Document is not null ? new WebUploadFile(form.Document) : null);

        return Ok(_mapper.Map<FinancialRecordVerificationDto>(ret));
    }

    [HttpPost("[action]")]
    [IsAdmin]
    public async Task<IActionResult> Decline(
        EntityKey<FinancialRecord> recordId,
        [FromForm] RejectFinancialRecordRequest form)
    {
        var ret = await _financialRecordsSvc.DeclineAsync(
            User.GetId(),
            recordId,
            form.Note,
            form.Document is not null ? new WebUploadFile(form.Document) : null);

        return Ok(_mapper.Map<FinancialRecordVerificationDto>(ret));
    }

    #endregion Actions
}