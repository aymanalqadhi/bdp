using BDP.Domain.Services.Interfaces;
using BDP.Web.Api.Auth.Attributes;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Requests;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    #region Private fileds

    private readonly IConfigurationService _configurationSvc;
    private readonly IUsersService _usersSvc;
    private readonly IProductsService _productsSvc;
    private readonly IFinanceService _financeSvc;
    private readonly IMapper _mapper;

    #endregion

    #region Ctors

    public ProductsController(
        IConfigurationService configurationSvc,
        IUsersService usersSvc,
        IProductsService productsSvc,
        IFinanceService financeSvc,
        IMapper mapper)
    {
        _configurationSvc = configurationSvc;
        _productsSvc = productsSvc;
        _usersSvc = usersSvc;
        _financeSvc = financeSvc;
        _mapper = mapper;
    }

    #endregion

    #region Actions

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
        => Ok(_mapper.Map<ProductDto>(await _productsSvc.GetByIdAsync(id)));

    [HttpPost]
    [IsProvider]
    public async Task<IActionResult> Create([FromForm] CreateProductRequest form)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername())!;
        var product = await _productsSvc.ListAsync(
            user, form.Title,
            form.Description,
            form.Price,
            form.AvailableQuantity,
            form.Attachments?.Select(a => new WebUploadFile(a))
        );

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.Id }, _mapper.Map<ProductDto>(product)
        );
    }

    [HttpPatch("{id}")]
    [IsProvider]
    public async Task<IActionResult> Update([FromBody] UpdateProductRequest form, int id)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername())!;
        var product = await _productsSvc.GetByIdAsync(id);

        if (user.Id != product.OfferedBy.Id)
            return Unauthorized(new { message = "you do not own the product" });

        await _productsSvc.UpdateAsync(product, form.Title, form.Description, form.Price);
        return Ok();
    }

    [HttpDelete("{id}")]
    [IsProvider]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername())!;
        var product = await _productsSvc.GetByIdAsync(id);

        if (user.Id != product.OfferedBy.Id)
            return Unauthorized(new { message = "you do not own the product" });

        await _productsSvc.UnlistAsync(product);
        return Ok();
    }

    [HttpGet("{id}/quantity")]
    public async Task<IActionResult> AvailableQuantity(int id)
    {
        var product = await _productsSvc.GetByIdAsync(id);

        return Ok(new { quantity = await _productsSvc.AvailableQuantityAsync(product) });
    }

    [HttpPost("{id}/[action]")]
    [IsCustomer]
    public async Task<IActionResult> Order([FromBody] OrderProductRequest form, int id)
    {
        var user = await _usersSvc.GetByUsernameAsync(User.GetUsername())!;
        var product = await _productsSvc.GetByIdAsync(id);
        var order = await _productsSvc.OrderAsync(user, product, form.Quantity);

        return Ok(_mapper.Map<OrderDto>(order));
    }

    #endregion
}
