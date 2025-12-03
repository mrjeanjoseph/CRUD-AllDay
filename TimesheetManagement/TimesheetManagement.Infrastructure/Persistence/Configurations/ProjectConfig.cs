using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimesheetManagement.Domain.Projects;

namespace TimesheetManagement.Infrastructure.Persistence.Configurations;

public class ProjectConfig : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> b)
    {
        b.ToTable("Projects");
        b.HasKey(x => x.Id);
        b.Property(x => x.Code).HasMaxLength(64).IsRequired();
        b.Property(x => x.Name).HasMaxLength(128).IsRequired();
        b.Property(x => x.Industry).HasMaxLength(128).IsRequired();
        b.Property(x => x.IsArchived).HasDefaultValue(false);
        b.HasIndex(x => x.Code).IsUnique();
        b.HasIndex(x => x.Name).IsUnique();
    }
}
