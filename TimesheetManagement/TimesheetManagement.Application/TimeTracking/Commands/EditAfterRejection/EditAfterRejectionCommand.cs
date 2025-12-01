using System;
using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.TimeTracking.Commands.EditAfterRejection;
public sealed record EditAfterRejectionCommand(Guid TimeSheetId) : ICommand<bool>;
