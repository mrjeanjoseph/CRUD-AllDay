using FluentAssertions;
using TimesheetManagement.Domain.Teams;
using TimesheetManagement.IntegrationTests.TestHelpers;

namespace TimesheetManagement.IntegrationTests.Configurations.InMemory;

[Trait("Category", "InMemory")]
public class TeamConfigTests : IClassFixture<InMemoryDatabaseFixture>
{
    private readonly InMemoryDatabaseFixture _fixture;
    public TeamConfigTests(InMemoryDatabaseFixture fixture) => _fixture = fixture;

    [Fact]
    public void TeamConfig_MapsMembersTableAndUniqueName()
    {
        var model = _fixture.DbContext.Model;
        var et = model.FindEntityType(typeof(Team));
        et.Should().NotBeNull();

        var hasNameIndex = et.GetIndexes().Any(ix => ix.Properties.Any(p => p.Name == "Name"));
        hasNameIndex.Should().BeTrue();
        var nameIdx = et.GetIndexes().First(ix => ix.Properties.Any(p => p.Name == "Name"));
        nameIdx.IsUnique.Should().BeTrue();

        var nameProp = et.GetProperties().First(p => p.Name == "Name");
        nameProp.GetMaxLength().Should().Be(128);

        var tm = model.FindEntityType(typeof(TeamMember));
        tm.Should().NotBeNull();
        tm.GetKeys().Should().Contain(k => k.Properties.Count == 2);

        var fk = tm.GetForeignKeys().FirstOrDefault();
        fk.Should().NotBeNull();
        fk.Properties.Any(p => p.Name == "TeamId").Should().BeTrue();
    }
}
