using System;
using TimesheetManagement.Domain.Common;

namespace TimesheetManagement.Domain.TimeTracking.Events;
public sealed record TimeSheetApprovedEvent(Guid TimeSheetId, Guid UserId, string? Comment, DateTime OccurredOn) : IDomainEvent;
