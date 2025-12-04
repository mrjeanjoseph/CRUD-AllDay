using FluentAssertions;
using TimesheetManagement.Domain.Common;
using Xunit;

namespace TimesheetManagement.UnitTests.Domain.Common;

public class EntityTests
{
    [Fact]
    public void Entity_ShouldHaveUniqueId()
    {
        // Arrange & Act
        var entity1 = new TestEntity();
        var entity2 = new TestEntity();

        // Assert
        entity1.Id.Should().NotBeEmpty();
        entity2.Id.Should().NotBeEmpty();
        entity1.Id.Should().NotBe(entity2.Id);
    }

    // Test entity for base class
    private class TestEntity : Entity { }
}