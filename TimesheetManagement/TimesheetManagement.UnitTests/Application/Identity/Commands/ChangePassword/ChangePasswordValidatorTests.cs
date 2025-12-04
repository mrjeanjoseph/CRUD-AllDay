using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Identity.Commands.ChangePassword;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.Domain.Identity.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Identity.Commands.ChangePassword;

public class ChangePasswordValidatorTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<IUserContext> _userContextMock;
    private readonly ChangePasswordValidator _validator;

    public ChangePasswordValidatorTests()
    {
        _userRepoMock = ApplicationTestHelpers.CreateUserRepository();
        _userContextMock = ApplicationTestHelpers.CreateUserContext();
        _validator = new ChangePasswordValidator(_userRepoMock.Object, _userContextMock.Object);
    }

    [Fact]
    public async Task Validate_ValidCommand_OwnPassword_ShouldPass()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = TestData.CreateSampleUser(userId);
        _userRepoMock.Setup(x => x.GetAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(user);
        _userContextMock.Setup(x => x.UserId).Returns(userId);
        var command = new ChangePasswordCommand(userId, "newhashedpassword123456789012345678901234567890123456789012345678901234567890");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_ValidCommand_Admin_ShouldPass()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = TestData.CreateSampleUser(userId);
        _userRepoMock.Setup(x => x.GetAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(user);
        _userContextMock.Setup(x => x.UserId).Returns(Guid.NewGuid()); // Different user
        _userContextMock.Setup(x => x.IsInRole("Admin")).Returns(true);
        var command = new ChangePasswordCommand(userId, "newhashedpassword123456789012345678901234567890123456789012345678901234567890");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_ShortPassword_ShouldFail()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new ChangePasswordCommand(userId, "short");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "NewPasswordHash");
    }

    [Fact]
    public async Task Validate_NoPermission_ShouldFail()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userContextMock.Setup(x => x.UserId).Returns(Guid.NewGuid()); // Different user
        _userContextMock.Setup(x => x.IsInRole("Admin")).Returns(false);
        _userContextMock.Setup(x => x.IsInRole("SuperAdmin")).Returns(false);
        var command = new ChangePasswordCommand(userId, "newhashedpassword123456789");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("Not authorized"));
    }

    [Fact]
    public async Task Validate_UserNotFound_ShouldFail()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userRepoMock.Setup(x => x.GetAsync(userId, It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync((User)null);
        _userContextMock.Setup(x => x.UserId).Returns(userId);
        var command = new ChangePasswordCommand(userId, "newhashedpassword123456789");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("User not found"));
    }
}