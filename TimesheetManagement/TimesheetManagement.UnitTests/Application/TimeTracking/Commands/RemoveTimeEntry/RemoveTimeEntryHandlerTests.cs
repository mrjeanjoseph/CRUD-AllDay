using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.TimeTracking.Commands.RemoveTimeEntry;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.Repositories;
using TimesheetManagement.Domain.TimeTracking.ValueObjects;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.TimeTracking.Commands.RemoveTimeEntry;

public class RemoveTimeEntryHandlerTests
{
    private readonly Mock<ITimeSheetRepository> _repoMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly RemoveTimeEntryHandler _handler;

    public RemoveTimeEntryHandlerTests()
    {
        _repoMock = new Mock<ITimeSheetRepository>();
        _uowMock = new Mock<IUnitOfWork>();
        _handler = new RemoveTimeEntryHandler(_repoMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldRemoveEntryAndReturnTrue()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var entryId = Guid.NewGuid();
        var ts = new TimeSheet(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddDays(7)));
        var entry = new TimeEntry(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), new HoursWorked(8));
        ts.AddEntry(entry);
        // Set entry Id
        typeof(TimesheetManagement.Domain.Common.Entity).GetProperty("Id")!.SetValue(entry, entryId);
        var command = new RemoveTimeEntryCommand(tsId, entryId);
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync(ts);
        _repoMock.Setup(x => x.UpdateAsync(ts, default)).Returns(Task.CompletedTask);
        _uowMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().BeTrue();
        ts.Entries.Should().BeEmpty();
        _repoMock.Verify(x => x.GetAsync(tsId, default), Times.Once);
        _repoMock.Verify(x => x.UpdateAsync(ts, default), Times.Once);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task Handle_TimeSheetNotFound_ShouldThrow()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var command = new RemoveTimeEntryCommand(tsId, Guid.NewGuid());
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync((TimeSheet?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, default);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Timesheet not found");
        _repoMock.Verify(x => x.UpdateAsync(It.IsAny<TimeSheet>(), default), Times.Never);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Never);
    }
}