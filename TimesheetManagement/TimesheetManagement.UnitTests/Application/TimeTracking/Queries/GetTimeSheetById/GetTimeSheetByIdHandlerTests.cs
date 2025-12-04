using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimesheetManagement.Application.TimeTracking.Queries.GetTimeSheetById;
using TimesheetManagement.Domain.Common;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.Repositories;
using TimesheetManagement.Domain.TimeTracking.ValueObjects;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.TimeTracking.Queries.GetTimeSheetById;

public class GetTimeSheetByIdHandlerTests
{
    private readonly Mock<ITimeSheetRepository> _repoMock;
    private readonly GetTimeSheetByIdHandler _handler;

    public GetTimeSheetByIdHandlerTests()
    {
        _repoMock = new Mock<ITimeSheetRepository>();
        _handler = new GetTimeSheetByIdHandler(_repoMock.Object);
    }

    [Fact]
    public async Task Handle_ValidQuery_ShouldReturnDto()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var from = DateOnly.FromDateTime(DateTime.Today);
        var to = from.AddDays(7);
        var ts = new TimeSheet(userId, from, to);
        typeof(Entity).GetProperty("Id")!.SetValue(ts, tsId);
        var entry = new TimeEntry(Guid.NewGuid(), from, new HoursWorked(8), "Test");
        ts.AddEntry(entry);
        var query = new GetTimeSheetByIdQuery(tsId);
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync(ts);

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(tsId);
        result.UserId.Should().Be(userId);
        result.From.Should().Be(from);
        result.To.Should().Be(to);
        result.Status.Should().Be("Draft");
        result.Entries.Should().HaveCount(1);
        result.Entries.First().Hours.Should().Be(8);
        _repoMock.Verify(x => x.GetAsync(tsId, default), Times.Once);
    }

    [Fact]
    public async Task Handle_TimeSheetNotFound_ShouldThrow()
    {
        // Arrange
        var tsId = Guid.NewGuid();
        var query = new GetTimeSheetByIdQuery(tsId);
        _repoMock.Setup(x => x.GetAsync(tsId, default)).ReturnsAsync((TimeSheet?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(query, default);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Timesheet not found");
    }
}