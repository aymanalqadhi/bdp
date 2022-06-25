using BDP.Domain.Entities;
using BDP.Domain.Repositories.Extensions.Exceptions;

namespace BDP.Domain.Repositories.Extensions;

public static class IQueryBuilderSecurityExtensions
{
    /// <summary>
    /// Asynchrnonously gets a user, then validates its role
    /// </summary>
    /// <param name="self"></param>
    /// <param name="userId">The id of the user to get and validate</param>
    /// <param name="role">The role to validate against</param>
    /// <returns>The validated user</returns>
    /// <exception cref="InsufficientPermissionsException"></exception>
    public static async Task<User> FindWithRoleValidationAsync(
        this IQueryBuilder<User> self,
        EntityKey<User> userId,
        UserRole role)
    {
        var user = await self.FindAsync(userId);

        if (!user.Role.HasFlag(role))
        {
            throw new InsufficientPermissionsException(
                userId,
                $"user #{userId} does not have the roles {role}");
        }

        return user;
    }

    /// <summary>
    /// Asynchrnously gets a resource, then validates its owner
    /// </summary>
    /// <typeparam name="TEntity">The type of the resource</typeparam>
    /// <param name="self"></param>
    /// <param name="userId">The id of the user who owns the resource</param>
    /// <param name="resourceId">The id of the resource to get</param>
    /// <returns>The ownership-validated resource</returns>
    /// <exception cref="InsufficientPermissionsException"></exception>
    public static async Task<TEntity> FindWithOwnershipValidationAsync<TEntity>(
        this IQueryBuilder<TEntity> self,
        EntityKey<User> userId,
        EntityKey<TEntity> resourceId) where TEntity : AuditableEntity<TEntity>, IOwnable

    {
        var resource = await self.FindAsync(resourceId);

        if (resource.OwnedBy.Id != userId)
        {
            var typeName = typeof(TEntity).Name;

            throw new InsufficientPermissionsException(
                userId,
                $"user #{userId} does not own the {typeName.ToLower()} with id #{resource.Id}");
        }

        return resource;
    }
}