namespace TimesheetManagement.Application.Common.Abstractions;
public interface IUserContext
{
    Guid UserId { get; }
    string Username { get; }
    string Role { get; }
    bool IsInRole(string role);
}
