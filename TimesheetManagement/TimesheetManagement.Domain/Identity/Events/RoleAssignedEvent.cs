using TimesheetManagement.Domain.Common;

namespace TimesheetManagement.Domain.Identity.Events;
public sealed record RoleAssignedEvent(Guid RoleAssignmentId, Guid AdminId, Guid UserId, DateTime OccurredOn) : IDomainEvent;
