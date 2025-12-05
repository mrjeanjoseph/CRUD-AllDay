using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Expenses.Commands.CreateExpenseReport;
using TimesheetManagement.Domain.Expenses.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Expenses.Commands.CreateExpenseReport;

public class CreateExpenseReportValidatorTests
{
    private readonly Mock<IExpenseReportRepository> _repoMock;
    private readonly CreateExpenseReportValidator _validator;

    public CreateExpenseReportValidatorTests()
    {
        _repoMock = ApplicationTestHelpers.CreateExpenseReportRepository();
        _validator = new CreateExpenseReportValidator(_repoMock.Object);
    }

    [Fact]
    public async Task Validate_ValidCommand_ShouldPass()
    {
        // Arrange
        var command = new CreateExpenseReportCommand(Guid.NewGuid(), new DateOnly(2023, 1, 1), new DateOnly(2023, 1, 31));
        _repoMock.Setup(x => x.HasSubmittedForRangeAsync(command.UserId, command.From, command.To, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyUserId_ShouldFail()
    {
        // Arrange
        var command = new CreateExpenseReportCommand(Guid.Empty, new DateOnly(2023, 1, 1), new DateOnly(2023, 1, 31));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "UserId");
    }

    [Fact]
    public async Task Validate_InvalidDateRange_ShouldFail()
    {
        // Arrange
        var command = new CreateExpenseReportCommand(Guid.NewGuid(), new DateOnly(2023, 1, 31), new DateOnly(2023, 1, 1));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "From");
    }
}