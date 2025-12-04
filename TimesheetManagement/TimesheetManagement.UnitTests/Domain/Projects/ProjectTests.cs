using FluentAssertions;
using System;
using TimesheetManagement.Domain.Projects;
using Xunit;

namespace TimesheetManagement.UnitTests.Domain.Projects;

public class ProjectTests
{
    [Fact]
    public void Constructor_ValidParameters_ShouldCreate()
    {
        // Arrange
        var code = "PROJ001";
        var name = "Test Project";
        var industry = "Tech";

        // Act
        var project = new Project(code, name, industry);

        // Assert
        project.Code.Should().Be(code);
        project.Name.Should().Be(name);
        project.Industry.Should().Be(industry);
        project.IsArchived.Should().BeFalse();
    }

    [Fact]
    public void Constructor_InvalidCode_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Project("", "Name", "Industry"));
    }

    [Fact]
    public void Rename_ValidName_ShouldUpdate()
    {
        // Arrange
        var project = new Project("PROJ001", "Old Name", "Tech");

        // Act
        project.Rename("New Name");

        // Assert
        project.Name.Should().Be("New Name");
    }

    [Fact]
    public void Archive_ShouldSetArchived()
    {
        // Arrange
        var project = new Project("PROJ001", "Name", "Tech");

        // Act
        project.Archive();

        // Assert
        project.IsArchived.Should().BeTrue();
    }

    [Fact]
    public void Restore_ShouldSetNotArchived()
    {
        // Arrange
        var project = new Project("PROJ001", "Name", "Tech");
        project.Archive();

        // Act
        project.Restore();

        // Assert
        project.IsArchived.Should().BeFalse();
    }
}