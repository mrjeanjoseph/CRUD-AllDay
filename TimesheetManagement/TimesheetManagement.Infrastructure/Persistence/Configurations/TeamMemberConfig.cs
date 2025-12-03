using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimesheetManagement.Domain.Teams;

namespace TimesheetManagement.Infrastructure.Persistence.Configurations;

public class TeamMemberConfig : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> b)
    {
        b.ToTable("TeamMembers");
        b.HasKey(tm => new { tm.TeamId, tm.UserId });
        b.Property(tm => tm.TeamId).IsRequired();
        b.Property(tm => tm.UserId).IsRequired();
    }
}
