using System;
using TimesheetManagement.Domain.Common;

namespace TimesheetManagement.Domain.Projects;
public class Project : Entity
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Industry { get; private set; }
    public bool IsArchived { get; private set; }
    public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;

    private Project() { }

    public Project(string code, string name, string industry)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Code required", nameof(code));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required", nameof(name));
        if (string.IsNullOrWhiteSpace(industry)) throw new ArgumentException("Industry required", nameof(industry));
        Code = code.Trim();
        Name = name.Trim();
        Industry = industry.Trim();
    }

    public void Rename(string name)
    {
        if (IsArchived) throw new InvalidOperationException("Cannot modify archived project");
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required", nameof(name));
        Name = name.Trim();
    }

    public void Archive()
    {
        if (IsArchived) return;
        IsArchived = true;
    }

    public void Restore()
    {
        if (!IsArchived) return;
        IsArchived = false;
    }
}
