using TimesheetManagement.Domain.Common;

namespace TimesheetManagement.Domain.Identity.Events;
public sealed record RoleAssignmentDeactivatedEvent(Guid RoleAssignmentId, DateTime OccurredOn) : IDomainEvent;
