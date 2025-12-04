using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimesheetManagement.Application.Projects.Queries.GetAllProjects;
using TimesheetManagement.Domain.Projects;
using TimesheetManagement.Domain.Projects.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Projects.Queries.GetAllProjects;

public class GetAllProjectsHandlerTests
{
    private readonly Mock<IProjectRepository> _repoMock;
    private readonly GetAllProjectsHandler _handler;

    public GetAllProjectsHandlerTests()
    {
        _repoMock = ApplicationTestHelpers.CreateProjectRepository();
        _handler = new GetAllProjectsHandler(_repoMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllProjectsAsDtos()
    {
        // Arrange
        var project1 = TestData.CreateSampleProject(Guid.NewGuid());
        var project2 = TestData.CreateSampleProject(Guid.NewGuid());
        project2.Archive();
        var projects = new List<Project> { project1, project2 };
        var query = new GetAllProjectsQuery();
        _repoMock.Setup(x => x.GetAllAsync(default)).ReturnsAsync(projects);

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().HaveCount(2);
        result.First().Id.Should().Be(project1.Id);
        result.First().Code.Should().Be("PROJ001");
        result.First().Name.Should().Be("Test Project");
        result.First().Industry.Should().Be("Tech");
        result.First().IsArchived.Should().BeFalse();
        result.Last().IsArchived.Should().BeTrue();
        _repoMock.Verify(x => x.GetAllAsync(default), Times.Once);
    }

    [Fact]
    public async Task Handle_NoProjects_ShouldReturnEmptyList()
    {
        // Arrange
        var query = new GetAllProjectsQuery();
        _repoMock.Setup(x => x.GetAllAsync(default)).ReturnsAsync(new List<Project>());

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().BeEmpty();
    }
}