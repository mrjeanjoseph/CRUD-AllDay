using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Teams.Queries.GetAllTeams;
using TimesheetManagement.Domain.Teams.Repositories;

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
        return teams.Select(t => new TeamDto(t.Id, t.Name, t.IsArchived, t.Members.Select(m => m.UserId).ToList())).ToList();
    }
}
