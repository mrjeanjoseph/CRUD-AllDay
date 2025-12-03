using System;
using System.Collections.Generic;
using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.TimeTracking.Queries.GetTimeSheetsForUser;
public sealed record GetTimeSheetsForUserQuery(Guid UserId, DateOnly? From = null, DateOnly? To = null) : IQuery<IReadOnlyList<TimeSheetSummaryDto>>;

public sealed record TimeSheetSummaryDto(Guid Id, DateOnly From, DateOnly To, string Status, int EntryCount);
