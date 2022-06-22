using BDP.Domain.Entities.Validators.Tests;
using BDP.Domain.Repositories;
using BDP.Tests.Util;
using BDP.Tests.Infrastructure.Repositories.EntityFramework.Util;

using System;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using BDP.Domain.Entities;

namespace BDP.Tests.Infrastructure.Repositories.EntityFramework;

public class QueryBuilderTests
{
    #region Private properties

    private readonly IUnitOfWork _uow;

    #endregion Private properties

    #region Constructors

    public QueryBuilderTests()
        => _uow = TestUnitOfWork.Create();

    #endregion Constructors

    #region Tests

    [Theory]
    [InlineData(3, true)]
    [InlineData(30, true)]
    [InlineData(1, true)]
    [InlineData(5, false)]
    public async Task FirstSuccessTheory(int itemsCount, bool shouldSuceed)
    {
        var logs = Enumerable
            .Range(0, itemsCount)
            .Select(_ => ValidEntitiesFactory.CreateLog())
            .ToArray();

        _uow.Logs.Add(logs);

        Assert.Equal(itemsCount, await _uow.CommitAsync());

        if (shouldSuceed)
        {
            Assert.Equal((await _uow.Logs.Query().FirstAsync()).Id, logs[0].Id);
            Assert.All(logs, async log =>
            {
                Assert.True(
                  (await _uow.Logs
                     .Query()
                     .FirstAsync(l => l.Id == log.Id)).Message == log.Message
               );
            });
            Assert.NotNull(await _uow.Logs.Query().FirstOrDefaultAsync(l => l.Id == logs[0].Id));
        }
        else
        {
            var exception = await Assert.ThrowsAsync<AggregateException>(async () =>
            {
                await _uow.Logs.Query().FirstAsync(l => l.Id == new EntityKey<Log>(Guid.NewGuid()));
            });

            Assert.IsType<InvalidOperationException>(exception.InnerException);
            Assert.Null(await _uow.Logs.Query().FirstOrDefaultAsync(l => l.Id == logs[0].Id));
        }
    }

    [Fact]
    public async Task CountFact()
    {
        var logs = Enumerable
            .Range(0, RandomGenerator.NextInt(0, 100))
            .Select(i => ValidEntitiesFactory.CreateLog())
            .ToArray();

        _uow.Logs.Add(logs);

        Assert.Equal(logs.Length, await _uow.CommitAsync());
        Assert.Equal(logs.Length, await _uow.Logs.Query().CountAsync());
        Assert.Equal(
            logs.Count(l => l.Message.Length % 2 == 0),
            await _uow.Logs.Query().CountAsync(l => l.Message.Length % 2 == 0)
        );
    }

    [Fact]
    public async Task AnyFact()
    {
        var log = ValidEntitiesFactory.CreateLog();

        Assert.False(await _uow.Logs.Query().AnyAsync(l => l.Message == log.Message));

        _uow.Logs.Add(log);

        Assert.Equal(1, await _uow.CommitAsync());
        Assert.True(await _uow.Logs.Query().AnyAsync(l => l.Message == log.Message));
    }

    [Fact]
    public async Task AllFact()
    {
        var logs = Enumerable
            .Range(0, RandomGenerator.NextInt(0, 100))
            .Select(i => ValidEntitiesFactory.CreateLog())
            .Where(l => l.Message.Length % 2 == 0)
            .ToArray();

        _uow.Logs.Add(logs);

        Assert.Equal(logs.Length, await _uow.CommitAsync());
        Assert.True(await _uow.Logs.Query().AllAsync(l => l.Message.Length % 2 == 0));
    }

    #endregion Tests
}