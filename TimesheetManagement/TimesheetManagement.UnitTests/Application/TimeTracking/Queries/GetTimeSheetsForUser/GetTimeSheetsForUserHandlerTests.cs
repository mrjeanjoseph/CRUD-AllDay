using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimesheetManagement.Application.TimeTracking.Queries.GetTimeSheetsForUser;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.Repositories;
using TimesheetManagement.Domain.TimeTracking.ValueObjects;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.TimeTracking.Queries.GetTimeSheetsForUser;

public class GetTimeSheetsForUserHandlerTests
{
    private readonly Mock<ITimeSheetRepository> _repoMock;
    private readonly GetTimeSheetsForUserHandler _handler;

    public GetTimeSheetsForUserHandlerTests()
    {
        _repoMock = new Mock<ITimeSheetRepository>();
        _handler = new GetTimeSheetsForUserHandler(_repoMock.Object);
    }

    [Fact]
    public async Task Handle_ValidQuery_ShouldReturnDtos()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var from = DateOnly.FromDateTime(DateTime.Today);
        var to = from.AddDays(7);
        var ts1 = new TimeSheet(userId, from, to);
        ts1.AddEntry(new TimeEntry(Guid.NewGuid(), from, new HoursWorked(8)));
        var ts2 = new TimeSheet(userId, from.AddDays(7), to.AddDays(7));
        var sheets = new List<TimeSheet> { ts1, ts2 };
        var query = new GetTimeSheetsForUserQuery(userId, from, to);
        _repoMock.Setup(x => x.GetForUserAsync(userId, from, to, default)).ReturnsAsync(sheets);

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().HaveCount(2);
        result.First().Id.Should().Be(ts1.Id);
        result.First().Status.Should().Be("Draft");
        result.First().EntryCount.Should().Be(1);
        result.Last().EntryCount.Should().Be(0);
        _repoMock.Verify(x => x.GetForUserAsync(userId, from, to, default), Times.Once);
    }
}