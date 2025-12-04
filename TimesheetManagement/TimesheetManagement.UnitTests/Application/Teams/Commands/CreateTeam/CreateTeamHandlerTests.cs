using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Teams.Commands.CreateTeam;
using TimesheetManagement.Domain.Teams;
using TimesheetManagement.Domain.Teams.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Teams.Commands.CreateTeam;

public class CreateTeamHandlerTests
{
    private readonly Mock<ITeamRepository> _repoMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly CreateTeamHandler _handler;

    public CreateTeamHandlerTests()
    {
        _repoMock = ApplicationTestHelpers.CreateTeamRepository();
        _uowMock = new Mock<IUnitOfWork>();
        _handler = new CreateTeamHandler(_repoMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateAndReturnId()
    {
        // Arrange
        var command = new CreateTeamCommand("Test Team");
        _repoMock.Setup(x => x.NameExistsAsync("Test Team", default)).ReturnsAsync(false);
        _repoMock.Setup(x => x.AddAsync(It.IsAny<Team>(), default)).Returns(Task.CompletedTask);
        _uowMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().NotBeEmpty();
        _repoMock.Verify(x => x.NameExistsAsync("Test Team", default), Times.Once);
        _repoMock.Verify(x => x.AddAsync(It.Is<Team>(t => t.Name == "Test Team"), default), Times.Once);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task Handle_NameExists_ShouldThrow()
    {
        // Arrange
        var command = new CreateTeamCommand("Test Team");
        _repoMock.Setup(x => x.NameExistsAsync("Test Team", default)).ReturnsAsync(true);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, default);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Team name already exists");
        _repoMock.Verify(x => x.AddAsync(It.IsAny<Team>(), default), Times.Never);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Never);
    }
}