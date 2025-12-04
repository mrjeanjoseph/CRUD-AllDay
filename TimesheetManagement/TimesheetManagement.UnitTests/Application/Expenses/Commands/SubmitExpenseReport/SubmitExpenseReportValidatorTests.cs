using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Expenses.Commands.SubmitExpenseReport;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Expenses.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Expenses.Commands.SubmitExpenseReport;

public class SubmitExpenseReportValidatorTests
{
    private readonly Mock<IExpenseReportRepository> _repoMock;
    private readonly SubmitExpenseReportValidator _validator;

    public SubmitExpenseReportValidatorTests()
    {
        _repoMock = ApplicationTestHelpers.CreateExpenseReportRepository();
        _validator = new SubmitExpenseReportValidator(_repoMock.Object);
    }

    [Fact]
    public async Task Validate_ValidCommand_ShouldPass()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        var report = TestData.CreateSampleExpenseReport(id: reportId);
        report.AddItem(new ExpenseItem(new DateOnly(2023, 1, 1), "Travel", TestData.SampleMoney, null, null));
        var command = new SubmitExpenseReportCommand(reportId);
        _repoMock.Setup(x => x.GetAsync(reportId, It.IsAny<CancellationToken>())).ReturnsAsync(report);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyReportId_ShouldFail()
    {
        // Arrange
        var command = new SubmitExpenseReportCommand(Guid.Empty);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "ExpenseReportId");
    }

    [Fact]
    public async Task Validate_ReportNotDraft_ShouldFail()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        var report = TestData.CreateSampleExpenseReport(id: reportId);
        report.AddItem(new ExpenseItem(new DateOnly(2023, 1, 1), "Travel", TestData.SampleMoney, null, null));
        typeof(ExpenseReport).GetProperty("Status")!.SetValue(report, ExpenseStatus.Submitted);
        var command = new SubmitExpenseReportCommand(reportId);
        _repoMock.Setup(x => x.GetAsync(reportId, It.IsAny<CancellationToken>())).ReturnsAsync(report);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("must be draft"));
    }
}