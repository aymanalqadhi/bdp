using BDP.Application.App.Exceptions;
using BDP.Domain.Entities.Validators.Tests;
using BDP.Domain.Repositories;
using BDP.Domain.Services;
using BDP.Infrastructure.Repositories.EntityFramework;
using BDP.Tests.Util;

using System.Threading.Tasks;

using Xunit;

namespace BDP.Application.App.Tests;

public class UsersServiceTests
{
    private readonly IUnitOfWork _uow;
    private readonly IUsersService _usersSvc;

    public UsersServiceTests(IUnitOfWork uow, IUsersService usersService)
    {
        _uow = uow;
        _usersSvc = usersService;
    }

    [Fact]
    public async Task FindByUsernameFact()
    {
        var user = ValidEntitiesFactory.CreateUser();

        _uow.Users.Add(user);
        await _uow.CommitAsync();

        Assert.True(await _uow.Users.Query().AnyAsync(u => u.Id == user.Id));

        var found = await _usersSvc.GetByUsernameAsync(user.Username);

        Assert.Equal(user.Id, found.Id);
        Assert.Equal(user.Username, found.Username);

        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _usersSvc.GetByUsernameAsync(user.Username + RandomGenerator.NextString(2));
        });
    }
}