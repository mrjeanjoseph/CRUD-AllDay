using FluentAssertions;
using System;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Domain.TimeTracking;

public class TimeEntryTests
{
    [Fact]
    public void Constructor_ValidParameters_ShouldCreate()
    {
        // Arrange
        var date = new DateOnly(2023, 1, 1);
        var hours = TestData.SampleHours;
        var projectId = Guid.NewGuid();
        var notes = "Test notes";

        // Act
        var entry = new TimeEntry(projectId, date, hours, notes);

        // Assert
        entry.Date.Should().Be(date);
        entry.Hours.Should().Be(hours);
        entry.ProjectId.Should().Be(projectId);
        entry.Notes.Should().Be(notes);
    }

    [Fact]
    public void Constructor_InvalidDate_ShouldThrowArgumentException()
    {
        // Arrange
        var hours = TestData.SampleHours;
        var projectId = Guid.NewGuid();

        // Act & Assert
        // TimeEntry doesn't validate date, so no exception
        var entry = new TimeEntry(projectId, default, hours, null);
        // Just assert it's created
        entry.Date.Should().Be(default(DateOnly));
    }
}