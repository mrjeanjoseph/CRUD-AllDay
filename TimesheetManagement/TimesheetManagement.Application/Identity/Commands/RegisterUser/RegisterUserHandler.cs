using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.Domain.Identity.Repositories;

namespace TimesheetManagement.Application.Identity.Commands.RegisterUser;
public sealed class RegisterUserHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _users;
    private readonly IUnitOfWork _uow;

    public RegisterUserHandler(IUserRepository users, IUnitOfWork uow)
    {
        _users = users;
        _uow = uow;
    }

    public async Task<Guid> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var email = new Email(command.Email);
        var exists = await _users.ExistsAsync(email, cancellationToken);
        if (exists) throw new InvalidOperationException("Email already registered");

        var user = new User(command.Username, email);
        user.AssignRole(command.Role);
        user.ChangePassword(new PasswordHash(command.PasswordHash));

        await _users.AddAsync(user, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}
