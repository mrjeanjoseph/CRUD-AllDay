using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Audit;
using TimesheetManagement.Infrastructure.Persistence;

namespace TimesheetManagement.Infrastructure.Repositories;
public sealed class AuditLogRepository : IAuditLogRepository
{
    private readonly AppDbContext _db;

    public AuditLogRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(AuditLogEntry entry, CancellationToken cancellationToken = default)
    {
        var auditLog = new AuditLog(entry.Action, entry.EntityType, entry.EntityId, entry.UserId, entry.Details ?? "");
        await _db.AuditLogs.AddAsync(auditLog, cancellationToken);
    }
}