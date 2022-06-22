using BDP.Domain.Entities;
using BDP.Domain.Services;
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

    [HttpGet]
    public IAsyncEnumerable<CategoryDto> GetTopLevelCategoreis()
        => _categoriesSvc
            .GetCategories()
            .Where(c => c.Parent == null)
            .Map<Category, CategoryDto>(_mapper)
            .AsAsyncEnumerable();

    [HttpGet("{id}")]
    public IAsyncEnumerable<CategoryDto> GetSubCategories(EntityKey<Category> id)
        => _categoriesSvc
            .GetCategories()
            .Where(c => c.Parent != null && c.Parent.Id == id)
            .Map<Category, CategoryDto>(_mapper)
            .AsAsyncEnumerable();

    [HttpPost]
    [IsAdmin]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest form)
    {
        var res = await _categoriesSvc.AddAsync(form.Name, form.Parent);

        return Ok(_mapper.Map<CategoryDto>(res));
    }

    [HttpPut("{id}")]
    [IsAdmin]
    public async Task<IActionResult> Update(EntityKey<Category> categoryId, [FromBody] UpdateCategoryRequest form)
    {
        var res = await _categoriesSvc.UpdateAsync(categoryId, form.Name);

        return Ok(_mapper.Map<CategoryDto>(res));
    }
}