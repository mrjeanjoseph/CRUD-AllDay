using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TimesheetManagement.Infrastructure.Persistence;
using Xunit;

namespace TimesheetManagement.IntegrationTests.TestHelpers;

public class InMemoryDatabaseFixture : IAsyncLifetime
{
    private SqliteConnection _connection = null!;
    
    public AppDbContext DbContext { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        // Use SQLite in-memory which better supports ComplexProperty
        _connection = new SqliteConnection("DataSource=:memory:");
        await _connection.OpenAsync();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        DbContext = new AppDbContext(options);
        await DbContext.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await DbContext.DisposeAsync();
        await _connection.DisposeAsync();
    }
}