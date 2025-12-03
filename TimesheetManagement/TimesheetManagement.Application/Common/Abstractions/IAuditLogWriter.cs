namespace TimesheetManagement.Application.Common.Abstractions;

public interface IAuditLogWriter
{
    Task WriteAsync(string action, string entityType, Guid entityId, Guid userId, string details, CancellationToken cancellationToken = default);
}

public interface INotificationSender
{
    Task SendToUserAsync(Guid userId, string message, CancellationToken cancellationToken = default);
    Task SendToRoleAsync(string role, string message, CancellationToken cancellationToken = default);
}
