using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Teams.Commands.ArchiveTeam;
using TimesheetManagement.Domain.Teams;
using TimesheetManagement.Domain.Teams.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Teams.Commands.ArchiveTeam;

public class ArchiveTeamHandlerTests
{
    private readonly Mock<ITeamRepository> _repoMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly ArchiveTeamHandler _handler;

    public ArchiveTeamHandlerTests()
    {
        _repoMock = ApplicationTestHelpers.CreateTeamRepository();
        _uowMock = new Mock<IUnitOfWork>();
        _handler = new ArchiveTeamHandler(_repoMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldArchiveAndReturnTrue()
    {
        // Arrange
        var teamId = Guid.NewGuid();
        var team = TestData.CreateSampleTeam(teamId);
        var command = new ArchiveTeamCommand(teamId);
        _repoMock.Setup(x => x.GetAsync(teamId, default)).ReturnsAsync(team);
        _repoMock.Setup(x => x.UpdateAsync(team, default)).Returns(Task.CompletedTask);
        _uowMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().BeTrue();
        team.IsArchived.Should().BeTrue();
        _repoMock.Verify(x => x.GetAsync(teamId, default), Times.Once);
        _repoMock.Verify(x => x.UpdateAsync(team, default), Times.Once);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task Handle_TeamNotFound_ShouldThrow()
    {
        // Arrange
        var teamId = Guid.NewGuid();
        var command = new ArchiveTeamCommand(teamId);
        _repoMock.Setup(x => x.GetAsync(teamId, default)).ReturnsAsync((TimesheetManagement.Domain.Teams.Team?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, default);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Team not found");
        _repoMock.Verify(x => x.UpdateAsync(It.IsAny<Team>(), default), Times.Never);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Never);
    }
}