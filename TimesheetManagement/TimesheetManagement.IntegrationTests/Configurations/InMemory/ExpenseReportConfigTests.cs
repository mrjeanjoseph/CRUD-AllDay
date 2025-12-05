using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.IntegrationTests.TestHelpers;

namespace TimesheetManagement.IntegrationTests.Configurations.InMemory;

[Trait("Category", "InMemory")]
public class ExpenseReportConfigTests : IClassFixture<InMemoryDatabaseFixture>
{
    private readonly InMemoryDatabaseFixture _fixture;
    public ExpenseReportConfigTests(InMemoryDatabaseFixture fixture) => _fixture = fixture;

    [Fact]
    public void ExpenseReportConfig_MapsPeriodAndCascadeDeleteItems()
    {
        var model = _fixture.DbContext.Model;
        var et = model.FindEntityType(typeof(ExpenseReport));
        et.Should().NotBeNull();

        var storeId = Microsoft.EntityFrameworkCore.Metadata.StoreObjectIdentifier.Table(et.GetTableName(), et.GetSchema());

        var periodNav = et.FindNavigation("Period");
        periodNav.Should().NotBeNull("Period should be configured as owned/complex type");
        var ownedType = periodNav!.TargetEntityType;
        ownedType.Should().NotBeNull();

        var fromProp = ownedType.GetProperties().FirstOrDefault(p => p.Name == "From");
        var toProp = ownedType.GetProperties().FirstOrDefault(p => p.Name == "To");
        fromProp.Should().NotBeNull();
        toProp.Should().NotBeNull();

        var fromCol = fromProp!.GetColumnName(storeId);
        var toCol = toProp!.GetColumnName(storeId);

        fromCol.Should().Be("FromDate");
        toCol.Should().Be("ToDate");

        var items = model.FindEntityType(typeof(ExpenseItem));
        items.Should().NotBeNull();
        var fk = items.GetForeignKeys().FirstOrDefault();
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
