using FluentAssertions;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Expenses.ValueObjects;
using TimesheetManagement.Infrastructure.Repositories;
using TimesheetManagement.IntegrationTests.TestHelpers;

namespace TimesheetManagement.IntegrationTests.Repositories;

public class ExpenseReportRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public ExpenseReportRepositoryTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AddAndRetrieveExpenseReport_WithItems_ShouldPersist()
    {
        // Arrange
        var repo = new ExpenseReportRepository(_fixture.DbContext);
        var userId = Guid.NewGuid();
        var report = new ExpenseReport(userId, new DateOnly(2025, 1, 1), new DateOnly(2025, 1, 7));
        var item = new ExpenseItem(new DateOnly(2025, 1, 2), "Meals", new Money(25m, "USD"), "receipt.jpg", "Lunch");
        report.AddItem(item);

        // Act
        await repo.AddAsync(report);
        await _fixture.DbContext.SaveChangesAsync();

        // Assert
        var loaded = await repo.GetAsync(report.Id);
        loaded.Should().NotBeNull();
        loaded!.UserId.Should().Be(userId);
        loaded.Items.Should().HaveCount(1);
        loaded.Items.First().Amount.Should().Be(new Money(25m, "USD"));
    }

    [Fact]
    public async Task GetForUserAsync_ShouldReturnReports()
    {
        // Arrange
        await TestDataSeeder.SeedBasicDataAsync(_fixture.DbContext);
        var repo = new ExpenseReportRepository(_fixture.DbContext);
        var userId = Guid.NewGuid();
        var report1 = new ExpenseReport(userId, new DateOnly(2023, 1, 1), new DateOnly(2023, 1, 31));
        var report2 = new ExpenseReport(userId, new DateOnly(2023, 2, 1), new DateOnly(2023, 2, 28));
        await repo.AddAsync(report1);
        await repo.AddAsync(report2);
        await _fixture.DbContext.SaveChangesAsync();

        // Act
        var reports = await repo.GetForUserAsync(userId);

        // Assert
        reports.Should().HaveCount(2);
        reports.Should().Contain(r => r.Id == report1.Id);
        reports.Should().Contain(r => r.Id == report2.Id);
    }
}
