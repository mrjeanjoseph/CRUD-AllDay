using System;
using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Teams.Commands.AddTeamMember;
public sealed record AddTeamMemberCommand(Guid TeamId, Guid UserId) : ICommand<bool>;
