using Microsoft.EntityFrameworkCore;
using TimesheetManagement.Infrastructure.Persistence;

namespace TimesheetManagement.IntegrationTests.TestHelpers;

public class InMemoryDatabaseFixture : IAsyncLifetime
{
    public AppDbContext DbContext { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        DbContext = new AppDbContext(options);

        // For in-memory, no migrations needed, but ensure schema is created
        await DbContext.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await DbContext.DisposeAsync();
    }
}