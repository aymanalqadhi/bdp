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

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PurchasesController : ControllerBase
{
    #region Private fields

    private readonly int _pageSize;

    private readonly IUsersService _usersSvc;
    private readonly IPurchasesService _purchasesSvc;
    private readonly IMapper _mapper;

    #endregion Private fields

    #region Ctors

    public PurchasesController(
        IConfigurationService configurationSvc,
        IUsersService usersSvc,
        IPurchasesService purchasesSvc,
        IMapper mapper)
    {
        _pageSize = configurationSvc.GetInt("QuerySettings:DefaultPageSize");
        _usersSvc = usersSvc;
        _purchasesSvc = purchasesSvc;
        _mapper = mapper;
    }

    #endregion Ctors

    #region Actions

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> MyPurchases([Required] int page)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername());
        var ret = _purchasesSvc.ForUserAsync(user)
            .PageDescending(page, _pageSize)
            .Include(p => p.Transaction)
            .Map(_mapper, typeof(PurchaseDto))
            .AsAsyncEnumerable();

        return Ok(ret);
    }

    #endregion Actions
}