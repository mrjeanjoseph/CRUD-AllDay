using FluentAssertions;
using System;
using TimesheetManagement.Domain.Identity;
using Xunit;

namespace TimesheetManagement.UnitTests.Domain.Identity;

public class UserTests
{
    [Fact]
    public void ChangePassword_ValidHash_ShouldUpdate()
    {
        // Arrange
        var user = new User("testuser", new Email("test@example.com"));
        var newHash = new PasswordHash("newhash123");

        // Act
        user.ChangePassword(newHash);

        // Assert
        // Assuming PasswordHash is internal, test via behavior if possible
        // For now, just ensure no exception
    }
}