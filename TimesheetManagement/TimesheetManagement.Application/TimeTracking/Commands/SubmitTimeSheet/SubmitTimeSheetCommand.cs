using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.TimeTracking.Commands.SubmitTimeSheet;
public sealed record SubmitTimeSheetCommand(Guid TimeSheetId) : ICommand<bool>;
