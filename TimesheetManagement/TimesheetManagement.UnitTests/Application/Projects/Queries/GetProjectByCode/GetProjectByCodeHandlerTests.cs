using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetManagement.Application.Projects.Queries.GetProjectByCode;
using TimesheetManagement.Domain.Projects.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Projects.Queries.GetProjectByCode;

public class GetProjectByCodeHandlerTests
{
    private readonly Mock<IProjectRepository> _repoMock;
    private readonly GetProjectByCodeHandler _handler;

    public GetProjectByCodeHandlerTests()
    {
        _repoMock = ApplicationTestHelpers.CreateProjectRepository();
        _handler = new GetProjectByCodeHandler(_repoMock.Object);
    }

    [Fact]
    public async Task Handle_ValidQuery_ShouldReturnDto()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var project = TestData.CreateSampleProject(projectId);
        var query = new GetProjectByCodeQuery("PROJ001");
        _repoMock.Setup(x => x.GetByCodeAsync("PROJ001", default)).ReturnsAsync(project);

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(projectId);
        result.Code.Should().Be("PROJ001");
        result.Name.Should().Be("Test Project");
        result.Industry.Should().Be("Tech");
        result.IsArchived.Should().BeFalse();
        _repoMock.Verify(x => x.GetByCodeAsync("PROJ001", default), Times.Once);
    }

    [Fact]
    public async Task Handle_ProjectNotFound_ShouldThrow()
    {
        // Arrange
        var query = new GetProjectByCodeQuery("NONEXISTENT");
        _repoMock.Setup(x => x.GetByCodeAsync("NONEXISTENT", default)).ReturnsAsync((TimesheetManagement.Domain.Projects.Project?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(query, default);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Project not found");
    }
}