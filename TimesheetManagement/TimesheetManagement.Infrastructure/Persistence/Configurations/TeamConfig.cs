using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimesheetManagement.Domain.Teams;

namespace TimesheetManagement.Infrastructure.Persistence.Configurations;

public class TeamConfig : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> b)
    {
        b.ToTable("Teams");
        b.HasKey(x => x.Id);
        b.Property(x => x.Name).HasMaxLength(128).IsRequired();
        b.Property(x => x.IsArchived).HasDefaultValue(false);
        b.HasIndex(x => x.Name).IsUnique();

        // Map members via junction table
        b.HasMany(x => x.Members).WithOne().HasForeignKey(tm => tm.TeamId).OnDelete(DeleteBehavior.Cascade);
    }
}
