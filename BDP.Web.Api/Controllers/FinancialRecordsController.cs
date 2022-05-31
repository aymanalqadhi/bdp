using AutoMapper;
using BDP.Domain.Entities;
using BDP.Domain.Services;
using BDP.Web.Api.Auth.Attributes;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
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

    private readonly int _pageSize;

    private readonly IUsersService _usersSvc;
    private readonly IFinancialRecordsService _financialRecordsSvc;
    private readonly IMapper _mapper;

    #endregion Private fields

    #region Ctors

    public FinancialRecordsController(
        IConfigurationService configurationSvc,
        IUsersService usersSvc,
        IFinancialRecordsService financialRecordsSvc,
        IMapper mapper)
    {
        _usersSvc = usersSvc;
        _mapper = mapper;
        _financialRecordsSvc = financialRecordsSvc;

        _pageSize = configurationSvc.GetInt("QuerySettings:DefaultPageSize");
    }

    #endregion Ctors

    #region Actions

    [HttpGet("[action]")]
    [IsAdmin]
    public async Task<IActionResult> Pending([Required] int page)
    {
        var ret = await _financialRecordsSvc
            .PendingAsync(
                page,
                _pageSize,
                descOrder: true,
                includes: new Expression<Func<FinancialRecord, object>>[]
                {
                    f => f.MadeBy,
                    f => f.MadeBy.ProfilePicture!
                })
            .ToListAsync();

        return Ok(ret.Select(_mapper.Map<FinancialRecordDto>));
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
        int recordId,
        FinancialRecordVerificationOutcome outcome,
        string? note,
        IFormFile? document)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername())!;

        return await _financialRecordsSvc.VerifyAsync(
            recordId,
            user,
            outcome,
            note,
            document is not null ? new WebUploadFile(document) : null
        );
    }

    #endregion Private methods
}