using BDP.Domain.Entities;
using BDP.Domain.Services;
using BDP.Web.Api.Auth.Attributes;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos.Requests;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BDP.Web.Api.Controllers;

[Route("api/products/{productId}/reviews")]
[ApiController]
public class ProductReviewsController : ControllerBase
{
    #region Fields

    private readonly IMapper _mapper;
    private readonly IProductReviewsService _productReviewsSvc;

    #endregion Fields

    #region Public Constructors

    public ProductReviewsController(
        IMapper mapper,
        IProductReviewsService productReviewsSvc)
    {
        _mapper = mapper;
        _productReviewsSvc = productReviewsSvc;
    }

    #endregion Public Constructors

    #region Actions

    [HttpGet("[action]")]
    [Authorize]
    public async Task<IActionResult> CanReview(EntityKey<Product> productId)
    {
        var ret = await _productReviewsSvc.CanReviewAsync(User.GetId(), productId);

        return Ok(new { Result = ret });
    }

    [HttpGet("[action]")]
    [Authorize]
    public async Task<IActionResult> MyReview(EntityKey<Product> productId)
    {
        var ret = await _productReviewsSvc
            .GetReviewForUser(User.GetId(), productId)
            .FirstOrDefaultAsync();

        return Ok(ret is not null ? _mapper.Map<ProductReview>(ret) : null);
    }

    [HttpPost("[action]")]
    [IsCustomer]
    public async Task<IActionResult> Review(
        EntityKey<Product> productId,
        [FromBody] SellableReviewRequest form)
    {
        var ret = await _productReviewsSvc
            .ReviewAsync(User.GetId(), productId, form.Rating, form.Comment);

        return Ok(_mapper.Map<ProductReview>(ret));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> ReviewSummary(EntityKey<Product> productId)
        => Ok(await _productReviewsSvc.SummaryForAsync(productId));

    #endregion Actions
}