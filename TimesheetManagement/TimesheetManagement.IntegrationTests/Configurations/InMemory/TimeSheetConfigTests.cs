using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.IntegrationTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.IntegrationTests.Configurations.InMemory;

[Trait("Category","InMemory")]
public class TimeSheetConfigTests : IClassFixture<InMemoryDatabaseFixture>
{
    private readonly InMemoryDatabaseFixture _fixture;
    public TimeSheetConfigTests(InMemoryDatabaseFixture fixture) => _fixture = fixture;

    [Fact]
    public void TimeSheetConfig_MapsPeriodAsFromToAndCascadeDeleteEntries()
    {
        var model = _fixture.DbContext.Model;
        var et = model.FindEntityType(typeof(TimeSheet));
        et.Should().NotBeNull();

        var storeId = Microsoft.EntityFrameworkCore.Metadata.StoreObjectIdentifier.Table(et.GetTableName(), et.GetSchema());

        var hasFrom = et.GetProperties().Any(p => p.GetColumnName(storeId) == "FromDate");
        var hasTo = et.GetProperties().Any(p => p.GetColumnName(storeId) == "ToDate");
        hasFrom.Should().BeTrue("TimeSheet Period.From should map to FromDate");
        hasTo.Should().BeTrue("TimeSheet Period.To should map to ToDate");

        // cascade delete configured via FK on TimeEntries
        var entries = model.FindEntityType(typeof(TimesheetManagement.Domain.TimeTracking.TimeEntry));
        entries.Should().NotBeNull();
        var fk = entries.GetForeignKeys().FirstOrDefault();
        fk.Should().NotBeNull();
        fk.DeleteBehavior.Should().Be(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
    }
}
