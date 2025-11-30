using System;
using System.Collections.Generic;
using TimesheetManagement.Domain.Common;

namespace TimesheetManagement.Domain.Teams;
public class Team : Entity
{
    public string Name { get; private set; }
    public bool IsArchived { get; private set; }
    private readonly List<Guid> _memberIds = new();
    public IReadOnlyList<Guid> MemberIds => _memberIds;

    private Team() { }

    public Team(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required", nameof(name));
        Name = name.Trim();
    }

    public void Rename(string name)
    {
        if (IsArchived) throw new InvalidOperationException("Cannot modify archived team");
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required", nameof(name));
        Name = name.Trim();
    }

    public void AddMember(Guid userId)
    {
        if (IsArchived) throw new InvalidOperationException("Cannot modify archived team");
        if (userId == Guid.Empty) throw new ArgumentException("UserId required", nameof(userId));
        if (_memberIds.Contains(userId)) return;
        _memberIds.Add(userId);
    }

    public void RemoveMember(Guid userId)
    {
        if (IsArchived) throw new InvalidOperationException("Cannot modify archived team");
        _memberIds.Remove(userId);
    }

    public void Archive() => IsArchived = true;
    public void Restore() => IsArchived = false;
}
