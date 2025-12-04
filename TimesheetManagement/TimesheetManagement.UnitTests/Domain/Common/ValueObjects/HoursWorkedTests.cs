using FluentAssertions;
using System;
using TimesheetManagement.Domain.TimeTracking.ValueObjects;
using Xunit;

namespace TimesheetManagement.UnitTests.Domain.Common.ValueObjects;

public class HoursWorkedTests
{
    [Theory]
    [InlineData(1.0)]
    [InlineData(8.5)]
    [InlineData(24.0)]
    public void Constructor_ValidValue_ShouldCreate(decimal value)
    {
        // Act
        var hours = new HoursWorked(value);

        // Assert
        hours.Value.Should().Be(Math.Round(value, 2));
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-1.0)]
    [InlineData(24.1)]
    public void Constructor_InvalidValue_ShouldThrowArgumentOutOfRangeException(decimal value)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new HoursWorked(value));
    }

    [Fact]
    public void ToString_ShouldFormatCorrectly()
    {
        // Arrange
        var hours = new HoursWorked(8.5m);

        // Act & Assert
        hours.ToString().Should().Be("8.50");
    }

    [Fact]
    public void ImplicitOperator_ShouldConvertToDecimal()
    {
        // Arrange
        var hours = new HoursWorked(8.5m);

        // Act
        decimal value = hours;

        // Assert
        value.Should().Be(8.5m);
    }
}