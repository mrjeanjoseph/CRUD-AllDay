using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using TimesheetManagement.Application.TimeTracking.Commands.CreateTimeSheet;
using TimesheetManagement.Domain.TimeTracking.Repositories;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.TimeTracking.Commands.CreateTimeSheet;

public class CreateTimeSheetValidatorTests
{
    private readonly Mock<ITimeSheetRepository> _repoMock;
    private readonly CreateTimeSheetValidator _validator;

    public CreateTimeSheetValidatorTests()
    {
        _repoMock = new Mock<ITimeSheetRepository>();
        _validator = new CreateTimeSheetValidator(_repoMock.Object);
    }

    [Fact]
    public async Task Validate_ValidCommand_ShouldPass()
    {
        // Arrange
        var command = new CreateTimeSheetCommand(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddDays(7)));
        _repoMock.Setup(x => x.HasSubmittedForRangeAsync(It.IsAny<Guid>(), It.IsAny<DateOnly>(), It.IsAny<DateOnly>(), default)).ReturnsAsync(false);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyUserId_ShouldFail()
    {
        // Arrange
        var command = new CreateTimeSheetCommand(Guid.Empty, DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddDays(7)));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "UserId");
    }

    [Fact]
    public async Task Validate_ToBeforeFrom_ShouldFail()
    {
        // Arrange
        var command = new CreateTimeSheetCommand(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddDays(-1)));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "To");
    }

    [Fact]
    public async Task Validate_OverlapExists_ShouldFail()
    {
        // Arrange
        var command = new CreateTimeSheetCommand(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddDays(7)));
        _repoMock.Setup(x => x.HasSubmittedForRangeAsync(It.IsAny<Guid>(), It.IsAny<DateOnly>(), It.IsAny<DateOnly>(), default)).ReturnsAsync(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("submitted timesheet already exists"));
    }
}