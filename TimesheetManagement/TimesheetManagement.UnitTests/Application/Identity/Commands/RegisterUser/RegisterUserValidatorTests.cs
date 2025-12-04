using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Identity.Commands.RegisterUser;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.Domain.Identity.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Identity.Commands.RegisterUser;

public class RegisterUserValidatorTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly RegisterUserValidator _validator;

    public RegisterUserValidatorTests()
    {
        _userRepoMock = ApplicationTestHelpers.CreateUserRepository();
        _validator = new RegisterUserValidator(_userRepoMock.Object);
    }

    [Fact]
    public async Task Validate_ValidCommand_ShouldPass()
    {
        // Arrange
        var command = new RegisterUserCommand("testuser", "test@example.com", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", Role.User);
        _userRepoMock.Setup(x => x.ExistsAsync(It.Is<Email>(e => e.Value == "test@example.com"), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyUsername_ShouldFail()
    {
        // Arrange
        var command = new RegisterUserCommand("", "test@example.com", "hashedpassword", Role.User);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Username");
    }

    [Fact]
    public async Task Validate_InvalidEmail_ShouldFail()
    {
        // Arrange
        var command = new RegisterUserCommand("testuser", "invalidemail", "hashedpassword", Role.User);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Email");
    }

    [Fact]
    public async Task Validate_EmailAlreadyExists_ShouldFail()
    {
        // Arrange
        var command = new RegisterUserCommand("testuser", "test@example.com", "hashedpassword", Role.User);
        _userRepoMock.Setup(x => x.ExistsAsync(It.Is<Email>(e => e.Value == "test@example.com"), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("Email already registered"));
    }

    [Fact]
    public async Task Validate_ShortPasswordHash_ShouldFail()
    {
        // Arrange
        var command = new RegisterUserCommand("testuser", "test@example.com", "short", Role.User);
        _userRepoMock.Setup(x => x.ExistsAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "PasswordHash");
    }
}