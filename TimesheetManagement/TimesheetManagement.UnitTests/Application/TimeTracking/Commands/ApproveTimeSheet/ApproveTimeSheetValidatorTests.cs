using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.TimeTracking.Commands.ApproveTimeSheet;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.Repositories;
using TimesheetManagement.Domain.TimeTracking.ValueObjects;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.TimeTracking.Commands.ApproveTimeSheet;

public class ApproveTimeSheetValidatorTests
{
    private readonly Mock<ITimeSheetRepository> _repoMock;
    private readonly Mock<IUserContext> _contextMock;
    private readonly ApproveTimeSheetValidator _validator;

    public ApproveTimeSheetValidatorTests()
    {
        _repoMock = new Mock<ITimeSheetRepository>();
        _contextMock = new Mock<IUserContext>();
        _validator = new ApproveTimeSheetValidator(_repoMock.Object, _contextMock.Object);
    }

    [Fact]
    public async Task Validate_ValidCommand_ShouldPass()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var ts = new TimeSheet(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddDays(7)));
        ts.AddEntry(new TimeEntry(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), new HoursWorked(8)));
        ts.Submit();
        var command = new ApproveTimeSheetCommand(tsId, Guid.NewGuid(), "Approved");
        _repoMock.Setup(x => x.GetAsync(tsId, It.IsAny<CancellationToken>())).ReturnsAsync(ts);
        _contextMock.Setup(x => x.IsInRole("Admin")).Returns(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyTimeSheetId_ShouldFail()
    {
        // Arrange
        var command = new ApproveTimeSheetCommand(Guid.Empty, Guid.NewGuid(), "Approved");
        _contextMock.Setup(x => x.IsInRole("Admin")).Returns(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "TimeSheetId");
    }

    [Fact]
    public async Task Validate_NotAdmin_ShouldFail()
    {
        // Arrange
        var command = new ApproveTimeSheetCommand(Guid.NewGuid(), Guid.NewGuid(), "Approved");
        _contextMock.Setup(x => x.IsInRole("Admin")).Returns(false);
        _contextMock.Setup(x => x.IsInRole("SuperAdmin")).Returns(false);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("Only Admin or SuperAdmin"));
    }

    [Fact]
    public async Task Validate_TimeSheetNotFound_ShouldFail()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var command = new ApproveTimeSheetCommand(tsId, Guid.NewGuid(), "Approved");
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync((TimeSheet?)null);
        _contextMock.Setup(x => x.IsInRole("Admin")).Returns(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("submitted to approve"));
    }

    [Fact]
    public async Task Validate_TimeSheetNotSubmitted_ShouldFail()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var ts = new TimeSheet(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddDays(7)));
        typeof(TimeSheet).GetProperty("Status")!.SetValue(ts, TimeSheetStatus.Submitted);
        var command = new ApproveTimeSheetCommand(tsId, Guid.NewGuid(), "Approved");
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync(ts);
        _contextMock.Setup(x => x.IsInRole("Admin")).Returns(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("submitted to approve"));
    }
}