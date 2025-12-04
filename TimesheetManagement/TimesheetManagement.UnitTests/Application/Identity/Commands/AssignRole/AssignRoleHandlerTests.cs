using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Identity.Commands.AssignRole;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.Domain.Identity.Repositories;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Identity.Commands.AssignRole;

public class AssignRoleHandlerTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly AssignRoleHandler _handler;

    public AssignRoleHandlerTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _uowMock = new Mock<IUnitOfWork>();
        _handler = new AssignRoleHandler(_userRepoMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldAssignRoleAndReturnTrue()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User("testuser", new Email("test@example.com"));
        _userRepoMock.Setup(x => x.GetAsync(userId, default)).ReturnsAsync(user);
        _userRepoMock.Setup(x => x.UpdateAsync(user, default)).Returns(Task.CompletedTask);
        _uowMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
        var command = new AssignRoleCommand(userId, Role.Admin);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().BeTrue();
        _userRepoMock.Verify(x => x.UpdateAsync(It.Is<User>(u => u.Role == Role.Admin), default), Times.Once);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task Handle_UserNotFound_ShouldThrowException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userRepoMock.Setup(x => x.GetAsync(userId, default)).ReturnsAsync((User)null);
        var command = new AssignRoleCommand(userId, Role.Admin);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, default);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("User not found");
    }
}