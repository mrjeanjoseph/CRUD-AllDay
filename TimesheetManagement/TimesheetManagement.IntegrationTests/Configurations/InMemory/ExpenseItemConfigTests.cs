using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.IntegrationTests.TestHelpers;

namespace TimesheetManagement.IntegrationTests.Configurations.InMemory;

[Trait("Category", "InMemory")]
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

        var storeId = Microsoft.EntityFrameworkCore.Metadata.StoreObjectIdentifier.Table(et.GetTableName(), et.GetSchema());

        var amountProp = et.GetProperties().FirstOrDefault(p => p.GetColumnName(storeId) == "Amount");
        amountProp.Should().NotBeNull();
        amountProp.GetColumnType().Should().Contain("decimal");

        var currencyProp = et.GetProperties().FirstOrDefault(p => p.GetColumnName(storeId) == "Currency") ?? et.GetProperties().FirstOrDefault(p => p.Name == "Currency");
        currencyProp.Should().NotBeNull();
        currencyProp.GetMaxLength().Should().Be(3);
        currencyProp.IsUnicode().Should().BeFalse();
    }
}
