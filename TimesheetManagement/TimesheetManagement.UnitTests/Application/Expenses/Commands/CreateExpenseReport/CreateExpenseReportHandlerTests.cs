using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Expenses.Commands.CreateExpenseReport;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Expenses.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Expenses.Commands.CreateExpenseReport;

public class CreateExpenseReportHandlerTests
{
    private readonly Mock<IExpenseReportRepository> _repoMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly CreateExpenseReportHandler _handler;

    public CreateExpenseReportHandlerTests()
    {
        _repoMock = ApplicationTestHelpers.CreateExpenseReportRepository();
        _uowMock = new Mock<IUnitOfWork>();
        _handler = new CreateExpenseReportHandler(_repoMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateExpenseReport()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var from = new DateOnly(2023, 1, 1);
        var to = new DateOnly(2023, 1, 31);
        var command = new CreateExpenseReportCommand(userId, from, to);
        _repoMock.Setup(x => x.AddAsync(It.IsAny<ExpenseReport>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        _uowMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().NotBeEmpty();
        _repoMock.Verify(x => x.AddAsync(It.IsAny<ExpenseReport>(), It.IsAny<CancellationToken>()), Times.Once);
        _uowMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}