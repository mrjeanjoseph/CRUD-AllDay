using System;
using TimesheetManagement.Domain.Common;

namespace TimesheetManagement.Domain.TimeTracking.Events;
public sealed record TimeSheetSubmittedEvent(Guid TimeSheetId, Guid UserId, DateOnly From, DateOnly To, DateTime OccurredOn) : IDomainEvent;
