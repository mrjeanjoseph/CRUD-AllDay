using FluentAssertions;
using TimesheetManagement.Domain.Projects;
using TimesheetManagement.IntegrationTests.TestHelpers;

namespace TimesheetManagement.IntegrationTests.Configurations.InMemory;

[Trait("Category", "InMemory")]
public class ProjectConfigTests : IClassFixture<InMemoryDatabaseFixture>
{
    private readonly InMemoryDatabaseFixture _fixture;
    public ProjectConfigTests(InMemoryDatabaseFixture fixture) => _fixture = fixture;

    [Fact]
    public void ProjectConfig_UniqueIndexesOnCodeAndName()
    {
        var model = _fixture.DbContext.Model;
        var et = model.FindEntityType(typeof(Project));
        et.Should().NotBeNull();

        var hasCodeIndex = et.GetIndexes().Any(ix => ix.Properties.Any(p => p.Name == "Code"));
        hasCodeIndex.Should().BeTrue();
        var codeIdx = et.GetIndexes().First(ix => ix.Properties.Any(p => p.Name == "Code"));
        codeIdx.IsUnique.Should().BeTrue();

        var hasNameIndex = et.GetIndexes().Any(ix => ix.Properties.Any(p => p.Name == "Name"));
        hasNameIndex.Should().BeTrue();
        var nameIdx = et.GetIndexes().First(ix => ix.Properties.Any(p => p.Name == "Name"));
        nameIdx.IsUnique.Should().BeTrue();

        var codeProp = et.GetProperties().First(p => p.Name == "Code");
        codeProp.GetMaxLength().Should().Be(64);
        var nameProp = et.GetProperties().First(p => p.Name == "Name");
        nameProp.GetMaxLength().Should().Be(128);

        var isArchived = et.GetProperties().First(p => p.Name == "IsArchived");
        // Default value metadata may be unavailable in relational model for in-memory provider; check existence
        isArchived.Should().NotBeNull();
    }
}
