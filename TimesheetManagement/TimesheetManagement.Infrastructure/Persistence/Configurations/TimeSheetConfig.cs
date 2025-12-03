using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimesheetManagement.Domain.TimeTracking;

namespace TimesheetManagement.Infrastructure.Persistence.Configurations;

public class TimeSheetConfig : IEntityTypeConfiguration<TimeSheet>
{
    public void Configure(EntityTypeBuilder<TimeSheet> b)
    {
        b.ToTable("TimeSheets");
        b.HasKey(x => x.Id);
        b.Property(x => x.UserId).IsRequired();
        b.Property(x => x.Status).HasConversion<int>();
        b.Property(x => x.Comment).HasMaxLength(1024);

        b.ComplexProperty(x => x.Period, nb =>
        {
            nb.Property(p => p.From).HasColumnName("FromDate").HasColumnType("date");
            nb.Property(p => p.To).HasColumnName("ToDate").HasColumnType("date");
        });

        // Define shadow properties for indexing
        b.Property<DateOnly>("FromDate").HasColumnType("date");
        b.Property<DateOnly>("ToDate").HasColumnType("date");

        // Add composite index for overlap queries
        b.HasIndex("UserId", "FromDate", "ToDate");

        var entriesNav = b.Navigation(x => x.Entries);
        entriesNav.HasField("_entries");
        entriesNav.UsePropertyAccessMode(PropertyAccessMode.Field);

        b.HasMany(x => x.Entries).WithOne().HasForeignKey("TimeSheetId").OnDelete(DeleteBehavior.Cascade);
    }
}
