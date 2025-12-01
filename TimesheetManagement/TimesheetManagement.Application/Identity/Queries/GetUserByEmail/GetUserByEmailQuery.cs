namespace TimesheetManagement.Application.Identity.Queries.GetUserByEmail;

using TimesheetManagement.Application.Common.Abstractions;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserDto>;

public sealed record UserDto(Guid Id, string Username, string Email, string Role);
