using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TimesheetManagement.Domain.Teams;
using TimesheetManagement.IntegrationTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.IntegrationTests.Configurations.InMemory;

[Trait("Category","InMemory")]
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

        var tm = model.FindEntityType(typeof(TeamMember));
        tm.Should().NotBeNull();
        tm.GetKeys().Should().Contain(k => k.Properties.Count == 2);
    }
}
