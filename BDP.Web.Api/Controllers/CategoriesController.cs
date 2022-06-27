using BDP.Domain.Entities;
using BDP.Domain.Services;
using BDP.Domain.Repositories.Extensions;
using BDP.Web.Api.Extensions;
using BDP.Web.Dtos;
using BDP.Web.Api.Auth.Attributes;
using BDP.Web.Dtos.Requests;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace BDP.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICategoriesService _categoriesSvc;

    public CategoriesController(IMapper mapper, ICategoriesService categoriesSvc)
    {
        _mapper = mapper;
        _categoriesSvc = categoriesSvc;
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetCategory([FromRoute] EntityKey<Category> categoryId)
        => Ok(_mapper.Map<CategoryDto>(await _categoriesSvc
            .GetCategories()
            .Where(c => c.Parent == null)
            .FindAsync(categoryId)));

    [HttpGet]
    public IAsyncEnumerable<CategoryDto> GetTopLevelCategoreis()
        => _categoriesSvc
            .GetCategories()
            .Where(c => c.Parent == null)
            .Map<Category, CategoryDto>(_mapper)
            .AsAsyncEnumerable();

    [HttpGet("{categoryId}/subCategories")]
    public IAsyncEnumerable<CategoryDto> GetSubCategories([FromRoute] EntityKey<Category> categoryId)
        => _categoriesSvc
            .GetCategories()
            .Where(c => c.Parent != null && c.Parent.Id == categoryId)
            .Map<Category, CategoryDto>(_mapper)
            .AsAsyncEnumerable();

    [HttpPost]
    [IsAdmin]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest form)
    {
        var res = await _categoriesSvc.AddAsync(User.GetId(), form.Name, form.Parent);

        return Ok(_mapper.Map<CategoryDto>(res));
    }

    [HttpPut("{categoryId}")]
    [IsAdmin]
    public async Task<IActionResult> Update(
        [FromRoute] EntityKey<Category> categoryId,
        [FromBody] UpdateCategoryRequest form)
    {
        var res = await _categoriesSvc.UpdateAsync(User.GetId(), categoryId, form.Name);

        return Ok(_mapper.Map<CategoryDto>(res));
    }
}