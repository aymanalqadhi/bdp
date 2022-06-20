using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BDP.Web.Dtos.Parameters;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PurchasesController : ControllerBase
{
    #region Private fields

    private readonly IPurchasesService _purchasesSvc;
    private readonly IMapper _mapper;

    #endregion Private fields

    #region Ctors

    public PurchasesController(
        IPurchasesService purchasesSvc,
        IMapper mapper)
    {
        _purchasesSvc = purchasesSvc;
        _mapper = mapper;
    }

    #endregion Ctors

    #region Actions

    [HttpGet]
    [Authorize]
    public IAsyncEnumerable<object> MyPurchases([FromQuery] PagingParameters paging)
    {
        var ret = _purchasesSvc.ForUserAsync(User.GetId())
            .PageDescending(paging.Page, paging.PageLength)
            .Include(p => p.Transaction)
            .Map(_mapper, typeof(PurchaseDto))
            .AsAsyncEnumerable();

        return ret;
    }

    #endregion Actions
}