using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimesheetManagement.Application.Teams.Queries.GetAllTeams;
using TimesheetManagement.Domain.Teams;
using TimesheetManagement.Domain.Teams.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Teams.Queries.GetAllTeams;

public class GetAllTeamsHandlerTests
{
    private readonly Mock<ITeamRepository> _repoMock;
    private readonly GetAllTeamsHandler _handler;

    public GetAllTeamsHandlerTests()
    {
        _repoMock = ApplicationTestHelpers.CreateTeamRepository();
        _handler = new GetAllTeamsHandler(_repoMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllTeamsAsDtos()
    {
        // Arrange
        var team1 = TestData.CreateSampleTeam(Guid.NewGuid());
        var team2 = TestData.CreateSampleTeam(Guid.NewGuid());
        team2.Archive();
        var userId = Guid.NewGuid();
        team1.AddMember(userId);
        var teams = new List<Team> { team1, team2 };
        var query = new GetAllTeamsQuery();
        _repoMock.Setup(x => x.GetAllAsync(default)).ReturnsAsync(teams);

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().HaveCount(2);
        result.First().Id.Should().Be(team1.Id);
        result.First().Name.Should().Be("Test Team");
        result.First().IsArchived.Should().BeFalse();
        result.First().MemberIds.Should().Contain(userId);
        result.Last().IsArchived.Should().BeTrue();
        _repoMock.Verify(x => x.GetAllAsync(default), Times.Once);
    }

    [Fact]
    public async Task Handle_NoTeams_ShouldReturnEmptyList()
    {
        // Arrange
        var query = new GetAllTeamsQuery();
        _repoMock.Setup(x => x.GetAllAsync(default)).ReturnsAsync(new List<Team>());

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().BeEmpty();
    }
}