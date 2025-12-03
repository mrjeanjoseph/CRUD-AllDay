using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimesheetManagement.Domain.Audit;
using System;

namespace TimesheetManagement.Infrastructure.Persistence.Configurations;

public class AuditLogConfig : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> b)
    {
        b.ToTable("AuditLogs");
        b.HasKey(x => x.Id);
        b.Property(x => x.Action).HasMaxLength(128).IsRequired();
        b.Property(x => x.EntityType).HasMaxLength(128).IsRequired();
        b.Property(x => x.EntityId).IsRequired();
        b.Property(x => x.UserId).IsRequired();
        b.Property(x => x.Details).HasMaxLength(1024);
        b.Property(x => x.Timestamp).IsRequired();
    }
}
