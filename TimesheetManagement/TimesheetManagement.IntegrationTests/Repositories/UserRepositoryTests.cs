using FluentAssertions;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.Infrastructure.Repositories;
using TimesheetManagement.IntegrationTests.TestHelpers;

namespace TimesheetManagement.IntegrationTests.Repositories;

public class UserRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public UserRepositoryTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AddAndGetUser_ShouldPersist()
    {
        // Arrange
        var repo = new UserRepository(_fixture.DbContext);
        var user = new User("testuser", new Email("test@example.com"));

        // Act
        await repo.AddAsync(user);
        await _fixture.DbContext.SaveChangesAsync();

        // Assert
        var loaded = await repo.GetAsync(user.Id);
        loaded.Should().NotBeNull();
        loaded!.Username.Should().Be("testuser");
        loaded.Email.Value.Should().Be("test@example.com");
    }

    [Fact]
    public async Task GetByEmailAsync_ShouldReturnUser()
    {
        // Arrange
        var repo = new UserRepository(_fixture.DbContext);
        var email = new Email("unique@example.com");
        var user = new User("uniqueuser", email);
        await repo.AddAsync(user);
        await _fixture.DbContext.SaveChangesAsync();

        // Act
        var found = await repo.GetByEmailAsync(email);

        // Assert
        found.Should().NotBeNull();
        found!.Id.Should().Be(user.Id);
    }
}