using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Requests;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BDP.Web.Dtos.Parameters;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    #region Private fields

    private readonly ITransactionsService _transactionsSvc;
    private readonly IMapper _mapper;

    #endregion Private fields

    #region Ctors

    public TransactionsController(
        ITransactionsService transactionsSvc,
        IMapper mapper)
    {
        _transactionsSvc = transactionsSvc;
        _mapper = mapper;
    }

    #endregion Ctors

    #region Actions

    [HttpGet]
    [Authorize]
    public IAsyncEnumerable<TransactionDto> MyTransactions([FromQuery] PagingParameters paging)
    {
        var ret = _transactionsSvc.ForUserAsync(User.GetId())
            .PageDescending(paging.Page, paging.PageLength)
            .Include(t => t.From)
            .Include(t => t.To)
            .Include(t => t.Confirmation!)
            .Map<Transaction, TransactionDto>(_mapper)
            .AsAsyncEnumerable();

        return ret;
    }

    [HttpPost("{transactionId}/[action]")]
    [Authorize]
    public async Task<IActionResult> Confirm(
        [FromRoute] EntityKey<Transaction> transactionId,
        [FromBody] ConfirmTransactionRequest form)
    {
        var ret = await _transactionsSvc.ConfirmAsync(
            User.GetId(),
            transactionId,
            form.Token);

        return Ok(_mapper.Map<TransactionConfirmationDto>(ret));
    }

    [HttpPost("{transactionId}/[action]")]
    [Authorize]
    public async Task<IActionResult> Cancel([FromRoute] EntityKey<Transaction> transactionId)
    {
        var ret = await _transactionsSvc.CancelAsync(User.GetId(), transactionId);

        return Ok(_mapper.Map<TransactionConfirmationDto>(ret));
    }

    [HttpGet("{transactionId}/token")]
    [Authorize]
    public async Task<IActionResult> GetConfirmationToken(
        [FromRoute] EntityKey<Transaction> transactionId)
    {
        // TODO:
        // Move ownership validation to service

        var tx = await _transactionsSvc.GetById(transactionId).FirstAsync();

        if (tx.From.Id != User.GetId())
            return Unauthorized(new { message = "you did not make the transaction" });

        return Ok(new { token = tx.ConfirmationToken });
    }

    #endregion Actions
}