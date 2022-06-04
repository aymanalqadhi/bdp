using BDP.Domain.Entities.Validators.Tests;
using BDP.Domain.Repositories;
using BDP.Infrastructure.Repositories.EntityFramework;
using BDP.Domain.Entities.Validators;
using BDP.Tests.Util;
using BDP.Tests.Infrastructure.Repositories.EntityFramework.Util;

using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Threading.Tasks;

using Xunit;

namespace BDP.Tests.Infrastructure.Repositories.EntityFramework;

public class RepositoryTests
{
    #region Fields

    private readonly BdpDbContext _ctx;
    private readonly IUnitOfWork _uow;

    #endregion Fields

    #region Constructors

    public RepositoryTests()
    {
        _ctx = TestDbContext.Create();
        _uow = TestUnitOfWork.Create(_ctx);
    }

    #endregion Constructors

    #region Tests

    [Fact]
    public async Task AddFact()
    {
        var log = ValidEntitiesFactory.CreateLog();

        _uow.Logs.Add(log);

        Assert.Equal(1, await _uow.CommitAsync());
        Assert.True(await _ctx.Logs.AnyAsync(l => l.Id == log.Id));
    }

    [Fact]
    public async Task DeleteFact()
    {
        var log = ValidEntitiesFactory.CreateLog();

        _uow.Logs.Add(log);

        Assert.Equal(1, await _uow.CommitAsync());
        Assert.True(_ctx.Logs.Any(l => l.Id == log.Id));

        _uow.Logs.Remove(log);

        Assert.Equal(1, await _uow.CommitAsync());
        Assert.False(_ctx.Logs.Any(l => l.Id == log.Id));
    }

    [Fact]
    public async Task UpdateFact()
    {
        var log = ValidEntitiesFactory.CreateLog();

        _uow.Logs.Add(log);

        Assert.Equal(1, await _uow.CommitAsync());
        Assert.True(_ctx.Logs.Any(l => l.Id == log.Id));

        log.Message = RandomGenerator.NextString(0xff);

        _uow.Logs.Update(log);

        Assert.Equal(1, await _uow.CommitAsync());
        Assert.True(await _ctx.Logs.AnyAsync(l => l.Id == log.Id && l.Message == log.Message));
    }

    #endregion Tests
}