using System;
using System.Collections.Generic;
using System.Text.Json;
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

        // Map members list as JSON in a single column for simplicity
        var serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General);
        b.Property<List<Guid>>("_memberIds")
            .HasField("_memberIds")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasConversion(
                v => JsonSerializer.Serialize(v, serializerOptions),
                v => string.IsNullOrWhiteSpace(v) ? new List<Guid>() : JsonSerializer.Deserialize<List<Guid>>(v, serializerOptions)!
            )
            .HasColumnName("MemberIds")
            .HasColumnType("nvarchar(max)");
    }
}
