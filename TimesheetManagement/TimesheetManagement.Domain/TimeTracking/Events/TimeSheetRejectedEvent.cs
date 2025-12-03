using TimesheetManagement.Domain.Common;

namespace TimesheetManagement.Domain.TimeTracking.Events;
public sealed record TimeSheetRejectedEvent(Guid TimeSheetId, Guid UserId, string Comment, DateTime OccurredOn) : IDomainEvent;
