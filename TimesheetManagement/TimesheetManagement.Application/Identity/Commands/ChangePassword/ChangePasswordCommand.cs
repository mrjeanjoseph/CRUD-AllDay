using System;
using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Identity.Commands.ChangePassword;
public sealed record ChangePasswordCommand(Guid UserId, string NewPasswordHash) : ICommand<bool>;
