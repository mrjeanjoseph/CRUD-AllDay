using System.Threading;
using System.Threading.Tasks;

namespace TimesheetManagement.Application.Common.Abstractions;
public interface IAuditLogRepository
{
    Task AddAsync(AuditLogEntry entry, CancellationToken cancellationToken = default);
}

public record AuditLogEntry(
    Guid Id,
    string Action,
    string EntityType,
    Guid EntityId,
    Guid UserId,
    DateTime Timestamp,
    string? Details);