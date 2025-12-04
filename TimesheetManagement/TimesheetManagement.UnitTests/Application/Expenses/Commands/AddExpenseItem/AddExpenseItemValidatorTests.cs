using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Expenses.Commands.AddExpenseItem;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Expenses.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Expenses.Commands.AddExpenseItem;

public class AddExpenseItemValidatorTests
{
    private readonly Mock<IExpenseReportRepository> _repoMock;
    private readonly AddExpenseItemValidator _validator;

    public AddExpenseItemValidatorTests()
    {
        _repoMock = ApplicationTestHelpers.CreateExpenseReportRepository();
        _validator = new AddExpenseItemValidator(_repoMock.Object);
    }

    [Fact]
    public async Task Validate_ValidCommand_ShouldPass()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        var report = TestData.CreateSampleExpenseReport(id: reportId);
        var command = new AddExpenseItemCommand(reportId, new DateOnly(2023, 1, 1), "Travel", 100.0m, "USD", null, "Test notes");
        _repoMock.Setup(x => x.GetAsync(reportId, It.IsAny<CancellationToken>())).ReturnsAsync(report);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyCategory_ShouldFail()
    {
        // Arrange
        var command = new AddExpenseItemCommand(Guid.NewGuid(), new DateOnly(2023, 1, 1), "", 100.0m, "USD", null, "Test notes");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Category");
    }

    [Fact]
    public async Task Validate_InvalidAmount_ShouldFail()
    {
        // Arrange
        var command = new AddExpenseItemCommand(Guid.NewGuid(), new DateOnly(2023, 1, 1), "Travel", -1.0m, "USD", null, "Test notes");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Amount");
    }

    [Fact]
    public async Task Validate_ReportNotFound_ShouldFail()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        var command = new AddExpenseItemCommand(reportId, new DateOnly(2023, 1, 1), "Travel", 100.0m, "USD", null, "Test notes");
        _repoMock.Setup(x => x.GetAsync(reportId, It.IsAny<CancellationToken>())).ReturnsAsync((ExpenseReport?)null);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("Expense report must be draft"));
    }
}