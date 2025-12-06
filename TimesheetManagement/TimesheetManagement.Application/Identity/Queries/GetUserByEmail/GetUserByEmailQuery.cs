using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Identity.Shared;

namespace TimesheetManagement.Application.Identity.Queries.GetUserByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserDto>;
