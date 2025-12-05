using FluentAssertions;
using TimesheetManagement.Domain.Teams;
using TimesheetManagement.Infrastructure.Repositories;
using TimesheetManagement.IntegrationTests.TestHelpers;

namespace TimesheetManagement.IntegrationTests.Repositories.InMemory;

[Trait("Category", "InMemory")]
public class TeamRepositoryInMemoryTests : IClassFixture<InMemoryDatabaseFixture>
{
    private readonly InMemoryDatabaseFixture _fixture;

    public TeamRepositoryInMemoryTests(InMemoryDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AddAndGetTeam_WithMembers_ShouldPersist()
    {
        // Arrange
        var repo = new TeamRepository(_fixture.DbContext);
        var team = new Team("Test Team");
        var userId = Guid.NewGuid();
        team.AddMember(userId);

        // Act
        await repo.AddAsync(team);
        await _fixture.DbContext.SaveChangesAsync();

        // Assert
        var loaded = await repo.GetAsync(team.Id);
        loaded.Should().NotBeNull();
        loaded!.Name.Should().Be("Test Team");
        loaded.Members.Should().ContainSingle(m => m.UserId == userId);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTeams()
    {
        // Arrange
        await TestDataSeeder.SeedBasicDataAsync(_fixture.DbContext);
        var repo = new TeamRepository(_fixture.DbContext);

        // Act
        var teams = await repo.GetAllAsync();

        // Assert
        teams.Should().NotBeEmpty();
        teams.Should().Contain(t => t.Name == "Team Alpha");
    }
}
