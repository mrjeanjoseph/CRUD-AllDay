using FluentAssertions;
using System;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.Domain.Identity.Events;
using Xunit;

namespace TimesheetManagement.UnitTests.Domain.Identity;

public class RoleAssignmentTests
{
    [Fact]
    public void Constructor_ValidParameters_ShouldCreate()
    {
        // Arrange
        var adminId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var createdBy = Guid.NewGuid();

        // Act
        var assignment = RoleAssignment.Create(adminId, userId, createdBy);

        // Assert
        assignment.AdminId.Should().Be(adminId);
        assignment.UserId.Should().Be(userId);
        assignment.CreatedBy.Should().Be(createdBy);
        assignment.Status.Should().Be(RoleAssignmentStatus.Active);
        assignment.DomainEvents.Should().ContainSingle(e => e is RoleAssignedEvent);
    }

    [Fact]
    public void Constructor_SameAdminAndUser_ShouldThrowArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => RoleAssignment.Create(id, id, Guid.NewGuid()));
    }

    [Fact]
    public void Activate_FromInactive_ShouldChangeStatus()
    {
        // Arrange
        var assignment = RoleAssignment.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        assignment.Deactivate();
        assignment.ClearDomainEvents();

        // Act
        assignment.Activate();

        // Assert
        assignment.Status.Should().Be(RoleAssignmentStatus.Active);
        assignment.DomainEvents.Should().BeEmpty(); // Assuming no event for activate
    }

    [Fact]
    public void Deactivate_FromActive_ShouldChangeStatusAndRaiseEvent()
    {
        // Arrange
        var assignment = RoleAssignment.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        assignment.ClearDomainEvents();

        // Act
        assignment.Deactivate();

        // Assert
        assignment.Status.Should().Be(RoleAssignmentStatus.Inactive);
        assignment.DomainEvents.Should().ContainSingle(e => e is RoleAssignmentDeactivatedEvent);
    }
}