using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Identity.Commands.AssignRole;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.Domain.Identity.Repositories;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Identity.Commands.AssignRole;

public class AssignRoleValidatorTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<IUserContext> _userContextMock;
    private readonly AssignRoleValidator _validator;

    public AssignRoleValidatorTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _userContextMock = new Mock<IUserContext>();
        _validator = new AssignRoleValidator(_userRepoMock.Object, _userContextMock.Object);
    }

    [Fact]
    public async Task Validate_ValidCommand_ShouldPass()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User("testuser", new Email("test@example.com"));
        _userRepoMock.Setup(x => x.GetAsync(userId, default)).ReturnsAsync(user);
        _userContextMock.Setup(x => x.IsInRole("Admin")).Returns(true);
        var command = new AssignRoleCommand(userId, Role.Admin);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_InvalidRole_ShouldFail()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new AssignRoleCommand(userId, (Role)999); // Invalid

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Role");
    }

    [Fact]
    public async Task Validate_NoPermission_ShouldFail()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userContextMock.Setup(x => x.IsInRole("Admin")).Returns(false);
        _userContextMock.Setup(x => x.IsInRole("SuperAdmin")).Returns(false);
        var command = new AssignRoleCommand(userId, Role.Admin);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("Only Admin or SuperAdmin"));
    }

    [Fact]
    public async Task Validate_UserNotFound_ShouldFail()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userRepoMock.Setup(x => x.GetAsync(userId, default)).ReturnsAsync((User)null);
        _userContextMock.Setup(x => x.IsInRole("Admin")).Returns(true);
        var command = new AssignRoleCommand(userId, Role.Admin);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("User not found"));
    }
}