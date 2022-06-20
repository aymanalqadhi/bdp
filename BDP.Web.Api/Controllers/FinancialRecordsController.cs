using AutoMapper;
using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Web.Api.Auth.Attributes;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Parameters;
using BDP.Web.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

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
        var ret = _financialRecordsSvc.PendingAsync()
            .PageDescending(paging.Page, paging.PageLength)
            .Include(f => f.MadeBy)
            .Include(f => f.MadeBy.ProfilePicture!)
            .Map<FinancialRecord, FinancialRecordDto>(_mapper)
            .AsAsyncEnumerable();

        return ret;
    }

    [HttpPost("[action]")]
    [IsAdmin]
    public async Task<IActionResult> Verify([FromForm] VerifyFinancialRecordRequest form)
    {
        var ret = await FinishRecord(
            form.RecordId, FinancialRecordVerificationOutcome.Accepted, form.Note, form.Document);

        return Ok(_mapper.Map<FinancialRecordVerificationDto>(ret));
    }

    [HttpPost("[action]")]
    [IsAdmin]
    public async Task<IActionResult> Decline([FromForm] RejectFinancialRecordRequest form)
    {
        var ret = await FinishRecord(
            form.RecordId, FinancialRecordVerificationOutcome.Rejected, form.Note, form.Document);

        return Ok(_mapper.Map<FinancialRecordVerificationDto>(ret));
    }

    #endregion Actions

    #region Private methods

    private async Task<FinancialRecordVerification> FinishRecord(
        Guid recordId,
        FinancialRecordVerificationOutcome outcome,
        string? note,
        IFormFile? document)
    {
        return await _financialRecordsSvc.VerifyAsync(
            User.GetId(),
            recordId,
            outcome,
            note,
            document is not null ? new WebUploadFile(document) : null
        );
    }

    #endregion Private methods
}