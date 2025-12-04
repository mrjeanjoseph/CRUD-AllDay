using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.TimeTracking.Commands.AddTimeEntry;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.Repositories;
using TimesheetManagement.Domain.TimeTracking.ValueObjects;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.TimeTracking.Commands.AddTimeEntry;

public class AddTimeEntryHandlerTests
{
    private readonly Mock<ITimeSheetRepository> _repoMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly AddTimeEntryHandler _handler;

    public AddTimeEntryHandlerTests()
    {
        _repoMock = new Mock<ITimeSheetRepository>();
        _uowMock = new Mock<IUnitOfWork>();
        _handler = new AddTimeEntryHandler(_repoMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldAddEntryAndReturnTrue()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var projId = Guid.NewGuid();
        var date = DateOnly.FromDateTime(DateTime.Today);
        var ts = new TimeSheet(Guid.NewGuid(), date, date.AddDays(7));
        var command = new AddTimeEntryCommand(tsId, projId, date, 8, "Test");
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync(ts);
        _repoMock.Setup(x => x.UpdateAsync(ts, default)).Returns(Task.CompletedTask);
        _uowMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().BeTrue();
        ts.Entries.Should().HaveCount(1);
        ts.Entries.First().ProjectId.Should().Be(projId);
        ts.Entries.First().Date.Should().Be(date);
        ts.Entries.First().Hours.Value.Should().Be(8);
        _repoMock.Verify(x => x.GetAsync(tsId, default), Times.Once);
        _repoMock.Verify(x => x.UpdateAsync(ts, default), Times.Once);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task Handle_TimeSheetNotFound_ShouldThrow()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var command = new AddTimeEntryCommand(tsId, Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), 8, "Test");
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync((TimeSheet?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, default);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Timesheet not found");
        _repoMock.Verify(x => x.UpdateAsync(It.IsAny<TimeSheet>(), default), Times.Never);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Never);
    }

    [Fact]
    public async Task Handle_TimeSheetNotDraft_ShouldThrow()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var ts = new TimeSheet(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddDays(7)));
        ts.AddEntry(new TimeEntry(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), new HoursWorked(8)));
        typeof(TimeSheet).GetProperty("Status")!.SetValue(ts, TimeSheetStatus.Submitted);
        var command = new AddTimeEntryCommand(tsId, Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), 8, "Test");
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync(ts);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, default);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Cannot add entry unless timesheet is draft");
        _repoMock.Verify(x => x.UpdateAsync(It.IsAny<TimeSheet>(), default), Times.Never);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Never);
    }
}