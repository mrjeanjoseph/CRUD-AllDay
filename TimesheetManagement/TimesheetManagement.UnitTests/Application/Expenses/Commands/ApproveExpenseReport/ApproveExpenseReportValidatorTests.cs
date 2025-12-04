using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Expenses.Commands.ApproveExpenseReport;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Expenses.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Expenses.Commands.ApproveExpenseReport;

public class ApproveExpenseReportValidatorTests
{
    private readonly Mock<IExpenseReportRepository> _repoMock;
    private readonly Mock<IUserContext> _contextMock;
    private readonly ApproveExpenseReportValidator _validator;

    public ApproveExpenseReportValidatorTests()
    {
        _repoMock = ApplicationTestHelpers.CreateExpenseReportRepository();
        _contextMock = ApplicationTestHelpers.CreateUserContext();
        _validator = new ApproveExpenseReportValidator(_repoMock.Object, _contextMock.Object);
    }

    [Fact]
    public async Task Validate_ValidCommand_ShouldPass()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        var report = TestData.CreateSampleExpenseReport(id: reportId);
        typeof(ExpenseReport).GetProperty("Status")!.SetValue(report, ExpenseStatus.Submitted);
        var command = new ApproveExpenseReportCommand(reportId, Guid.NewGuid(), "Approved");
        _repoMock.Setup(x => x.GetAsync(reportId, It.IsAny<CancellationToken>())).ReturnsAsync(report);
        _contextMock.Setup(x => x.IsInRole("Admin")).Returns(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyReportId_ShouldFail()
    {
        // Arrange
        var command = new ApproveExpenseReportCommand(Guid.Empty, Guid.NewGuid(), "Approved");
        _contextMock.Setup(x => x.IsInRole("Admin")).Returns(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "ExpenseReportId");
    }

    [Fact]
    public async Task Validate_NotAdmin_ShouldFail()
    {
        // Arrange
        var command = new ApproveExpenseReportCommand(Guid.NewGuid(), Guid.NewGuid(), "Approved");
        _contextMock.Setup(x => x.IsInRole("Admin")).Returns(false);
        _contextMock.Setup(x => x.IsInRole("SuperAdmin")).Returns(false);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("Only Admin or SuperAdmin"));
    }

    [Fact]
    public async Task Validate_ReportNotSubmitted_ShouldFail()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        var report = TestData.CreateSampleExpenseReport(id: reportId);
        var command = new ApproveExpenseReportCommand(reportId, Guid.NewGuid(), "Approved");
        _repoMock.Setup(x => x.GetAsync(reportId, It.IsAny<CancellationToken>())).ReturnsAsync(report);
        _contextMock.Setup(x => x.IsInRole("Admin")).Returns(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("submitted to approve"));
    }
}