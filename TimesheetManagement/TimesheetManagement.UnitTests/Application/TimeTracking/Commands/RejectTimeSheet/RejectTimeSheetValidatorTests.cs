using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.TimeTracking.Commands.RejectTimeSheet;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.Repositories;
using TimesheetManagement.Domain.TimeTracking.ValueObjects;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.TimeTracking.Commands.RejectTimeSheet;

public class RejectTimeSheetValidatorTests
{
    private readonly Mock<ITimeSheetRepository> _repoMock;
    private readonly Mock<IUserContext> _contextMock;
    private readonly RejectTimeSheetValidator _validator;

    public RejectTimeSheetValidatorTests()
    {
        _repoMock = new Mock<ITimeSheetRepository>();
        _contextMock = new Mock<IUserContext>();
        _validator = new RejectTimeSheetValidator(_repoMock.Object, _contextMock.Object);
    }

    [Fact]
    public async Task Validate_ValidCommand_ShouldPass()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var ts = new TimeSheet(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddDays(7)));
        ts.AddEntry(new TimeEntry(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), new HoursWorked(8)));
        ts.Submit();
        var command = new RejectTimeSheetCommand(tsId, Guid.NewGuid(), "Rejected");
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
        var command = new RejectTimeSheetCommand(Guid.Empty, Guid.NewGuid(), "Rejected");
        _contextMock.Setup(x => x.IsInRole("Admin")).Returns(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "TimeSheetId");
    }

    [Fact]
    public async Task Validate_EmptyComment_ShouldFail()
    {
        // Arrange
        var command = new RejectTimeSheetCommand(Guid.NewGuid(), Guid.NewGuid(), "");
        _contextMock.Setup(x => x.IsInRole("Admin")).Returns(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Comment");
    }

    [Fact]
    public async Task Validate_NotAdmin_ShouldFail()
    {
        // Arrange
        var command = new RejectTimeSheetCommand(Guid.NewGuid(), Guid.NewGuid(), "Rejected");
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
        var command = new RejectTimeSheetCommand(tsId, Guid.NewGuid(), "Rejected");
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync((TimeSheet?)null);
        _contextMock.Setup(x => x.IsInRole("Admin")).Returns(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("submitted to reject"));
    }

    [Fact]
    public async Task Validate_TimeSheetNotSubmitted_ShouldFail()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var ts = new TimeSheet(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddDays(7)));
        var command = new RejectTimeSheetCommand(tsId, Guid.NewGuid(), "Rejected");
        _repoMock.Setup(x => x.GetAsync(tsId, It.IsAny<CancellationToken>())).ReturnsAsync(ts);
        _contextMock.Setup(x => x.IsInRole("Admin")).Returns(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("submitted to reject"));
    }
}