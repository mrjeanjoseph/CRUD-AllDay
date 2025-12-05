using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetManagement.Application.Teams.Queries.GetTeamById;
using TimesheetManagement.Domain.Teams.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Teams.Queries.GetTeamById;

public class GetTeamByIdHandlerTests
{
    private readonly Mock<ITeamRepository> _repoMock;
    private readonly GetTeamByIdHandler _handler;

    public GetTeamByIdHandlerTests()
    {
        _repoMock = ApplicationTestHelpers.CreateTeamRepository();
        _handler = new GetTeamByIdHandler(_repoMock.Object);
    }

    [Fact]
    public async Task Handle_ValidQuery_ShouldReturnDto()
    {
        // Arrange
        var teamId = Guid.NewGuid();
        var team = TestData.CreateSampleTeam(teamId);
        var userId = Guid.NewGuid();
        team.AddMember(userId);
        var query = new GetTeamByIdQuery(teamId);
        _repoMock.Setup(x => x.GetAsync(teamId, default)).ReturnsAsync(team);

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(teamId);
        result.Name.Should().Be("Test Team");
        result.IsArchived.Should().BeFalse();
        result.MemberIds.Should().Contain(userId);
        _repoMock.Verify(x => x.GetAsync(teamId, default), Times.Once);
    }

    [Fact]
    public async Task Handle_TeamNotFound_ShouldThrow()
    {
        // Arrange
        var teamId = Guid.NewGuid();
        var query = new GetTeamByIdQuery(teamId);
        _repoMock.Setup(x => x.GetAsync(teamId, default)).ReturnsAsync((TimesheetManagement.Domain.Teams.Team?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(query, default);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Team not found");
    }
}