using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.IntegrationTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.IntegrationTests.Configurations.InMemory;

[Trait("Category","InMemory")]
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

        var hasFrom = et.GetProperties().Any(p => p.GetColumnName(storeId) == "FromDate");
        var hasTo = et.GetProperties().Any(p => p.GetColumnName(storeId) == "ToDate");
        hasFrom.Should().BeTrue();
        hasTo.Should().BeTrue();

        var items = model.FindEntityType(typeof(ExpenseItem));
        items.Should().NotBeNull();
        var fk = items.GetForeignKeys().FirstOrDefault();
        fk.Should().NotBeNull();
        fk.DeleteBehavior.Should().Be(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
    }
}
