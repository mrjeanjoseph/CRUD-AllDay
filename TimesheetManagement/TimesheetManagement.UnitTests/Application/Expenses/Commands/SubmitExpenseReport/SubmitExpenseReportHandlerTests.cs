using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Expenses.Commands.SubmitExpenseReport;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Expenses.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Expenses.Commands.SubmitExpenseReport;

public class SubmitExpenseReportHandlerTests
{
    private readonly Mock<IExpenseReportRepository> _repoMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly SubmitExpenseReportHandler _handler;

    public SubmitExpenseReportHandlerTests()
    {
        _repoMock = ApplicationTestHelpers.CreateExpenseReportRepository();
        _uowMock = new Mock<IUnitOfWork>();
        _handler = new SubmitExpenseReportHandler(_repoMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldSubmitReport()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        var report = TestData.CreateSampleExpenseReport(id: reportId);
        report.AddItem(new ExpenseItem(new DateOnly(2023, 1, 1), "Travel", TestData.SampleMoney, null, null));
        var command = new SubmitExpenseReportCommand(reportId);
        _repoMock.Setup(x => x.GetAsync(reportId, It.IsAny<CancellationToken>())).ReturnsAsync(report);
        _repoMock.Setup(x => x.UpdateAsync(report, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        _uowMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().BeTrue();
        _repoMock.Verify(x => x.UpdateAsync(report, It.IsAny<CancellationToken>()), Times.Once);
        _uowMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ReportNotFound_ShouldThrowException()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        var command = new SubmitExpenseReportCommand(reportId);
        _repoMock.Setup(x => x.GetAsync(reportId, It.IsAny<CancellationToken>())).ReturnsAsync((ExpenseReport?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, default);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Expense report not found");
    }
}