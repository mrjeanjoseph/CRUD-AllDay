using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TimesheetManagement.Application.Identity.Queries.GetUserById;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.Domain.Identity.Repositories;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Identity.Queries.GetUserById;

public class GetUserByIdHandlerTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly GetUserByIdHandler _handler;

    public GetUserByIdHandlerTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _handler = new GetUserByIdHandler(_userRepoMock.Object);
    }

    [Fact]
    public async Task Handle_UserExists_ShouldReturnUserDto()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User("testuser", new Email("test@example.com"));
        var idProperty = typeof(User).GetProperty("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        idProperty!.SetValue(user, userId);
        _userRepoMock.Setup(x => x.GetAsync(userId, default)).ReturnsAsync(user);
        var query = new GetUserByIdQuery(userId);

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(userId);
        result.Username.Should().Be("testuser");
        result.Email.Should().Be("test@example.com");
    }

    [Fact]
    public async Task Handle_UserNotFound_ShouldThrowException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userRepoMock.Setup(x => x.GetAsync(userId, default)).ReturnsAsync((User)null);
        var query = new GetUserByIdQuery(userId);

        // Act
        Func<Task> act = async () => await _handler.Handle(query, default);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("User not found");
    }
}