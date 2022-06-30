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
    private readonly IPurchasesService _purchasesSvc;

    #endregion Fields

    #region Public Constructors

    public ProductVariantsController(
        IMapper mapper,
        IProductVariantsService productsSvc,
        IPurchasesService purchasesSvc)
    {
        _mapper = mapper;
        _variantsSvc = productsSvc;
        _purchasesSvc = purchasesSvc;
    }

    #endregion Public Constructors

    #region Public Methods

    [HttpPost]
    [IsProvider]
    public async Task<IActionResult> Create(
        [FromRoute] EntityKey<Product> productId,
        [FromBody] CreateProductVariantRequest form)
    {
        var variant = await _variantsSvc.AddAsync(
            User.GetId(),
            productId,
            form.Name,
            form.Description,
            form.Price,
            form.Type,
            form.Attachments?.Select(f => new WebUploadFile(f)));

        return CreatedAtAction(
            nameof(GetById),
            new { id = variant.Id }, _mapper.Map<ProductVariantDto>(variant)
        );
    }

    [HttpGet("{variantId}")]
    public async Task<IActionResult> GetById(
        [FromRoute] EntityKey<Product> productId,
        [FromRoute] EntityKey<ProductVariant> variantid)
    {
        return Ok(_mapper.Map<ProductVariantDto>(
            await _variantsSvc
                .GetVariants(productId)
                .FindAsync(variantid)));
    }

    [HttpGet]
    public IAsyncEnumerable<ProductVariantDto> GetVariants(
        [FromRoute] EntityKey<Product> productId,
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
        [FromRoute] EntityKey<Product> _,
        [FromRoute] EntityKey<ProductVariant> variantId,
        [FromBody] UpdateProductVariantRequest form)
    {
        var updatedVariant = await _variantsSvc.UpdateAsync(
            User.GetId(),
            variantId,
            form.Name,
            form.Description,
            form.Price);

        return Ok(_mapper.Map<ProductVariantDto>(updatedVariant));
    }

    [HttpPost("{variantId}/order")]
    [IsCustomer]
    public async Task<IActionResult> Order(
         [FromRoute] EntityKey<Product> _,
         [FromRoute] EntityKey<ProductVariant> variantId,
        [FromBody] OrderRequest form)
    {
        var purchase = await _purchasesSvc.OrderAsync(User.GetId(), variantId, form.Quantity);

        return Ok(_mapper.Map<OrderDto>(purchase));
    }

    [HttpPost("{variantId}/reserve")]
    [IsCustomer]
    public async Task<IActionResult> Reserve(
        [FromRoute] EntityKey<Product> _,
        [FromRoute] EntityKey<ProductVariant> variantId)
    {
        var purchase = await _purchasesSvc.ReserveAsync(User.GetId(), variantId);

        return Ok(_mapper.Map<ReservationDto>(purchase));
    }

    #endregion Public Methods
}