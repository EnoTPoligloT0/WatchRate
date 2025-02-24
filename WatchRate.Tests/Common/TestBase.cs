using Microsoft.EntityFrameworkCore;
using WatchRate.Infrastucture.Persistence;

namespace WatchRate.Tests.Common;

public class TestBase : IDisposable
{
    protected readonly WatchRateDbContext Context;

    protected TestBase()
    {
        var options = new DbContextOptionsBuilder<WatchRateDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Context = new WatchRateDbContext(options);
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}