using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Expenses.Commands.RejectExpenseReport;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Expenses.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Expenses.Commands.RejectExpenseReport;

public class RejectExpenseReportHandlerTests
{
    private readonly Mock<IExpenseReportRepository> _repoMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly RejectExpenseReportHandler _handler;

    public RejectExpenseReportHandlerTests()
    {
        _repoMock = ApplicationTestHelpers.CreateExpenseReportRepository();
        _uowMock = new Mock<IUnitOfWork>();
        _handler = new RejectExpenseReportHandler(_repoMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldRejectReport()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        var report = TestData.CreateSampleExpenseReport(id: reportId);
        typeof(ExpenseReport).GetProperty("Status")!.SetValue(report, ExpenseStatus.Submitted);
        var command = new RejectExpenseReportCommand(reportId, Guid.NewGuid(), "Rejected");
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
}