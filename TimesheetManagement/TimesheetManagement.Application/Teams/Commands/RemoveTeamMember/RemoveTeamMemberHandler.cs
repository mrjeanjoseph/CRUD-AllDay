using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Teams.Repositories;

namespace TimesheetManagement.Application.Teams.Commands.RemoveTeamMember;
public sealed class RemoveTeamMemberHandler : ICommandHandler<RemoveTeamMemberCommand, bool>
{
    private readonly ITeamRepository _repo;
    private readonly IUnitOfWork _uow;

    public RemoveTeamMemberHandler(ITeamRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<bool> Handle(RemoveTeamMemberCommand command, CancellationToken cancellationToken)
    {
        var team = await _repo.GetAsync(command.TeamId, cancellationToken);
        if (team is null) throw new KeyNotFoundException("Team not found");
        team.RemoveMember(command.UserId);
        await _repo.UpdateAsync(team, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
