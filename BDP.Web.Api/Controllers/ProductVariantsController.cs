using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Web.Api.Auth.Attributes;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Requests;
using BDP.Web.Dtos.Parameters;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

namespace BDP.Web.Api.Controllers;

[Route("api/products/{productId}/variants")]
[ApiController]
public class ProductVariantsController : ControllerBase
{
    #region Fields

    private readonly IMapper _mapper;
    private readonly IProductVariantsService _variantsSvc;

    #endregion Fields

    #region Public Constructors

    public ProductVariantsController(IMapper mapper, IProductVariantsService productsSvc)
    {
        _mapper = mapper;
        _variantsSvc = productsSvc;
    }

    #endregion Public Constructors

    #region Public Methods

    [HttpPost]
    [IsProvider]
    public async Task<IActionResult> Create(
        EntityKey<Product> productId,
        [FromBody] CreateProductVariantRequest form)
    {
        Func<EntityKey<Product>, string, string?, decimal, IEnumerable<IUploadFile>?, Task<ProductVariant>> fn =
            form.Type == ProductVariantType.Sellable
                ? _variantsSvc.AddSellableAsync
                : _variantsSvc.AddReservableAsync;

        var variant = await fn(
            productId,
            form.Name,
            form.Description,
            form.Price,
            form.Attachments?.Select(f => new WebUploadFile(f)));

        return CreatedAtAction(
            nameof(GetById),
            new { id = variant.Id }, _mapper.Map<ProductVariantDto>(variant)
        );
    }

    [HttpGet("{variantId}")]
    public async Task<IActionResult> GetById(
        EntityKey<Product> productId,
        EntityKey<ProductVariant> variantid)
    {
        return Ok(_mapper.Map<ProductVariantDto>(
            await _variantsSvc
                .GetVariants(productId)
                .FindAsync(variantid)));
    }

    [HttpGet]
    public IAsyncEnumerable<ProductVariantDto> GetVariants(
        EntityKey<Product> productId,
        [FromQuery] PagingParameters paging)
    {
        return _variantsSvc.GetVariants(productId)
            .PageDescending(paging.Page, paging.PageLength)
            .Include(v => v.Attachments)
            .Map<ProductVariant, ProductVariantDto>(_mapper)
            .AsAsyncEnumerable();
    }

    [HttpPatch("{variantId}")]
    [IsProvider]
    public async Task<IActionResult> Update(
        EntityKey<Product> productId,
        EntityKey<ProductVariant> variantId,
        [FromBody] UpdateProductVariantRequest form)
    {
        // TODO:
        // Move ownership verification to services

        var updatedVariant = await _variantsSvc.UpdateAsync(
            variantId, form.Name, form.Description, form.Price);

        return Ok(_mapper.Map<ProductVariantDto>(updatedVariant));
    }

    #endregion Public Methods
}