using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TimesheetManagement.Domain.Projects;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.IntegrationTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.IntegrationTests.Persistence.InMemory;

[Trait("Category","InMemory")]
public class AppDbContextInMemoryTests : IClassFixture<InMemoryDatabaseFixture>
{
    private readonly InMemoryDatabaseFixture _fixture;

    public AppDbContextInMemoryTests(InMemoryDatabaseFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task BasicCRUD_ShouldWork()
    {
        // Arrange
        var project = new Project("PROJ_TEST", "Test Project", "Tech");

        // Act
        await _fixture.DbContext.Projects.AddAsync(project);
        await _fixture.DbContext.SaveChangesAsync();

        var loaded = await _fixture.DbContext.Projects
            .FirstOrDefaultAsync(p => p.Code == "PROJ_TEST");

        // Assert
        loaded.Should().NotBeNull();
        loaded!.Name.Should().Be("Test Project");
    }

    [Fact]
    public async Task OwnedTypes_ShouldMap_TimeSheetPeriodAndEntries()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var from = new DateOnly(2024, 1, 1);
        var to = new DateOnly(2024, 1, 7);
        var ts = new TimeSheet(userId, from, to);
        ts.AddEntry(new TimeEntry(Guid.NewGuid(), from, 
            new Domain.TimeTracking.ValueObjects.HoursWorked(8), "Work"));

        // Act
        await _fixture.DbContext.TimeSheets.AddAsync(ts);
        await _fixture.DbContext.SaveChangesAsync();

        var loaded = await _fixture.DbContext.TimeSheets
            .Include(s => s.Entries)
            .FirstOrDefaultAsync(s => s.Id == ts.Id);

        // Assert
        loaded.Should().NotBeNull();
        loaded!.Period.From.Should().Be(from);
        loaded.Period.To.Should().Be(to);
        loaded.Entries.Should().ContainSingle()
            .Which.Notes.Should().Be("Work");
    }
}