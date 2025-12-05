using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.ValueObjects;
using TimesheetManagement.Infrastructure.Repositories;
using TimesheetManagement.IntegrationTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.IntegrationTests.Repositories.InMemory;

[Trait("Category", "InMemory")]
public class TimeSheetRepositoryInMemoryTests : IClassFixture<InMemoryDatabaseFixture>
{
    private readonly InMemoryDatabaseFixture _fixture;

    public TimeSheetRepositoryInMemoryTests(InMemoryDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AddAndRetrieveTimeSheet_WithEntries_ShouldPersist()
    {
        // Arrange
        var repo = new TimeSheetRepository(_fixture.DbContext);
        var userId = Guid.NewGuid();
        var sheet = new TimeSheet(userId, new DateOnly(2025, 1, 1), new DateOnly(2025, 1, 7));
        var entry = new TimeEntry(Guid.NewGuid(), new DateOnly(2025, 1, 2), new HoursWorked(8m), "Work");
        sheet.AddEntry(entry);

        // Act
        await repo.AddAsync(sheet);
        await _fixture.DbContext.SaveChangesAsync();

        // Assert
        var loaded = await repo.GetAsync(sheet.Id);
        loaded.Should().NotBeNull();
        loaded!.UserId.Should().Be(userId);
        loaded.Entries.Should().HaveCount(1);
        loaded.Entries.First().Hours.Should().Be(new HoursWorked(8m));
    }
}
