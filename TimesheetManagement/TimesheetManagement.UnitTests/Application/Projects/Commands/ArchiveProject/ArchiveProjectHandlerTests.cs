using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Projects.Commands.ArchiveProject;
using TimesheetManagement.Domain.Projects;
using TimesheetManagement.Domain.Projects.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Projects.Commands.ArchiveProject;

public class ArchiveProjectHandlerTests
{
    private readonly Mock<IProjectRepository> _repoMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly ArchiveProjectHandler _handler;

    public ArchiveProjectHandlerTests()
    {
        _repoMock = ApplicationTestHelpers.CreateProjectRepository();
        _uowMock = new Mock<IUnitOfWork>();
        _handler = new ArchiveProjectHandler(_repoMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldArchiveAndReturnTrue()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var project = TestData.CreateSampleProject(projectId);
        var command = new ArchiveProjectCommand(projectId);
        _repoMock.Setup(x => x.GetAsync(projectId, default)).ReturnsAsync(project);
        _repoMock.Setup(x => x.UpdateAsync(project, default)).Returns(Task.CompletedTask);
        _uowMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().BeTrue();
        project.IsArchived.Should().BeTrue();
        _repoMock.Verify(x => x.GetAsync(projectId, default), Times.Once);
        _repoMock.Verify(x => x.UpdateAsync(project, default), Times.Once);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task Handle_ProjectNotFound_ShouldThrow()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var command = new ArchiveProjectCommand(projectId);
        _repoMock.Setup(x => x.GetAsync(projectId, default)).ReturnsAsync((TimesheetManagement.Domain.Projects.Project?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, default);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Project not found");
        _repoMock.Verify(x => x.UpdateAsync(It.IsAny<Project>(), default), Times.Never);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Never);
    }
}