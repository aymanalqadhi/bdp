using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services;

namespace BDP.Application.App;

/// <inheritdoc/>
public sealed class CategoriesService : ICategoriesService
{
    #region Fields

    private readonly IUnitOfWork _uow;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the application</param>
    public CategoriesService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public async Task<Category> AddAsync(string name, Category? parent = null)
    {
        var category = new Category
        {
            Name = name,
            Parent = parent,
        };

        _uow.Categories.Add(category);
        await _uow.CommitAsync();

        return category;
    }

    /// <inheritdoc/>
    public IQueryBuilder<Category> GetCategories()
        => _uow.Categories.Query();

    #endregion Public Methods
}