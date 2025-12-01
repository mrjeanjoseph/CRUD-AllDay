using System;
using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.TimeTracking.Commands.RemoveTimeEntry;
public sealed record RemoveTimeEntryCommand(Guid TimeSheetId, Guid EntryId) : ICommand<bool>;
