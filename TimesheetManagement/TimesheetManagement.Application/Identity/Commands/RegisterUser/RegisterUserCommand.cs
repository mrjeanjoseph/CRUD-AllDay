using System;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Identity;

namespace TimesheetManagement.Application.Identity.Commands.RegisterUser;
public sealed record RegisterUserCommand(string Username, string Email, string PasswordHash, Role Role) : ICommand<Guid>;
