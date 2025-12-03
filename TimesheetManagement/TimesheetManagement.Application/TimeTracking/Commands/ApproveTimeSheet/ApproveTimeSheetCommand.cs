using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.TimeTracking.Commands.ApproveTimeSheet;
public sealed record ApproveTimeSheetCommand(Guid TimeSheetId, Guid AdminId, string? Comment) : ICommand<bool>;
