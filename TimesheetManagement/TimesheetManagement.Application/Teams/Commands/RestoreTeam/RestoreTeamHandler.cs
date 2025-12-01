using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Teams.Repositories;

namespace TimesheetManagement.Application.Teams.Commands.RestoreTeam;
public sealed class RestoreTeamHandler : ICommandHandler<RestoreTeamCommand, bool>
{
    private readonly ITeamRepository _repo;
    private readonly IUnitOfWork _uow;

    public RestoreTeamHandler(ITeamRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<bool> Handle(RestoreTeamCommand command, CancellationToken cancellationToken)
    {
        var team = await _repo.GetAsync(command.TeamId, cancellationToken);
        if (team is null) throw new KeyNotFoundException("Team not found");
        team.Restore();
        await _repo.UpdateAsync(team, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
