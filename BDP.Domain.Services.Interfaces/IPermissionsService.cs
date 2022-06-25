using BDP.Domain.Entities;
using BDP.Domain.Repositories;

namespace BDP.Domain.Services;

public interface IPermissionsService
{
    Task<bool> HasRoleAsync(EntityKey<User> userId, UserRole role);

    Task<TResult?> GetIfOwnedAsync<TEntity, TResult>(
        EntityKey<User> userId,
        EntityKey<TEntity> resourceId,
        Func<IQueryBuilder<TEntity>, IQueryBuilder<TResult>>? queryConfiguration = null)
        where TEntity : class
        where TResult : class;
}