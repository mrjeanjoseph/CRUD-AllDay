using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Teams.Repositories;

namespace TimesheetManagement.Application.Teams.Commands.AddTeamMember;
public sealed class AddTeamMemberHandler : ICommandHandler<AddTeamMemberCommand, bool>
{
    private readonly ITeamRepository _repo;
    private readonly IUnitOfWork _uow;

    public AddTeamMemberHandler(ITeamRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<bool> Handle(AddTeamMemberCommand command, CancellationToken cancellationToken)
    {
        var team = await _repo.GetAsync(command.TeamId, cancellationToken);
        if (team is null) throw new KeyNotFoundException("Team not found");
        team.AddMember(command.UserId);
        await _repo.UpdateAsync(team, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
