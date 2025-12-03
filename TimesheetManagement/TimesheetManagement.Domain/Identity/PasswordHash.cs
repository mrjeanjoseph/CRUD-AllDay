namespace TimesheetManagement.Domain.Identity;

public readonly record struct PasswordHash(string Value)
{
    public static PasswordHash Empty => new("");
}
