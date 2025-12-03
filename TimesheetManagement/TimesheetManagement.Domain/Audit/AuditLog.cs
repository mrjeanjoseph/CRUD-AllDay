using TimesheetManagement.Domain.Common;

namespace TimesheetManagement.Domain.Audit;

public class AuditLog : Entity
{
    public string Action { get; private set; }
    public string EntityType { get; private set; }
    public Guid EntityId { get; private set; }
    public Guid UserId { get; private set; }
    public string Details { get; private set; }
    public DateTime Timestamp { get; private set; }

    private AuditLog() { }

    public AuditLog(string action, string entityType, Guid entityId, Guid userId, string details)
    {
        Action = action;
        EntityType = entityType;
        EntityId = entityId;
        UserId = userId;
        Details = details;
        Timestamp = DateTime.UtcNow;
    }
}
