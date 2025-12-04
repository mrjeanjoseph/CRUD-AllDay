using FluentAssertions;
using System;
using TimesheetManagement.Domain.Identity;
using Xunit;

namespace TimesheetManagement.Tests.Domain.Identity.ValueObjects;

public class EmailTests
{
    [Fact]
    public void Constructor_ValidEmail_ShouldCreate()
    {
        // Arrange
        var emailStr = "test@example.com";

        // Act
        var email = new Email(emailStr);

        // Assert
        email.Value.Should().Be(emailStr);
    }

    [Fact]
    public void Constructor_NullOrEmpty_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Email(null));
        Assert.Throws<ArgumentException>(() => new Email(""));
        Assert.Throws<ArgumentException>(() => new Email("   "));
    }

    [Fact]
    public void Constructor_InvalidEmail_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Email("invalidemail"));
    }

    [Fact]
    public void ToString_ShouldReturnValue()
    {
        // Arrange
        var emailStr = "test@example.com";
        var email = new Email(emailStr);

        // Act & Assert
        email.ToString().Should().Be(emailStr);
    }
}