using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <inheritdoc/>
public sealed class Repository<T, Validator> : IRepository<T>
    where T : AuditableEntity<T>
    where Validator : IValidator<T>, new()
{
    #region Private fields

    private readonly DbSet<T> _set;
    private readonly Validator _validator;

    #endregion Private fields

    #region Ctors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="set">the database set that backs the repository</param>
    public Repository(DbSet<T> set)
    {
        _set = set;
        _validator = new Validator();
    }

    #endregion Ctors

    #region Public methods

    /// <inheritdoc/>
    public void Add(params T[] entites)
    {
        foreach (var entity in entites)
        {
            _validator.Validate(entity);

            entity.CreatedAt = DateTime.Now;
            entity.ModifiedAt = DateTime.Now;
        }

        _set.AddRange(entites);
    }

    /// <inheritdoc/>
    public void Update(T entity)
    {
        _validator.Validate(entity);

        entity.ModifiedAt = DateTime.Now;

        _set.Update(entity);
    }

    /// <inheritdoc/>
    public void Remove(T entity)
        => _set.Remove(entity);

    /// <inheritdoc/>
    public IQueryBuilder<T> Query()
        => new QueryBuilder<T>(_set.AsQueryable());

    #endregion Public methods
}

/// <summary>
/// A factory class to simplify the creation of repository instrances
/// </summary>
public static class RepositoryFactory
{
    /// <summary>
    /// Creates a new <see cref="Repository{T, Validator}"/> instance
    /// </summary>
    /// <typeparam name="TEntity">The entity of the repository</typeparam>
    /// <typeparam name="Validator">The validator to be used by the repository</typeparam>
    /// <param name="set">The database set</param>
    /// <returns>The created repository instance</returns>
    public static IRepository<TEntity> Create<TEntity, Validator>(DbSet<TEntity> set)
        where TEntity : AuditableEntity<TEntity>
        where Validator : IValidator<TEntity>, new()
    {
        return new Repository<TEntity, Validator>(set);
    }

    /// <summary>
    /// Creates a new <see cref="Repository{T, Validator}"/> instance with a default
    /// validator of <see cref="Validator{T}"/>
    /// </summary>
    /// <typeparam name="TEntity">The entity of the repository</typeparam>
    /// <param name="set">The database set</param>
    /// <returns>The created repository instance</returns>
    public static IRepository<TEntity> Create<TEntity>(DbSet<TEntity> set)
        where TEntity : AuditableEntity<TEntity>
    {
        return new Repository<TEntity, Validator<TEntity>>(set);
    }
}