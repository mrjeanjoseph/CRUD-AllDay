using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using TimesheetManagement.Application.TimeTracking.Commands.SubmitTimeSheet;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.Repositories;
using TimesheetManagement.Domain.TimeTracking.ValueObjects;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.TimeTracking.Commands.SubmitTimeSheet;

public class SubmitTimeSheetValidatorTests
{
    private readonly Mock<ITimeSheetRepository> _repoMock;
    private readonly SubmitTimeSheetValidator _validator;

    public SubmitTimeSheetValidatorTests()
    {
        _repoMock = new Mock<ITimeSheetRepository>();
        _validator = new SubmitTimeSheetValidator(_repoMock.Object);
    }

    [Fact]
    public async Task Validate_ValidCommand_ShouldPass()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var ts = new TimeSheet(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddDays(7)));
        ts.AddEntry(new TimeEntry(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), new HoursWorked(8)));
        var command = new SubmitTimeSheetCommand(tsId);
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync(ts);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyTimeSheetId_ShouldFail()
    {
        // Arrange
        var command = new SubmitTimeSheetCommand(Guid.Empty);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "TimeSheetId");
    }

    [Fact]
    public async Task Validate_TimeSheetNotFound_ShouldFail()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var command = new SubmitTimeSheetCommand(tsId);
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync((TimeSheet?)null);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("contain at least one entry"));
    }

    [Fact]
    public async Task Validate_TimeSheetNotDraft_ShouldFail()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var ts = new TimeSheet(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddDays(7)));
        ts.AddEntry(new TimeEntry(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), new HoursWorked(8)));
        ts.Submit();
        var command = new SubmitTimeSheetCommand(tsId);
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync(ts);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("contain at least one entry"));
    }

    [Fact]
    public async Task Validate_TimeSheetNoEntries_ShouldFail()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var ts = new TimeSheet(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddDays(7)));
        var command = new SubmitTimeSheetCommand(tsId);
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync(ts);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("contain at least one entry"));
    }
}