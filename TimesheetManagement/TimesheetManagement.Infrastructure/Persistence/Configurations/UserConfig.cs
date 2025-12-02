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

        // Map Email value object via ComplexProperty (record struct) without duplicating navigation
        b.ComplexProperty(x => x.Email, nb =>
        {
            nb.Property(v => v.Value).HasColumnName("Email").HasMaxLength(256).IsRequired();
        });

        b.Property(x => x.PasswordHash)
            .HasConversion(v => v.Value, v => new PasswordHash(v))
            .HasMaxLength(512);

        b.Property(x => x.Role).HasConversion<int>();

        b.HasIndex(x => x.Username).IsUnique();
        // Avoid separate Email index until EF supports indexing ComplexProperty easily; can add shadow property if needed.
    }
}
