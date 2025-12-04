using FluentAssertions;
using System;
using TimesheetManagement.Domain.Teams;
using Xunit;

namespace TimesheetManagement.Tests.Domain.Teams;

public class TeamTests
{
    [Fact]
    public void Constructor_ValidName_ShouldCreate()
    {
        // Arrange
        var name = "Test Team";

        // Act
        var team = new Team(name);

        // Assert
        team.Name.Should().Be(name);
        team.IsArchived.Should().BeFalse();
        team.Members.Should().BeEmpty();
    }

    [Fact]
    public void Constructor_InvalidName_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Team(""));
    }

    [Fact]
    public void Rename_ValidName_ShouldUpdate()
    {
        // Arrange
        var team = new Team("Old Name");

        // Act
        team.Rename("New Name");

        // Assert
        team.Name.Should().Be("New Name");
    }

    [Fact]
    public void AddMember_ValidUserId_ShouldAdd()
    {
        // Arrange
        var team = new Team("Test Team");
        var userId = Guid.NewGuid();

        // Act
        team.AddMember(userId);

        // Assert
        team.Members.Should().ContainSingle(m => m.UserId == userId);
    }

    [Fact]
    public void AddMember_DuplicateUserId_ShouldNotAdd()
    {
        // Arrange
        var team = new Team("Test Team");
        var userId = Guid.NewGuid();
        team.AddMember(userId);

        // Act
        team.AddMember(userId);

        // Assert
        team.Members.Should().ContainSingle(m => m.UserId == userId);
    }

    [Fact]
    public void RemoveMember_ExistingUserId_ShouldRemove()
    {
        // Arrange
        var team = new Team("Test Team");
        var userId = Guid.NewGuid();
        team.AddMember(userId);

        // Act
        team.RemoveMember(userId);

        // Assert
        team.Members.Should().BeEmpty();
    }

    [Fact]
    public void Archive_ShouldSetArchived()
    {
        // Arrange
        var team = new Team("Test Team");

        // Act
        team.Archive();

        // Assert
        team.IsArchived.Should().BeTrue();
    }

    [Fact]
    public void AddMember_WhenArchived_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var team = new Team("Test Team");
        team.Archive();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => team.AddMember(Guid.NewGuid()));
    }
}