using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.TimeTracking.Commands.RejectTimeSheet;
public sealed record RejectTimeSheetCommand(Guid TimeSheetId, Guid AdminId, string Comment) : ICommand<bool>;
