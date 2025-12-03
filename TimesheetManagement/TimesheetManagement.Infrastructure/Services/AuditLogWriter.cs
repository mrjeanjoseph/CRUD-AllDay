using System;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Audit;
using TimesheetManagement.Infrastructure.Persistence;

namespace TimesheetManagement.Infrastructure.Services;

public class AuditLogWriter : IAuditLogWriter
{
    private readonly AppDbContext _db;

    public AuditLogWriter(AppDbContext db)
    {
        _db = db;
    }

    public async Task WriteAsync(string action, string entityType, Guid entityId, Guid userId, string details, CancellationToken cancellationToken = default)
    {
        var log = new AuditLog(action, entityType, entityId, userId, details);
        await _db.AuditLogs.AddAsync(log, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
