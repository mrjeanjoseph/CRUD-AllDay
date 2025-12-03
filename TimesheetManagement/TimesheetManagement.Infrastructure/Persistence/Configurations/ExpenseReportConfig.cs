using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimesheetManagement.Domain.Expenses;

namespace TimesheetManagement.Infrastructure.Persistence.Configurations;

public class ExpenseReportConfig : IEntityTypeConfiguration<ExpenseReport>
{
    public void Configure(EntityTypeBuilder<ExpenseReport> b)
    {
        b.ToTable("ExpenseReports");
        b.HasKey(x => x.Id);
        b.Property(x => x.UserId).IsRequired();
        b.Property(x => x.Status).HasConversion<int>();
        b.Property(x => x.Comment).HasMaxLength(1024);

        // Map DateRange using ComplexProperty (record struct)
        b.ComplexProperty(x => x.Period, nb =>
        {
            nb.Property(p => p.From).HasColumnName("FromDate").HasColumnType("date");
            nb.Property(p => p.To).HasColumnName("ToDate").HasColumnType("date");
        });

        // Define shadow properties for indexing
        b.Property<DateOnly>("FromDate").HasColumnType("date");
        b.Property<DateOnly>("ToDate").HasColumnType("date");

        // Index for overlap queries
        b.HasIndex("UserId", "FromDate", "ToDate");

        var itemsNav = b.Navigation(x => x.Items);
        itemsNav.HasField("_items");
        itemsNav.UsePropertyAccessMode(PropertyAccessMode.Field);

        // Map collection using navigation property Items with backing field _items
        b.HasMany(x => x.Items).WithOne().HasForeignKey("ExpenseReportId").OnDelete(DeleteBehavior.Cascade);
    }
}
