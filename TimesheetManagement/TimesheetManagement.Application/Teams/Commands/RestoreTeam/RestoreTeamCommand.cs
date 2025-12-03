using System;
using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Teams.Commands.RestoreTeam;
public sealed record RestoreTeamCommand(Guid TeamId) : ICommand<bool>;
