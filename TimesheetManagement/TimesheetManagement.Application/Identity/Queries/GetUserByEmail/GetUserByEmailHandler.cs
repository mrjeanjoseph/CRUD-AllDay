using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.Domain.Identity.Repositories;

namespace TimesheetManagement.Application.Identity.Queries.GetUserByEmail;

public sealed class GetUserByEmailHandler : IQueryHandler<GetUserByEmailQuery, UserDto>
{
    private readonly IUserRepository _users;

    public GetUserByEmailHandler(IUserRepository users)
    {
        _users = users;
    }

    public async Task<UserDto> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        var email = new Email(query.Email);
        var user = await _users.GetByEmailAsync(email, cancellationToken);
        if (user is null) throw new KeyNotFoundException("User not found");
        return new UserDto(user.Id, user.Username, user.Email.Value, user.Role.ToString());
    }
}
