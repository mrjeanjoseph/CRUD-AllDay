using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.IntegrationTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.IntegrationTests.Configurations.InMemory;

[Trait("Category","InMemory")]
public class UserConfigTests : IClassFixture<InMemoryDatabaseFixture>
{
    private readonly InMemoryDatabaseFixture _fixture;

    public UserConfigTests(InMemoryDatabaseFixture fixture) => _fixture = fixture;

    [Fact]
    public void UserConfig_MapsEmailAsColumnAndHasIndexes()
    {
        var model = _fixture.DbContext.Model;
        var et = model.FindEntityType(typeof(User));
        et.Should().NotBeNull();

        var storeId = Microsoft.EntityFrameworkCore.Metadata.StoreObjectIdentifier.Table(et.GetTableName(), et.GetSchema());
        var hasEmailColumn = et.GetProperties().Any(p => p.GetColumnName(storeId) == "Email");
        hasEmailColumn.Should().BeTrue("Email value object should be mapped to column 'Email'");

        var hasUsernameIndex = et.GetIndexes().Any(ix => ix.Properties.Any(p => p.Name == "Username"));
        hasUsernameIndex.Should().BeTrue("Username should have an index");

        var usernameIndex = et.GetIndexes().First(ix => ix.Properties.Any(p => p.Name == "Username"));
        usernameIndex.IsUnique.Should().BeTrue("Username index should be unique");
    }
}
