using System.Diagnostics.CodeAnalysis;
using TimesheetManagement.Domain.Common;

namespace TimesheetManagement.Domain.Identity;
public class User : Entity
{
    public required string Username { get; init; }
    public required Email Email { get; init; }
    public PasswordHash PasswordHash { get; private set; } = PasswordHash.Empty;
    public Role Role { get; private set; } = Role.User;

    private User() { }

    [SetsRequiredMembers]
    public User(string username, Email email)
    {
        if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username is required", nameof(username));
        Username = username.Trim();
        Email = email;
    }

    public void ChangePassword(PasswordHash newHash)
    {
        PasswordHash = newHash;
    }

    public void AssignRole(Role role)
    {
        Role = role;
    }
}
