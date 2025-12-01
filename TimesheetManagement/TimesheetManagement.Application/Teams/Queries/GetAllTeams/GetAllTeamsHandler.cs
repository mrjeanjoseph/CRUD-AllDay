using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Teams.Repositories;
namespace TimesheetManagement.Application.Teams.Queries.GetAllTeams;


public sealed class GetAllTeamsHandler : IQueryHandler<GetAllTeamsQuery, IReadOnlyList<TeamDto>>
{
    private readonly ITeamRepository _repo;

    public GetAllTeamsHandler(ITeamRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<TeamDto>> Handle(GetAllTeamsQuery query, CancellationToken cancellationToken)
    {
        var teams = await _repo.GetAllAsync(cancellationToken);
        return teams.Select(t => new TeamDto(t.Id, t.Name, t.IsArchived, t.MemberIds)).ToList();
    }
}
