using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Projects.Commands.CreateProject;
using TimesheetManagement.Domain.Projects;
using TimesheetManagement.Domain.Projects.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Projects.Commands.CreateProject;

public class CreateProjectHandlerTests
{
    private readonly Mock<IProjectRepository> _repoMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly CreateProjectHandler _handler;

    public CreateProjectHandlerTests()
    {
        _repoMock = ApplicationTestHelpers.CreateProjectRepository();
        _uowMock = new Mock<IUnitOfWork>();
        _handler = new CreateProjectHandler(_repoMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateAndReturnId()
    {
        // Arrange
        var command = new CreateProjectCommand("PROJ001", "Test Project", "Tech");
        _repoMock.Setup(x => x.CodeExistsAsync("PROJ001", default)).ReturnsAsync(false);
        _repoMock.Setup(x => x.NameExistsAsync("Test Project", default)).ReturnsAsync(false);
        _repoMock.Setup(x => x.AddAsync(It.IsAny<Project>(), default)).Returns(Task.CompletedTask);
        _uowMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().NotBeEmpty();
        _repoMock.Verify(x => x.CodeExistsAsync("PROJ001", default), Times.Once);
        _repoMock.Verify(x => x.NameExistsAsync("Test Project", default), Times.Once);
        _repoMock.Verify(x => x.AddAsync(It.Is<Project>(p => p.Code == "PROJ001" && p.Name == "Test Project" && p.Industry == "Tech"), default), Times.Once);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task Handle_CodeExists_ShouldThrow()
    {
        // Arrange
        var command = new CreateProjectCommand("PROJ001", "Test Project", "Tech");
        _repoMock.Setup(x => x.CodeExistsAsync("PROJ001", default)).ReturnsAsync(true);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, default);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Project code already exists");
        _repoMock.Verify(x => x.AddAsync(It.IsAny<Project>(), default), Times.Never);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Never);
    }

    [Fact]
    public async Task Handle_NameExists_ShouldThrow()
    {
        // Arrange
        var command = new CreateProjectCommand("PROJ001", "Test Project", "Tech");
        _repoMock.Setup(x => x.CodeExistsAsync("PROJ001", default)).ReturnsAsync(false);
        _repoMock.Setup(x => x.NameExistsAsync("Test Project", default)).ReturnsAsync(true);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, default);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Project name already exists");
        _repoMock.Verify(x => x.AddAsync(It.IsAny<Project>(), default), Times.Never);
        _uowMock.Verify(x => x.SaveChangesAsync(default), Times.Never);
    }
}