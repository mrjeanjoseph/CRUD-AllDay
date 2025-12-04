using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetManagement.Application.Identity.Queries.GetUserByEmail;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.Domain.Identity.Repositories;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Identity.Queries.GetUserByEmail;

public class GetUserByEmailHandlerTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly GetUserByEmailHandler _handler;

    public GetUserByEmailHandlerTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _handler = new GetUserByEmailHandler(_userRepoMock.Object);
    }

    [Fact]
    public async Task Handle_UserExists_ShouldReturnUserDto()
    {
        // Arrange
        var emailStr = "test@example.com";
        var email = new Email(emailStr);
        var user = new User("testuser", email);
        var userId = Guid.NewGuid();
        var idProperty = typeof(User).GetProperty("Id");
        idProperty.SetValue(user, userId);
        _userRepoMock.Setup(x => x.GetByEmailAsync(email, default)).ReturnsAsync(user);
        var query = new GetUserByEmailQuery(emailStr);

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
        result.Username.Should().Be("testuser");
        result.Email.Should().Be(emailStr);
    }

    [Fact]
    public async Task Handle_UserNotFound_ShouldThrowException()
    {
        // Arrange
        var emailStr = "test@example.com";
        var email = new Email(emailStr);
        _userRepoMock.Setup(x => x.GetByEmailAsync(email, default)).ReturnsAsync((User)null);
        var query = new GetUserByEmailQuery(emailStr);

        // Act
        Func<Task> act = async () => await _handler.Handle(query, default);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("User not found");
    }
}