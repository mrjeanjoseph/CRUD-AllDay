using System;

namespace TimesheetManagement.Domain.Identity;
public readonly record struct Email
{
    public string Value { get; }
    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Email is required", nameof(value));
        if (!value.Contains('@')) throw new ArgumentException("Invalid email", nameof(value));
        Value = value.Trim();
    }
    public override string ToString() => Value;
}
