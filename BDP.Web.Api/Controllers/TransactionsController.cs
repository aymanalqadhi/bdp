using AutoMapper;
using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    #region Private fields

    private readonly int _pageSize;

    private readonly IUsersService _usersSvc;
    private readonly ITransactionsService _transactionsSvc;
    private readonly IMapper _mapper;

    #endregion Private fields

    #region Ctors

    public TransactionsController(
        IConfigurationService configurationSvc,
        IUsersService usersSvc,
        ITransactionsService transactionsSvc,
        IMapper mapper)
    {
        _usersSvc = usersSvc;
        _transactionsSvc = transactionsSvc;
        _mapper = mapper;

        _pageSize = configurationSvc.GetInt("QuerySettings:DefaultPageSize");
    }

    #endregion Ctors

    #region Actions

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> MyTransactions([Required] int page)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        var ret = _transactionsSvc.ForUserAsync(user)
            .PageDescending(page, _pageSize)
            .Include(t => t.From)
            .Include(t => t.To)
            .Include(t => t.Confirmation!)
            .AsAsyncEnumerable();

        return Ok(await ret.Select(t =>
        {
            var dto = _mapper.Map<TransactionDto>(t);

            if (t.From.Id != user.Id)
                dto.ConfirmationToken = null;

            return dto;
        }).ToListAsync());
    }

    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> Confirm([FromBody] ConfirmTransactionRequest form)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        var ret = await _transactionsSvc.ConfirmAsync(user, form.Token);

        return Ok(_mapper.Map<TransactionConfirmationDto>(ret));
    }

    [HttpPost("{id}/[action]")]
    [Authorize]
    public async Task<IActionResult> Cancel(int id)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        var ret = await _transactionsSvc.CancelAsync(user, id);

        return Ok(_mapper.Map<TransactionConfirmationDto>(ret));
    }

    #endregion Actions
}