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

        // Inspect owned navigation 'Period'
        var periodNav = et.FindNavigation("Period");
        periodNav.Should().NotBeNull("Period should be configured as owned/complex type");

        var ownedType = periodNav!.TargetEntityType;
        ownedType.Should().NotBeNull();

        var fromProp = ownedType.GetProperties().FirstOrDefault(p => p.Name == "From");
        var toProp = ownedType.GetProperties().FirstOrDefault(p => p.Name == "To");
        fromProp.Should().NotBeNull("Owned type should have From property");
        toProp.Should().NotBeNull("Owned type should have To property");

        var fromCol = fromProp!.GetColumnName(storeId);
        var toCol = toProp!.GetColumnName(storeId);

        fromCol.Should().Be("FromDate", "TimeSheet Period.From should map to FromDate");
        toCol.Should().Be("ToDate", "TimeSheet Period.To should map to ToDate");

        // cascade delete configured via FK on TimeEntries
        var entries = model.FindEntityType(typeof(TimesheetManagement.Domain.TimeTracking.TimeEntry));
        entries.Should().NotBeNull();
        var fk = entries.GetForeignKeys().FirstOrDefault();
        fk.Should().NotBeNull();
        fk.DeleteBehavior.Should().Be(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

        // Status index exists
        var hasStatusIndex = et.GetIndexes().Any(ix => ix.Properties.Any(p => p.Name == "Status"));
        hasStatusIndex.Should().BeTrue("There should be an index on Status");

        // Concurrency RowVersion
        var rv = et.GetProperties().FirstOrDefault(p => p.Name == "RowVersion");
        rv.Should().NotBeNull();
        rv.IsConcurrencyToken.Should().BeTrue();

        // Comment max length
        var comment = et.GetProperties().FirstOrDefault(p => p.Name == "Comment");
        comment.Should().NotBeNull();
        comment.GetMaxLength().Should().Be(1024);
    }
}
