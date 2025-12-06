namespace TimesheetManagement.Application.Identity.Shared;

/// <summary>
/// Shared User data transfer object for Identity operations
/// </summary>
public sealed record UserDto(
    Guid Id, 
    string Username, 
    string Email, 
    string Role);