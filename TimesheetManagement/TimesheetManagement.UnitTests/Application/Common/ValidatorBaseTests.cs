using FluentAssertions;
using TimesheetManagement.Application.Common.Validation;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Common;

public class ValidatorBaseTests
{
    [Fact]
    public void ValidatorBase_ShouldInheritFromAbstractValidator()
    {
        // Arrange
        var validator = new TestValidator();

        // Act & Assert
        validator.Should().BeAssignableTo<FluentValidation.AbstractValidator<string>>();
    }

    private class TestValidator : ValidatorBase<string> { }
}