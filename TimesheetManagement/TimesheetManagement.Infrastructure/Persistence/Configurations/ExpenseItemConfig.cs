using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Expenses.ValueObjects;

namespace TimesheetManagement.Infrastructure.Persistence.Configurations;

public class ExpenseItemConfig : IEntityTypeConfiguration<ExpenseItem>
{
    public void Configure(EntityTypeBuilder<ExpenseItem> b)
    {
        b.ToTable("ExpenseItems");
        b.HasKey(x => x.Id);
        b.Property<Guid>("ExpenseReportId").IsRequired();
        b.Property(x => x.Date).HasColumnType("date");
        b.Property(x => x.Category).HasMaxLength(128).IsRequired();
        b.Property(x => x.ReceiptPath).HasMaxLength(1024);
        b.Property(x => x.Notes).HasMaxLength(1024);
        b.Property(x => x.Amount)
            .HasConversion(v => v.Amount, v => new Money(v, "USD"))
            .HasColumnType("decimal(18,2)")
            .HasColumnName("Amount");
        b.Property<string>("Currency").HasMaxLength(3).IsUnicode(false);
    }
}
