using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="User"/>
/// </summary>
public sealed class UsersRepository :
    EfRepository<User, UserValidator>, IUsersRepository
{
    public UsersRepository(DbSet<User> set) : base(set)
    {
    }
}
