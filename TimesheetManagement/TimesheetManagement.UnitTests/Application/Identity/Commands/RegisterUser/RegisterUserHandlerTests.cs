using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Identity.Commands.RegisterUser;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.Domain.Identity.Repositories;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Identity.Commands.RegisterUser;

public class RegisterUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly RegisterUserHandler _handler;

    public RegisterUserHandlerTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _uowMock = new Mock<IUnitOfWork>();
        _handler = new RegisterUserHandler(_userRepoMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateUserAndReturnId()
    {
        // Arrange
        var command = new RegisterUserCommand("testuser", "test@example.com", "hashedpassword", Role.User);
        _userRepoMock.Setup(x => x.ExistsAsync(It.IsAny<Email>(), default)).ReturnsAsync(false);
        _userRepoMock.Setup(x => x.AddAsync(It.IsAny<User>(), default)).Returns(Task.CompletedTask);
        _uowMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().NotBeEmpty();
        _userRepoMock.Verify(x => x.AddAsync(It.Is<User>(u => u.Username == "testuser" && u.Email.Value == "test@example.com"), default), Times.Once);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task Handle_EmailAlreadyExists_ShouldThrowException()
    {
        // Arrange
        var command = new RegisterUserCommand("testuser", "test@example.com", "hashedpassword", Role.User);
        _userRepoMock.Setup(x => x.ExistsAsync(It.IsAny<Email>(), default)).ReturnsAsync(true);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, default);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Email already registered");
    }
}