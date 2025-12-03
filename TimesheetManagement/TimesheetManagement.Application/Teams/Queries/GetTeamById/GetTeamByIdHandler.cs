using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Teams.Queries.GetAllTeams;
using TimesheetManagement.Domain.Teams.Repositories;

namespace TimesheetManagement.Application.Teams.Queries.GetTeamById;
public sealed class GetTeamByIdHandler : IQueryHandler<GetTeamByIdQuery, TeamDto>
{
    private readonly ITeamRepository _repo;

    public GetTeamByIdHandler(ITeamRepository repo)
    {
        _repo = repo;
    }

    public async Task<TeamDto> Handle(GetTeamByIdQuery query, CancellationToken cancellationToken)
    {
        var team = await _repo.GetAsync(query.TeamId, cancellationToken);
        if (team is null) throw new KeyNotFoundException("Team not found");
        return new TeamDto(team.Id, team.Name, team.IsArchived, team.Members.Select(m => m.UserId).ToList());
    }
}
