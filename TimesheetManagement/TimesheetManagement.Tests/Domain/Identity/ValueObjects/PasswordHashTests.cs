using FluentAssertions;
using TimesheetManagement.Domain.Identity;
using Xunit;

namespace TimesheetManagement.Tests.Domain.Identity.ValueObjects;

public class PasswordHashTests
{
    [Fact]
    public void Constructor_ShouldSetValue()
    {
        // Arrange
        var hash = "hashedpassword";

        // Act
        var passwordHash = new PasswordHash(hash);

        // Assert
        passwordHash.Value.Should().Be(hash);
    }

    [Fact]
    public void Empty_ShouldReturnEmptyString()
    {
        // Act & Assert
        PasswordHash.Empty.Value.Should().Be("");
    }
}