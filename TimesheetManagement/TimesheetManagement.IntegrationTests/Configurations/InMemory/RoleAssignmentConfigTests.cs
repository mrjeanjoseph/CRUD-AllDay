using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.IntegrationTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.IntegrationTests.Configurations.InMemory;

[Trait("Category","InMemory")]
public class RoleAssignmentConfigTests : IClassFixture<InMemoryDatabaseFixture>
{
    private readonly InMemoryDatabaseFixture _fixture;
    public RoleAssignmentConfigTests(InMemoryDatabaseFixture fixture) => _fixture = fixture;

    [Fact]
    public void RoleAssignmentConfig_MapsRequiredFieldsAndUniqueIndex()
    {
        var model = _fixture.DbContext.Model;
        var et = model.FindEntityType(typeof(RoleAssignment));
        et.Should().NotBeNull();

        var storeId = Microsoft.EntityFrameworkCore.Metadata.StoreObjectIdentifier.Table(et.GetTableName(), et.GetSchema());

        // required properties exist
        var requiredProps = new[] { "AdminId", "UserId", "CreatedBy", "CreatedOn", "Status" };
        foreach (var name in requiredProps)
            et.GetProperties().Any(p => p.Name == name).Should().BeTrue($"{name} should be a mapped property");

        // unique index on AdminId + UserId
        var hasIndex = et.GetIndexes().Any(ix => ix.Properties.Count == 2 &&
                                                ix.Properties.Any(p => p.Name == "AdminId") &&
                                                ix.Properties.Any(p => p.Name == "UserId"));
        hasIndex.Should().BeTrue("RoleAssignment should have unique index on (AdminId, UserId)");
    }
}
