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

        // Index on Status for pending approvals
        b.HasIndex(x => x.Status);

        // Concurrency token
        b.Property<byte[]>("RowVersion").IsRowVersion();

        var entriesNav = b.Navigation(x => x.Entries);
        entriesNav.HasField("_entries");
        entriesNav.UsePropertyAccessMode(PropertyAccessMode.Field);

        b.HasMany(x => x.Entries).WithOne().HasForeignKey("TimeSheetId").OnDelete(DeleteBehavior.Cascade);
    }
}
