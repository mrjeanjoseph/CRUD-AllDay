using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.TimeTracking.Commands.CreateTimeSheet;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.Repositories;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.TimeTracking.Commands.CreateTimeSheet;

public class CreateTimeSheetHandlerTests
{
    private readonly Mock<ITimeSheetRepository> _repoMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly CreateTimeSheetHandler _handler;

    public CreateTimeSheetHandlerTests()
    {
        _repoMock = new Mock<ITimeSheetRepository>();
        _uowMock = new Mock<IUnitOfWork>();
        _handler = new CreateTimeSheetHandler(_repoMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateAndReturnId()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var from = DateOnly.FromDateTime(DateTime.Today);
        var to = from.AddDays(7);
        var command = new CreateTimeSheetCommand(userId, from, to);
        _repoMock.Setup(x => x.HasSubmittedForRangeAsync(userId, from, to, default)).ReturnsAsync(false);
        _repoMock.Setup(x => x.AddAsync(It.IsAny<TimeSheet>(), default)).Returns(Task.CompletedTask);
        _uowMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().NotBeEmpty();
        _repoMock.Verify(x => x.HasSubmittedForRangeAsync(userId, from, to, default), Times.Once);
        _repoMock.Verify(x => x.AddAsync(It.Is<TimeSheet>(s => s.UserId == userId && s.Period.From == from && s.Period.To == to), default), Times.Once);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task Handle_OverlapExists_ShouldThrow()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var from = DateOnly.FromDateTime(DateTime.Today);
        var to = from.AddDays(7);
        var command = new CreateTimeSheetCommand(userId, from, to);
        _repoMock.Setup(x => x.HasSubmittedForRangeAsync(userId, from, to, default)).ReturnsAsync(true);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, default);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("A submitted timesheet already exists for this period");
        _repoMock.Verify(x => x.AddAsync(It.IsAny<TimeSheet>(), default), Times.Never);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Never);
    }
}