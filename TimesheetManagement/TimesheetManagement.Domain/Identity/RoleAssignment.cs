namespace TimesheetManagement.Domain.Identity;

using System;
using System.Collections.Generic;
using TimesheetManagement.Domain.Common;
using TimesheetManagement.Domain.Identity.Events;

public enum RoleAssignmentStatus
{
    Active = 0,
    Inactive = 1
}

// Aggregate root representing the assignment of a User to an Admin (manager)
public class RoleAssignment : Entity
{
    public Guid AdminId { get; private set; }
    public Guid UserId { get; private set; }
    public RoleAssignmentStatus Status { get; private set; } = RoleAssignmentStatus.Active;
    public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;
    public Guid CreatedBy { get; private set; }

    private readonly List<object> _domainEvents = new();
    public IReadOnlyList<object> DomainEvents => _domainEvents;

    private RoleAssignment() { }

    private RoleAssignment(Guid adminId, Guid userId, Guid createdBy)
    {
        if (adminId == Guid.Empty) throw new ArgumentException("Admin required", nameof(adminId));
        if (userId == Guid.Empty) throw new ArgumentException("User required", nameof(userId));
        if (createdBy == Guid.Empty) throw new ArgumentException("CreatedBy required", nameof(createdBy));
        if (adminId == userId) throw new InvalidOperationException("User cannot assign themselves as admin");
        AdminId = adminId;
        UserId = userId;
        CreatedBy = createdBy;
        _domainEvents.Add(new RoleAssignedEvent(Id, AdminId, UserId, DateTime.UtcNow));
    }

    public static RoleAssignment Create(Guid adminId, Guid userId, Guid createdBy)
        => new(adminId, userId, createdBy);

    public void Deactivate()
    {
        if (Status == RoleAssignmentStatus.Inactive) return;
        Status = RoleAssignmentStatus.Inactive;
        _domainEvents.Add(new RoleAssignmentDeactivatedEvent(Id, DateTime.UtcNow));
    }

    public void Activate()
    {
        if (Status == RoleAssignmentStatus.Active) return;
        Status = RoleAssignmentStatus.Active;
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}
