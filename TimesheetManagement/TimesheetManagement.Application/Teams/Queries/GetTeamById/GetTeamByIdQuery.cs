using System;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Teams.Queries.GetAllTeams;

namespace TimesheetManagement.Application.Teams.Queries.GetTeamById;
public sealed record GetTeamByIdQuery(Guid TeamId) : IQuery<TeamDto>;
