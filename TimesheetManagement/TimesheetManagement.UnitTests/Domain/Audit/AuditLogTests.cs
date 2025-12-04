using FluentAssertions;
using System;
using TimesheetManagement.Domain.Audit;
using Xunit;

namespace TimesheetManagement.UnitTests.Domain.Audit;

public class AuditLogTests
{
    [Fact]
    public void Constructor_ShouldSetProperties()
    {
        // Arrange
        var action = "Create";
        var entityType = "User";
        var entityId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var details = "User created";

        // Act
        var auditLog = new AuditLog(action, entityType, entityId, userId, details);

        // Assert
        auditLog.Action.Should().Be(action);
        auditLog.EntityType.Should().Be(entityType);
        auditLog.EntityId.Should().Be(entityId);
        auditLog.UserId.Should().Be(userId);
        auditLog.Details.Should().Be(details);
        auditLog.Timestamp.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}