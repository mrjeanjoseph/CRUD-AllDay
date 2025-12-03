using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.ValueObjects;

namespace TimesheetManagement.Infrastructure.Persistence.Configurations;

public class TimeEntryConfig : IEntityTypeConfiguration<TimeEntry>
{
    public void Configure(EntityTypeBuilder<TimeEntry> b)
    {
        b.ToTable("TimeEntries");
        b.HasKey(x => x.Id);
        b.Property<Guid>("TimeSheetId").IsRequired();
        b.Property(x => x.ProjectId).IsRequired();
        b.Property(x => x.Date).HasColumnType("date");
        b.Property(x => x.Notes).HasMaxLength(1024);
        b.Property(x => x.Hours)
            .HasConversion(v => v.Value, v => new HoursWorked(v))
            .HasColumnType("decimal(5,2)");
    }
}
