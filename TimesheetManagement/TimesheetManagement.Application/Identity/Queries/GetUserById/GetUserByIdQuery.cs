using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Identity.Shared;

namespace TimesheetManagement.Application.Identity.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserDto>;
