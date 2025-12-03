using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimesheetManagement.Domain.Identity;

namespace TimesheetManagement.Infrastructure.Persistence.Configurations;

public class RoleAssignmentConfig : IEntityTypeConfiguration<RoleAssignment>
{
    public void Configure(EntityTypeBuilder<RoleAssignment> b)
    {
        b.ToTable("RoleAssignments");
        b.HasKey(x => x.Id);
        b.Property(x => x.AdminId).IsRequired();
        b.Property(x => x.UserId).IsRequired();
        b.Property(x => x.CreatedBy).IsRequired();
        b.Property(x => x.CreatedOn).IsRequired();
        b.Property(x => x.Status).HasConversion<int>();
        b.HasIndex(x => new { x.AdminId, x.UserId }).IsUnique();
    }
}
