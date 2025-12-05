using FluentAssertions;
using TimesheetManagement.Domain.Projects;
using TimesheetManagement.Infrastructure.Repositories;
using TimesheetManagement.IntegrationTests.TestHelpers;

namespace TimesheetManagement.IntegrationTests.Repositories;

public class ProjectRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public ProjectRepositoryTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AddProject_CodeExists_ShouldPersistAndCheckUniqueness()
    {
        // Arrange
        var repo = new ProjectRepository(_fixture.DbContext);
        var code = Guid.NewGuid().ToString().Substring(0, 8);
        var project = new Project(code, "Test Project", "Tech");

        // Act
        await repo.AddAsync(project);
        await _fixture.DbContext.SaveChangesAsync();

        // Assert
        var exists = await repo.CodeExistsAsync(code);
        exists.Should().BeTrue();

        var loaded = await repo.GetByCodeAsync(code);
        loaded.Should().NotBeNull();
        loaded!.Name.Should().Be("Test Project");
        loaded.Industry.Should().Be("Tech");
    }

    [Fact]
    public async Task ArchiveAndRestore_ShouldUpdateState()
    {
        // Arrange
        var repo = new ProjectRepository(_fixture.DbContext);
        var code = Guid.NewGuid().ToString().Substring(0, 8);
        var project = new Project(code, "Archivable Project", "Ops");
        await repo.AddAsync(project);
        await _fixture.DbContext.SaveChangesAsync();

        // Act
        project.Archive();
        await repo.UpdateAsync(project);
        await _fixture.DbContext.SaveChangesAsync();

        // Assert
        var loaded = await repo.GetByCodeAsync(code);
        loaded!.IsArchived.Should().BeTrue();

        // Restore
        project.Restore();
        await repo.UpdateAsync(project);
        await _fixture.DbContext.SaveChangesAsync();

        loaded = await repo.GetByCodeAsync(code);
        loaded!.IsArchived.Should().BeFalse();
    }
}
