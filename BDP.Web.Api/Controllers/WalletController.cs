using BDP.Domain.Entities;
using BDP.Domain.Services;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Requests;
using BDP.Web.Dtos.Responses;
using BDP.Domain.Repositories.Extensions;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Linq.Expressions;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalletController : ControllerBase
{
    #region Private fields

    private readonly int _pageSize;

    private readonly IMapper _mapper;
    private readonly IUsersService _usersSvc;
    private readonly IFinancialRecordsService _financialRecordsSvc;
    private readonly IFinanceService _financeSvc;

    #endregion Private fields

    #region Ctors

    public WalletController(
        IMapper mapper,
        IConfigurationService configurationSvc,
        IUsersService usersSvc,
        IFinancialRecordsService financialRecordsSvc,
        IFinanceService financeSvc)
    {
        _mapper = mapper;
        _usersSvc = usersSvc;
        _financialRecordsSvc = financialRecordsSvc;
        _financeSvc = financeSvc;

        _pageSize = configurationSvc.GetInt("QuerySettings:DefaultPageSize");
    }

    #endregion Ctors

    #region Actions

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Records(int page)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        var ret = _financialRecordsSvc.ForUserAsync(user)
            .PageDescending(page, _pageSize)
            .Include(r => r.Verification!)
            .Include(r => r.Verification!.Document!)
            .AsAsyncEnumerable()
            .Select(_mapper.Map<FinancialRecordDto>);

        return Ok(await ret.ToListAsync());
    }

    [HttpGet("[action]")]
    [Authorize]
    public async Task<IActionResult> Balance()
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());

        var virtualBalance = await _financeSvc.TotalVirtualAsync(user);
        var usableBalance = await _financeSvc.TotalUsableAsync(user);

        return Ok(new BalanceResponse(virtualBalance, usableBalance));
    }

    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> Deposit([FromBody] DepositRequest form)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        await _financeSvc.DepositAsync(user, form.Amount, form.Note);

        return Ok();
    }

    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> Withdraw([FromBody] WithdrawRequest form)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        await _financeSvc.WithdrawAsync(user, form.Amount, form.Note);

        return Ok();
    }

    #endregion Actions
}