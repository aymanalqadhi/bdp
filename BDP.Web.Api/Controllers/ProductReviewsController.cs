using BDP.Domain.Entities;
using BDP.Domain.Services;
using BDP.Web.Api.Auth.Attributes;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
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
    public async Task<IActionResult> CanReview([FromRoute] EntityKey<Product> productId)
    {
        var ret = await _productReviewsSvc.CanReviewAsync(User.GetId(), productId);

        return Ok(new { Result = ret });
    }

    [HttpGet]
    public IAsyncEnumerable<ProductReviewDto> Reviews([FromRoute] EntityKey<Product> productId)
    {
        return _productReviewsSvc.GetFor(productId)
            .Include(r => r.LeftBy)
            .Map<ProductReview, ProductReviewDto>(_mapper)
            .AsAsyncEnumerable();
    }

    [HttpGet("[action]/{userId}")]
    [Authorize]
    public async Task<IActionResult> Of(
        [FromRoute] EntityKey<Product> productId,
        [FromRoute] EntityKey<User> userId)
    {
        var ret = await _productReviewsSvc
            .GetReviewForUser(userId, productId)
            .FirstOrDefaultAsync();

        return Ok(ret is not null ? _mapper.Map<ProductReview>(ret) : null);
    }

    [HttpPost]
    [IsCustomer]
    public async Task<IActionResult> Review(
        [FromRoute] EntityKey<Product> productId,
        [FromBody] SellableReviewRequest form)
    {
        var ret = await _productReviewsSvc
            .ReviewAsync(User.GetId(), productId, form.Rating, form.Comment);

        return Ok(_mapper.Map<ProductReview>(ret));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Summary([FromRoute] EntityKey<Product> productId)
        => Ok(await _productReviewsSvc.SummaryForAsync(productId));

    #endregion Actions
}