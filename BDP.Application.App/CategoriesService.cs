using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Repositories.Extensions;
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
    public IQueryBuilder<Category> GetCategories()
        => _uow.Categories.Query();

    /// <inheritdoc/>
    public async Task<Category> AddAsync(
        EntityKey<User> userId,
        string name,
        EntityKey<Category>? parent = null)
    {
        var user = await _uow.Users.Query().FindWithRoleValidationAsync(userId, UserRole.Admin);

        var category = new Category
        {
            Name = name,
            Parent = parent is not null ? await _uow.Categories.Query().FindAsync(parent) : null,
            AddedBy = user,
        };

        _uow.Categories.Add(category);
        await _uow.CommitAsync();

        return category;
    }

    /// <inheritdoc/>
    public async Task<Category> UpdateAsync(
        EntityKey<User> userId,
        EntityKey<Category> categoryId,
        string name)
    {
        await _uow.Users.Query().FindWithRoleValidationAsync(userId, UserRole.Admin);

        var category = await _uow.Categories.Query().FindAsync(categoryId);

        category.Name = name;

        _uow.Categories.Update(category);
        await _uow.CommitAsync();

        return category;
    }

    #endregion Public Methods
}