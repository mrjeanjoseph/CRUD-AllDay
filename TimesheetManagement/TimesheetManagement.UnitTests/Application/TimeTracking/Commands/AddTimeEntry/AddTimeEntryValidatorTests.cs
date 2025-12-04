using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using TimesheetManagement.Application.TimeTracking.Commands.AddTimeEntry;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.Repositories;
using TimesheetManagement.Domain.TimeTracking.ValueObjects;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.TimeTracking.Commands.AddTimeEntry;

public class AddTimeEntryValidatorTests
{
    private readonly Mock<ITimeSheetRepository> _repoMock;
    private readonly AddTimeEntryValidator _validator;

    public AddTimeEntryValidatorTests()
    {
        _repoMock = new Mock<ITimeSheetRepository>();
        _validator = new AddTimeEntryValidator(_repoMock.Object);
    }

    [Fact]
    public async Task Validate_ValidCommand_ShouldPass()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var projId = Guid.NewGuid();
        var date = DateOnly.FromDateTime(DateTime.Today);
        var ts = new TimeSheet(Guid.NewGuid(), date, date.AddDays(7));
        var command = new AddTimeEntryCommand(tsId, projId, date, 8, "Test");
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync(ts);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyProjectId_ShouldFail()
    {
        // Arrange
        var command = new AddTimeEntryCommand(Guid.NewGuid(), Guid.Empty, DateOnly.FromDateTime(DateTime.Today), 8, "Test");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "ProjectId");
    }

    [Fact]
    public async Task Validate_InvalidHours_ShouldFail()
    {
        // Arrange
        var command = new AddTimeEntryCommand(Guid.NewGuid(), Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), 25, "Test");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Hours");
    }

    [Fact]
    public async Task Validate_TimeSheetNotFound_ShouldFail()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var command = new AddTimeEntryCommand(tsId, Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), 8, "Test");
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync((TimeSheet?)null);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("draft and date within its period"));
    }

    [Fact]
    public async Task Validate_TimeSheetNotDraft_ShouldFail()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var ts = new TimeSheet(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddDays(7)));
        ts.AddEntry(new TimeEntry(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), new HoursWorked(8)));
        typeof(TimeSheet).GetProperty("Status")!.SetValue(ts, TimeSheetStatus.Submitted);
        var command = new AddTimeEntryCommand(tsId, Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), 8, "Test");
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync(ts);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("draft and date within its period"));
    }

    [Fact]
    public async Task Validate_DateNotInPeriod_ShouldFail()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var ts = new TimeSheet(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddDays(7)));
        var command = new AddTimeEntryCommand(tsId, Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today.AddDays(-1)), 8, "Test");
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync(ts);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("draft and date within its period"));
    }
}