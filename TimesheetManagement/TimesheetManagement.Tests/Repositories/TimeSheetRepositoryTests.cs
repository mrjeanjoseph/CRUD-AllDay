using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.ValueObjects;
using TimesheetManagement.Infrastructure.Persistence;
using TimesheetManagement.Infrastructure.Repositories;
using Xunit;

namespace TimesheetManagement.Tests.Repositories;

public class TimeSheetRepositoryTests
{
    private static AppDbContext CreateDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAndLoad_WithEntry_ShouldPersist()
    {
        using var db = CreateDb();
        var repo = new TimeSheetRepository(db);
        var sheet = new TimeSheet(Guid.NewGuid(), new DateOnly(2025, 1, 1), new DateOnly(2025, 1, 7));
        var entry = new TimeEntry(Guid.NewGuid(), new DateOnly(2025, 1, 2), new HoursWorked(8m), "Work");
        sheet.AddEntry(entry);

        await repo.AddAsync(sheet);
        await db.SaveChangesAsync();

        var loaded = await repo.GetAsync(sheet.Id);
        loaded.Should().NotBeNull();
        loaded!.Entries.Count.Should().Be(1);
    }
}
