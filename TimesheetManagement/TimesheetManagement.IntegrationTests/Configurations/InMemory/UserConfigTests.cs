using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.IntegrationTests.TestHelpers;

namespace TimesheetManagement.IntegrationTests.Configurations.InMemory;

[Trait("Category", "InMemory")]
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

        // Email unique index
        var hasEmailIndex = et.GetIndexes().Any(ix => ix.Properties.Any(p => p.GetColumnName(storeId) == "Email") || ix.Properties.Any(p => p.Name == "Email"));
        hasEmailIndex.Should().BeTrue("Email should have an index");
        var emailIndex = et.GetIndexes().First(ix => ix.Properties.Any(p => p.GetColumnName(storeId) == "Email") || ix.Properties.Any(p => p.Name == "Email"));
        emailIndex.IsUnique.Should().BeTrue("Email index should be unique");

        // PasswordHash mapping
        var pwdProp = et.GetProperties().FirstOrDefault(p => p.Name == "PasswordHash");
        pwdProp.Should().NotBeNull();
        pwdProp.GetMaxLength().Should().BeGreaterThan(0);
    }
}
