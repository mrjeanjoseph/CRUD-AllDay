using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.IntegrationTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.IntegrationTests.Configurations.InMemory;

[Trait("Category","InMemory")]
public class ExpenseItemConfigTests : IClassFixture<InMemoryDatabaseFixture>
{
    private readonly InMemoryDatabaseFixture _fixture;
    public ExpenseItemConfigTests(InMemoryDatabaseFixture fixture) => _fixture = fixture;

    [Fact]
    public void ExpenseItemConfig_MapsAmountAndCurrency()
    {
        var model = _fixture.DbContext.Model;
        var et = model.FindEntityType(typeof(ExpenseItem));
        et.Should().NotBeNull();

        var amount = et.GetProperties().FirstOrDefault(p => p.Name == "Amount");
        amount.Should().NotBeNull();
        amount.GetColumnType().Should().Contain("decimal");

        var currency = et.GetProperties().FirstOrDefault(p => p.Name == "Currency") ?? et.GetProperties().FirstOrDefault(p => p.GetColumnName(Microsoft.EntityFrameworkCore.Metadata.StoreObjectIdentifier.Table(et.GetTableName(), et.GetSchema())) == "Currency");
        currency.Should().NotBeNull();
    }
}
