using FluentAssertions;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.Infrastructure.Repositories;
using TimesheetManagement.IntegrationTests.TestHelpers;

namespace TimesheetManagement.IntegrationTests.Repositories;

public class RoleAssignmentRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public RoleAssignmentRepositoryTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AddAndGetRoleAssignment_ShouldPersist()
    {
        // Arrange
        var repo = new RoleAssignmentRepository(_fixture.DbContext);
        var adminId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var createdBy = Guid.NewGuid();
        var assignment = RoleAssignment.Create(adminId, userId, createdBy);

        // Act
        await repo.AddAsync(assignment);
        await _fixture.DbContext.SaveChangesAsync();

        // Assert
        var loaded = await repo.GetAsync(assignment.Id);
        loaded.Should().NotBeNull();
        loaded!.AdminId.Should().Be(adminId);
        loaded.UserId.Should().Be(userId);
        loaded.Status.Should().Be(RoleAssignmentStatus.Active);
    }

    [Fact]
    public async Task GetByAdminAsync_ShouldReturnAssignments()
    {
        // Arrange
        var repo = new RoleAssignmentRepository(_fixture.DbContext);
        var adminId = Guid.NewGuid();
        var createdBy = Guid.NewGuid();
        var assignment1 = RoleAssignment.Create(adminId, Guid.NewGuid(), createdBy);
        var assignment2 = RoleAssignment.Create(adminId, Guid.NewGuid(), createdBy);
        await repo.AddAsync(assignment1);
        await repo.AddAsync(assignment2);
        await _fixture.DbContext.SaveChangesAsync();

        // Act
        var assignments = await repo.GetByAdminAsync(adminId);

        // Assert
        assignments.Should().HaveCount(2);
        assignments.Should().Contain(a => a.Id == assignment1.Id);
    }
}