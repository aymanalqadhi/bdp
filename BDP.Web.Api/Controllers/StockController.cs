using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Web.Api.Auth.Attributes;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Requests;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BDP.Web.Api.Controllers;

[Route("api/products/{productId}/variants/{variantId}/stock")]
[ApiController]
public class StockController : ControllerBase
{
    #region Fields

    private readonly IMapper _mapper;
    private readonly IStockBatchesService _stockSvc;

    #endregion Fields

    #region Public Constructors

    public StockController(IMapper mapper, IStockBatchesService stockSvc)
    {
        _mapper = mapper;
        _stockSvc = stockSvc;
    }

    #endregion Public Constructors

    [HttpGet]
    [IsProvider]
    public IAsyncEnumerable<StockBatchDto> GetStockBatches(
        [FromRoute] EntityKey<Product> _,
        [FromRoute] EntityKey<ProductVariant> variantId)
    {
        return _stockSvc.GetStockBatches(variantId)
            .OrderDescending()
            .Map<StockBatch, StockBatchDto>(_mapper)
            .AsAsyncEnumerable();
    }

    [HttpGet("quantity")]
    [Authorize]
    public async Task<IActionResult> AvailableQuantity(
        [FromRoute] EntityKey<Product> _,
        [FromRoute] EntityKey<ProductVariant> variantId)
    {
        var quantity = await _stockSvc.AvailableQuantityAsync(variantId);

        return Ok(new { quantity });
    }

    [HttpPost]
    [IsProvider]
    public async Task<IActionResult> Create(
        [FromRoute] EntityKey<Product> _,
        [FromRoute] EntityKey<ProductVariant> variantId,
        [FromBody] CreateStockBatchRequest form)
    {
        var batch = await _stockSvc.AddAsync(User.GetId(), variantId, form.Quantity);

        return Ok(_mapper.Map<StockBatchDto>(batch));
    }

    [HttpDelete("{batchId}")]
    [IsProvider]
    public async Task<IActionResult> Remove(
        [FromRoute] EntityKey<Product> _,
        [FromRoute] EntityKey<StockBatch> batchId)
    {
        await _stockSvc.RemoveAsync(User.GetId(), batchId);

        return Ok();
    }
}