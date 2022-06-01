using BDP.Infrastructure.Repositories.EntityFramework;
using BDP.Tests.Utils;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BDP.Tests.Infrastructure.Repositories.EntityFramework.Util;

public static class TestDbContext
{
    #region Public Methods

    /// <summary>
    /// Creates an in-memory database context
    /// </summary>
    /// <returns></returns>
    public static BdpDbContext Create()
    {
        var options = new DbContextOptionsBuilder<BdpDbContext>()
            .UseInMemoryDatabase($"test_apss_db_{RandomGenerator.NextString(16)}")
            .ConfigureWarnings((o) => o.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        return new BdpDbContext(options);
    }

    #endregion Public Methods
}