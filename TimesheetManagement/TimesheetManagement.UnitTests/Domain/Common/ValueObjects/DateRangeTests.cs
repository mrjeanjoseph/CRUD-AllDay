using FluentAssertions;
using System;
using TimesheetManagement.Domain.Common.ValueObjects;
using Xunit;

namespace TimesheetManagement.UnitTests.Domain.Common.ValueObjects;

public class DateRangeTests
{
    [Fact]
    public void Constructor_ValidDates_ShouldCreateRange()
    {
        // Arrange
        var from = new DateOnly(2023, 1, 1);
        var to = new DateOnly(2023, 1, 31);

        // Act
        var range = new DateRange(from, to);

        // Assert
        range.From.Should().Be(from);
        range.To.Should().Be(to);
    }

    [Fact]
    public void Constructor_ToBeforeFrom_ShouldThrowArgumentException()
    {
        // Arrange
        var from = new DateOnly(2023, 1, 31);
        var to = new DateOnly(2023, 1, 1);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new DateRange(from, to));
    }

    [Fact]
    public void TotalDays_ShouldCalculateCorrectly()
    {
        // Arrange
        var from = new DateOnly(2023, 1, 1);
        var to = new DateOnly(2023, 1, 5);
        var range = new DateRange(from, to);

        // Act & Assert
        range.TotalDays.Should().Be(5);
    }

    [Theory]
    [InlineData(2023, 1, 1, true)]  // From date
    [InlineData(2023, 1, 15, true)] // Middle
    [InlineData(2023, 1, 31, true)] // To date
    [InlineData(2022, 12, 31, false)] // Before
    [InlineData(2023, 2, 1, false)]  // After
    public void Contains_ShouldReturnCorrectResult(int year, int month, int day, bool expected)
    {
        // Arrange
        var from = new DateOnly(2023, 1, 1);
        var to = new DateOnly(2023, 1, 31);
        var range = new DateRange(from, to);
        var date = new DateOnly(year, month, day);

        // Act & Assert
        range.Contains(date).Should().Be(expected);
    }
}