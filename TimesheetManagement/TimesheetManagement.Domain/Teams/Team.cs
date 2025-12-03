using TimesheetManagement.Domain.Common;

namespace TimesheetManagement.Domain.Teams;
public class Team : Entity
{
    public string Name { get; private set; }
    public bool IsArchived { get; private set; }
    private readonly List<TeamMember> _members = new();
    public IReadOnlyList<TeamMember> Members => _members;

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
        if (_members.Any(m => m.UserId == userId)) return;
        _members.Add(new TeamMember(Id, userId));
    }

    public void RemoveMember(Guid userId)
    {
        if (IsArchived) throw new InvalidOperationException("Cannot modify archived team");
        _members.RemoveAll(m => m.UserId == userId);
    }

    public void Archive() => IsArchived = true;
    public void Restore() => IsArchived = false;
}
