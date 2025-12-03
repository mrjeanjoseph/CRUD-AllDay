using System;
using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.TimeTracking.Commands.CreateTimeSheet;
public sealed record CreateTimeSheetCommand(Guid UserId, DateOnly From, DateOnly To) : ICommand<Guid>;
