using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="UserGroup"/>
/// </summary>
public sealed class UserGroupsRepository :
    EfRepository<UserGroup, UserGroupValidator>, IUserGroupsRepository
{
    public UserGroupsRepository(DbSet<UserGroup> set) : base(set)
    {
    }
}
