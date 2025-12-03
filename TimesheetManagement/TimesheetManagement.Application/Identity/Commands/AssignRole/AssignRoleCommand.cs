using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Identity;

namespace TimesheetManagement.Application.Identity.Commands.AssignRole;
public sealed record AssignRoleCommand(Guid UserId, Role Role) : ICommand<bool>;
