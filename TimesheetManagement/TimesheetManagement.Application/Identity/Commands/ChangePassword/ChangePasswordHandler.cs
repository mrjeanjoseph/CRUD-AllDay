using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.Domain.Identity.Repositories;

namespace TimesheetManagement.Application.Identity.Commands.ChangePassword;
public sealed class ChangePasswordHandler : ICommandHandler<ChangePasswordCommand, bool>
{
    private readonly IUserRepository _users;
    private readonly IUnitOfWork _uow;

    public ChangePasswordHandler(IUserRepository users, IUnitOfWork uow)
    {
        _users = users;
        _uow = uow;
    }

    public async Task<bool> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _users.GetAsync(command.UserId, cancellationToken);
        if (user is null) throw new KeyNotFoundException("User not found");

        user.ChangePassword(new PasswordHash(command.NewPasswordHash));
        await _users.UpdateAsync(user, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
