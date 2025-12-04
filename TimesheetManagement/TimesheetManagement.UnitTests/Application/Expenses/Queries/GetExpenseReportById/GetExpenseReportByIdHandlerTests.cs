using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Expenses.Queries.GetExpenseReportById;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Expenses.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Expenses.Queries.GetExpenseReportById;

public class GetExpenseReportByIdHandlerTests
{
    private readonly Mock<IExpenseReportRepository> _repoMock;
    private readonly GetExpenseReportByIdHandler _handler;

    public GetExpenseReportByIdHandlerTests()
    {
        _repoMock = ApplicationTestHelpers.CreateExpenseReportRepository();
        _handler = new GetExpenseReportByIdHandler(_repoMock.Object);
    }

    [Fact]
    public async Task Handle_ReportExists_ShouldReturnDto()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        var report = TestData.CreateSampleExpenseReport(id: reportId);
        report.AddItem(new ExpenseItem(new DateOnly(2023, 1, 1), "Travel", TestData.SampleMoney, null, null));
        _repoMock.Setup(x => x.GetAsync(reportId, It.IsAny<CancellationToken>())).ReturnsAsync(report);
        var query = new GetExpenseReportByIdQuery(reportId);

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(reportId);
        result.Items.Should().HaveCount(1);
    }

    [Fact]
    public async Task Handle_ReportNotFound_ShouldThrowException()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        _repoMock.Setup(x => x.GetAsync(reportId, It.IsAny<CancellationToken>())).ReturnsAsync((ExpenseReport?)null);
        var query = new GetExpenseReportByIdQuery(reportId);

        // Act
        Func<Task> act = async () => await _handler.Handle(query, default);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Expense report not found");
    }
}