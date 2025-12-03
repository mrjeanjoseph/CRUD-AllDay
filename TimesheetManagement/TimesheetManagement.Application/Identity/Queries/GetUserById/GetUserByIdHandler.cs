using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Identity.Repositories;

namespace TimesheetManagement.Application.Identity.Queries.GetUserById;
public sealed class GetUserByIdHandler : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _users;

    public GetUserByIdHandler(IUserRepository users)
    {
        _users = users;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _users.GetAsync(query.UserId, cancellationToken);
        if (user is null) throw new KeyNotFoundException("User not found");
        return new UserDto(user.Id, user.Username, user.Email.Value, user.Role.ToString());
    }
}
