using BDP.Domain.Repositories;
using BDP.Infrastructure.Repositories.EntityFramework;

namespace BDP.Tests.Infrastructure.Repositories.EntityFramework.Util;

public static class TestUnitOfWork
{
    #region Public Methods

    /// <summary>
    /// Creates a testing unit of work object
    /// </summary>
    /// <returns>The created object</returns>
    public static IUnitOfWork Create(BdpDbContext? context = null)
        => new BdpUnitOfWork(context ?? TestDbContext.Create());

    #endregion Public Methods
}