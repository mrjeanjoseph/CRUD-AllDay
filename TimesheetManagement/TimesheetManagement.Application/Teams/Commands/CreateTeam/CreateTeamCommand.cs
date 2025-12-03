using System;
using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Teams.Commands.CreateTeam;
public sealed record CreateTeamCommand(string Name) : ICommand<Guid>;
