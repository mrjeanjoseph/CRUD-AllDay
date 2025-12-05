using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Expenses.ValueObjects;
using TimesheetManagement.Infrastructure.Persistence;
using TimesheetManagement.Infrastructure.Repositories;
using Xunit;

namespace TimesheetManagement.IntegrationTests.Repositories;

public class ExpenseReportRepositoryTests
{
    private static AppDbContext CreateDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAndLoad_WithItem_ShouldPersist()
    {
        using var db = CreateDb();
        var repo = new ExpenseReportRepository(db);
        var report = new ExpenseReport(Guid.NewGuid(), new DateOnly(2025, 1, 1), new DateOnly(2025, 1, 7));
        var item = new ExpenseItem(new DateOnly(2025, 1, 2), "Meals", new Money(25m, "USD"), null, "Lunch");
        report.AddItem(item);

        await repo.AddAsync(report);
        await db.SaveChangesAsync();

        var loaded = await repo.GetAsync(report.Id);
        loaded.Should().NotBeNull();
        loaded!.Items.Count.Should().Be(1);
    }
}
