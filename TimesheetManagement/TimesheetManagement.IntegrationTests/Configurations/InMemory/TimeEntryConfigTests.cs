using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.IntegrationTests.TestHelpers;

namespace TimesheetManagement.IntegrationTests.Configurations.InMemory;

[Trait("Category", "InMemory")]
public class TimeEntryConfigTests : IClassFixture<InMemoryDatabaseFixture>
{
    private readonly InMemoryDatabaseFixture _fixture;
    public TimeEntryConfigTests(InMemoryDatabaseFixture fixture) => _fixture = fixture;

    [Fact]
    public void TimeEntryConfig_MapsHoursAndDate()
    {
        var model = _fixture.DbContext.Model;
        var et = model.FindEntityType(typeof(TimeEntry));
        et.Should().NotBeNull();

        var storeId = Microsoft.EntityFrameworkCore.Metadata.StoreObjectIdentifier.Table(et.GetTableName(), et.GetSchema());
        var dateCol = et.GetProperties().FirstOrDefault(p => p.Name == "Date")?.GetColumnName(storeId);
        dateCol.Should().Be("Date");

        var hours = et.GetProperties().FirstOrDefault(p => p.Name == "Hours");
        hours.Should().NotBeNull();
        hours.GetColumnType().Should().Contain("decimal");
    }
}
