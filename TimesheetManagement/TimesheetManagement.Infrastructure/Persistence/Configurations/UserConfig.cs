using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimesheetManagement.Domain.Identity;

namespace TimesheetManagement.Infrastructure.Persistence.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> b)
    {
        b.ToTable("Users");
        b.HasKey(x => x.Id);
        b.Property(x => x.Username).HasMaxLength(64).IsRequired();

        // Map Email value object via HasConversion to a single Email column
        b.Property(x => x.Email)
            .HasConversion(v => v.Value, v => new Email(v))
            .HasColumnName("Email")
            .HasMaxLength(256)
            .IsRequired();

        // Create unique index on Email
        b.HasIndex(x => x.Email).IsUnique();

        b.Property(x => x.PasswordHash)
            .HasConversion(v => v.Value, v => new PasswordHash(v))
            .HasMaxLength(512);

        b.Property(x => x.Role).HasConversion<int>();

        b.HasIndex(x => x.Username).IsUnique();
    }
}
