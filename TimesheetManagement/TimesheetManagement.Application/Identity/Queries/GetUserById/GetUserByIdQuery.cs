using System;
using TimesheetManagement.Application.Common.Abstractions;
namespace TimesheetManagement.Application.Identity.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserDto>;

public sealed record UserDto(Guid Id, string Username, string Email, string Role);
