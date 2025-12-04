using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using TimesheetManagement.Application.Projects.Commands.CreateProject;
using TimesheetManagement.Domain.Projects.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Projects.Commands.CreateProject;

public class CreateProjectValidatorTests
{
    private readonly Mock<IProjectRepository> _repoMock;
    private readonly CreateProjectValidator _validator;

    public CreateProjectValidatorTests()
    {
        _repoMock = ApplicationTestHelpers.CreateProjectRepository();
        _validator = new CreateProjectValidator(_repoMock.Object);
    }

    [Fact]
    public async Task Validate_ValidCommand_ShouldPass()
    {
        // Arrange
        var command = new CreateProjectCommand("PROJ001", "Test Project", "Tech");
        _repoMock.Setup(x => x.CodeExistsAsync("PROJ001", default)).ReturnsAsync(false);
        _repoMock.Setup(x => x.NameExistsAsync("Test Project", default)).ReturnsAsync(false);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyCode_ShouldFail()
    {
        // Arrange
        var command = new CreateProjectCommand("", "Test Project", "Tech");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Code");
    }

    [Fact]
    public async Task Validate_CodeTooLong_ShouldFail()
    {
        // Arrange
        var command = new CreateProjectCommand(new string('A', 65), "Test Project", "Tech");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Code");
    }

    [Fact]
    public async Task Validate_EmptyName_ShouldFail()
    {
        // Arrange
        var command = new CreateProjectCommand("PROJ001", "", "Tech");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Name");
    }

    [Fact]
    public async Task Validate_NameTooLong_ShouldFail()
    {
        // Arrange
        var command = new CreateProjectCommand("PROJ001", new string('A', 129), "Tech");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Name");
    }

    [Fact]
    public async Task Validate_EmptyIndustry_ShouldFail()
    {
        // Arrange
        var command = new CreateProjectCommand("PROJ001", "Test Project", "");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Industry");
    }

    [Fact]
    public async Task Validate_IndustryTooLong_ShouldFail()
    {
        // Arrange
        var command = new CreateProjectCommand("PROJ001", "Test Project", new string('A', 129));

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Industry");
    }

    [Fact]
    public async Task Validate_CodeExists_ShouldFail()
    {
        // Arrange
        var command = new CreateProjectCommand("PROJ001", "Test Project", "Tech");
        _repoMock.Setup(x => x.CodeExistsAsync("PROJ001", default)).ReturnsAsync(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("Project code already exists"));
    }

    [Fact]
    public async Task Validate_NameExists_ShouldFail()
    {
        // Arrange
        var command = new CreateProjectCommand("PROJ001", "Test Project", "Tech");
        _repoMock.Setup(x => x.CodeExistsAsync("PROJ001", default)).ReturnsAsync(false);
        _repoMock.Setup(x => x.NameExistsAsync("Test Project", default)).ReturnsAsync(true);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("Project name already exists"));
    }
}