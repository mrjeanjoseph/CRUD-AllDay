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

public class RejectExpenseReportValidatorTests
{
    private readonly Mock<IExpenseReportRepository> _repoMock;
    private readonly Mock<IUserContext> _contextMock;
    private readonly RejectExpenseReportValidator _validator;

    public RejectExpenseReportValidatorTests()
    {
        _repoMock = ApplicationTestHelpers.CreateExpenseReportRepository();
        _contextMock = ApplicationTestHelpers.CreateUserContext();
        _validator = new RejectExpenseReportValidator(_repoMock.Object, _contextMock.Object);
    }

    [Fact]
    public async Task Validate_ValidCommand_ShouldPass()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        var report = TestData.CreateSampleExpenseReport(id: reportId);
        typeof(ExpenseReport).GetProperty("Status")!.SetValue(report, ExpenseStatus.Submitted);
        var command = new RejectExpenseReportCommand(reportId, Guid.NewGuid(), "Rejected");
        _repoMock.Setup(x => x.GetAsync(reportId, It.IsAny<CancellationToken>())).ReturnsAsync(report);
        _contextMock.Setup(x => x.IsInRole("Admin")).Returns(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyComment_ShouldFail()
    {
        // Arrange
        var command = new RejectExpenseReportCommand(Guid.NewGuid(), Guid.NewGuid(), "");
        _contextMock.Setup(x => x.IsInRole("Admin")).Returns(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Comment");
    }
}