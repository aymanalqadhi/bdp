using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions;
using BDP.Domain.Services;
using BDP.Web.Api.Auth.Attributes;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Dtos.Parameters;
using BDP.Web.Dtos.Requests;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Api.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    #region Private fileds

    private readonly IProductsService _productsSvc;
    private readonly IMapper _mapper;

    #endregion Private fileds

    #region Ctors

    public ProductsController(
        IProductsService productsSvc,
        IMapper mapper)
    {
        _productsSvc = productsSvc;
        _mapper = mapper;
    }

    #endregion Ctors

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetById([FromRoute] EntityKey<Product> productId)
        => Ok(_mapper.Map<ProductDto>(await _productsSvc.GetProducts().FindAsync(productId)));

    [HttpGet]
    public IAsyncEnumerable<ProductDto> GetByUserPaged(
        [Required][FromQuery] EntityKey<User> userId,
        [FromQuery] PagingParameters paging)
    {
        var ret = _productsSvc.GetFor(userId)
            .PageDescending(paging.Page, paging.PageLength)
            .Include(p => p.OfferedBy)
            .Include(p => p.Variants)
            .Include(p => p.Variants.Select(v => v.Attachments))
            .Map<Product, ProductDto>(_mapper)
            .AsAsyncEnumerable();

        return ret;
    }

    [HttpGet("[action]")]
    public IAsyncEnumerable<object> Search(
        [Required] string query,
        EntityKey<User>? userId,
        [FromQuery] PagingParameters paging)
    {
        var searchQuery = userId is not null
            ? _productsSvc.Search(query, userId)
            : _productsSvc.Search(query);

        var ret = searchQuery
            .PageDescending(paging.Page, paging.PageLength)
            .Include(p => p.OfferedBy)
            .Include(p => p.Variants)
            .Include(p => p.Variants.Select(v => v.Attachments))
            .Map<Product, ProductDto>(_mapper)
            .AsAsyncEnumerable();

        return ret;
    }

    [HttpPost]
    [IsProvider]
    public async Task<IActionResult> Create([FromForm] CreateProductRequest form)
    {
        var product = await _productsSvc.AddAsync(
            User.GetId(),
            form.Title,
            form.Description,
            form.Categories
        );

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.Id }, _mapper.Map<ProductDto>(product)
        );
    }

    [HttpPatch("{productId}")]
    [IsProvider]
    public async Task<IActionResult> Update(
        [FromRoute] EntityKey<Product> productId,
        [FromBody] UpdateProductRequest form)
    {
        // TODO:
        // Move ownership verification to services

        var product = await _productsSvc.GetProducts().FindAsync(productId);

        if (product.OfferedBy.Id != User.GetId())
            return Unauthorized(new { message = "you do not own the product" });

        await _productsSvc.UpdateAsync(productId, form.Title, form.Description);

        return Ok();
    }

    [HttpDelete("{productId}")]
    [IsProvider]
    public async Task<IActionResult> Delete([FromRoute] EntityKey<Product> productId)
    {
        // TODO:
        // Move ownership verification to services

        var product = await _productsSvc.GetProducts().FindAsync(productId);

        if (product.OfferedBy.Id != User.GetId())
            return Unauthorized(new { message = "you do not own the product" });

        await _productsSvc.RemoveAsync(productId);

        return Ok();
    }
}