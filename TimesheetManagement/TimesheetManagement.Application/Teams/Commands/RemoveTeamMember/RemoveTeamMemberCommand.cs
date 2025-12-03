using System;
using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Teams.Commands.RemoveTeamMember;
public sealed record RemoveTeamMemberCommand(Guid TeamId, Guid UserId) : ICommand<bool>;
