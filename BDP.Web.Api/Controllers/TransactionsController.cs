using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Requests;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using BDP.Web.Dtos.Parameters;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    #region Private fields

    private readonly IUsersService _usersSvc;
    private readonly ITransactionsService _transactionsSvc;
    private readonly IMapper _mapper;

    #endregion Private fields

    #region Ctors

    public TransactionsController(
        IUsersService usersSvc,
        ITransactionsService transactionsSvc,
        IMapper mapper)
    {
        _usersSvc = usersSvc;
        _transactionsSvc = transactionsSvc;
        _mapper = mapper;
    }

    #endregion Ctors

    #region Actions

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> MyTransactions([FromQuery] PagingParameters paging)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        var ret = _transactionsSvc.ForUserAsync(user)
            .PageDescending(paging.Page, paging.PageLength)
            .Include(t => t.From)
            .Include(t => t.To)
            .Include(t => t.Confirmation!)
            .Select(t => t.ConcealConfirmationTokenIf(t.From.Id != user.Id))
            .Map<Transaction, TransactionDto>(_mapper)
            .AsAsyncEnumerable();

        return Ok(ret);
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

    [HttpGet("{id}/token")]
    [Authorize]
    public async Task<IActionResult> GetConfirmationToken(long id)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        var tx = await _transactionsSvc.GetByIdAsync(id).FirstAsync();

        if (tx.From.Id != user.Id)
            return Unauthorized(new { message = "you did not make the transaction" });

        return Ok(new { token = tx.ConfirmationToken });
    }

    #endregion Actions
}