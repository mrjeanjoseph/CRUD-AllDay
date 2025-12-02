using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TimesheetManagement.Domain.Projects;
using TimesheetManagement.Infrastructure.Persistence;
using TimesheetManagement.Infrastructure.Repositories;
using Xunit;

namespace TimesheetManagement.Tests.Repositories;

public class ProjectRepositoryTests
{
    private static AppDbContext CreateDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAndGetByCode_ShouldPersistProject()
    {
        using var db = CreateDb();
        var repo = new ProjectRepository(db);
        var project = new Project("PRJ1", "Demo", "IT");

        await repo.AddAsync(project);
        await db.SaveChangesAsync();

        var exists = await repo.CodeExistsAsync("PRJ1");
        exists.Should().BeTrue();

        var loaded = await repo.GetByCodeAsync("PRJ1");
        loaded.Should().NotBeNull();
        loaded!.Name.Should().Be("Demo");
    }
}
