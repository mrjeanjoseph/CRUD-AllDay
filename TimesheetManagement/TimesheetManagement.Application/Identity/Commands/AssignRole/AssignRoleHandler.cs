using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Identity.Repositories;

namespace TimesheetManagement.Application.Identity.Commands.AssignRole;
public sealed class AssignRoleHandler : ICommandHandler<AssignRoleCommand, bool>
{
    private readonly IUserRepository _users;
    private readonly IUnitOfWork _uow;

    public AssignRoleHandler(IUserRepository users, IUnitOfWork uow)
    {
        _users = users;
        _uow = uow;
    }

    public async Task<bool> Handle(AssignRoleCommand command, CancellationToken cancellationToken)
    {
        var user = await _users.GetAsync(command.UserId, cancellationToken);
        if (user is null) throw new KeyNotFoundException("User not found");

        user.AssignRole(command.Role);
        await _users.UpdateAsync(user, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
