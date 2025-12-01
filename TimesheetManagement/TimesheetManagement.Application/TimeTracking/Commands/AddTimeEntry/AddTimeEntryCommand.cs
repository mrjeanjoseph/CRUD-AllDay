using System;
using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.TimeTracking.Commands.AddTimeEntry;
public sealed record AddTimeEntryCommand(Guid TimeSheetId, Guid ProjectId, DateOnly Date, decimal Hours, string? Notes) : ICommand<bool>;
