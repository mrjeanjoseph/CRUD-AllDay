using System;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Teams;
using TimesheetManagement.Domain.Teams.Repositories;

namespace TimesheetManagement.Application.Teams.Commands.CreateTeam;
public sealed class CreateTeamHandler : ICommandHandler<CreateTeamCommand, Guid>
{
    private readonly ITeamRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateTeamHandler(ITeamRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateTeamCommand command, CancellationToken cancellationToken)
    {
        if (await _repo.NameExistsAsync(command.Name, cancellationToken)) throw new InvalidOperationException("Team name already exists");
        var team = new Team(command.Name);
        await _repo.AddAsync(team, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return team.Id;
    }
}
