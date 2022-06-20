using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using BDP.Web.Dtos.Parameters;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PurchasesController : ControllerBase
{
    #region Private fields

    private readonly IUsersService _usersSvc;
    private readonly IPurchasesService _purchasesSvc;
    private readonly IMapper _mapper;

    #endregion Private fields

    #region Ctors

    public PurchasesController(
        IUsersService usersSvc,
        IPurchasesService purchasesSvc,
        IMapper mapper)
    {
        _usersSvc = usersSvc;
        _purchasesSvc = purchasesSvc;
        _mapper = mapper;
    }

    #endregion Ctors

    #region Actions

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> MyPurchases([FromQuery] PagingParameters paging)
    {
        var user = await _usersSvc.GetByUsername(User.GetUsername()).FirstAsync();
        var ret = _purchasesSvc.ForUserAsync(user)
            .PageDescending(paging.Page, paging.PageLength)
            .Include(p => p.Transaction)
            .Map(_mapper, typeof(PurchaseDto))
            .AsAsyncEnumerable();

        return Ok(ret);
    }

    #endregion Actions
}