using System;
using System.Collections.Generic;
using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Teams.Queries.GetAllTeams;
public sealed record GetAllTeamsQuery() : IQuery<IReadOnlyList<TeamDto>>;

public sealed record TeamDto(Guid Id, string Name, bool IsArchived, IReadOnlyList<Guid> MemberIds);
