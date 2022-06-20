using BDP.Domain.Entities;
using BDP.Domain.Services;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Requests;
using BDP.Web.Dtos.Responses;
using BDP.Domain.Repositories.Extensions;
using BDP.Web.Dtos.Parameters;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalletController : ControllerBase
{
    #region Private fields

    private readonly IMapper _mapper;
    private readonly IFinancialRecordsService _financialRecordsSvc;
    private readonly IFinanceService _financeSvc;

    #endregion Private fields

    #region Ctors

    public WalletController(
        IMapper mapper,
        IFinancialRecordsService financialRecordsSvc,
        IFinanceService financeSvc)
    {
        _mapper = mapper;
        _financialRecordsSvc = financialRecordsSvc;
        _financeSvc = financeSvc;
    }

    #endregion Ctors

    #region Actions

    [HttpGet]
    [Authorize]
    public IAsyncEnumerable<FinancialRecordDto> Records([FromQuery] PagingParameters paging)
    {
        var ret = _financialRecordsSvc.ForUser(User.GetId())
            .PageDescending(paging.Page, paging.PageLength)
            .Include(r => r.Verification!)
            .Include(r => r.Verification!.Document!)
            .Map<FinancialRecord, FinancialRecordDto>(_mapper)
            .AsAsyncEnumerable();

        return ret;
    }

    [HttpGet("[action]")]
    [Authorize]
    public async Task<IActionResult> Balance()
    {
        var virtualBalance = await _financeSvc.TotalVirtualAsync(User.GetId());
        var usableBalance = await _financeSvc.TotalUsableAsync(User.GetId());

        return Ok(new BalanceResponse(virtualBalance, usableBalance));
    }

    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> Deposit([FromBody] DepositRequest form)
    {
        var ret = await _financeSvc.DepositAsync(User.GetId(), form.Amount, form.Note);

        return Ok(_mapper.Map<FinancialRecordDto>(ret));
    }

    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> Withdraw([FromBody] WithdrawRequest form)
    {
        var ret = await _financeSvc.WithdrawAsync(User.GetId(), form.Amount, form.Note);

        return Ok(_mapper.Map<FinancialRecordDto>(ret));
    }

    #endregion Actions
}