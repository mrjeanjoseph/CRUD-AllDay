using FluentAssertions;
using System;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.Events;
using TimesheetManagement.Tests.TestHelpers;
using Xunit;

namespace TimesheetManagement.Tests.Domain.TimeTracking;

public class TimeSheetTests
{
    [Fact]
    public void Constructor_ValidParameters_ShouldCreate()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var period = TestData.SampleDateRange;

        // Act
        var timeSheet = new TimeSheet(userId, period.From, period.To);

        // Assert
        timeSheet.UserId.Should().Be(userId);
        timeSheet.Period.Should().Be(period);
        timeSheet.Status.Should().Be(TimeSheetStatus.Draft);
        timeSheet.Entries.Should().BeEmpty();
    }

    [Fact]
    public void AddEntry_ValidEntry_ShouldAdd()
    {
        // Arrange
        var timeSheet = new TimeSheet(Guid.NewGuid(), TestData.SampleDateRange.From, TestData.SampleDateRange.To);
        var entry = new TimeEntry(Guid.NewGuid(), TestData.SampleDateRange.From, TestData.SampleHours, "Notes");

        // Act
        timeSheet.AddEntry(entry);

        // Assert
        timeSheet.Entries.Should().Contain(entry);
    }

    [Fact]
    public void AddEntry_DateOutsidePeriod_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var timeSheet = new TimeSheet(Guid.NewGuid(), TestData.SampleDateRange.From, TestData.SampleDateRange.To);
        var entry = new TimeEntry(Guid.NewGuid(), new DateOnly(2022, 1, 1), TestData.SampleHours, "Notes");

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => timeSheet.AddEntry(entry));
    }

    [Fact]
    public void Submit_WithEntries_ShouldChangeStatusAndRaiseEvent()
    {
        // Arrange
        var timeSheet = new TimeSheet(Guid.NewGuid(), TestData.SampleDateRange.From, TestData.SampleDateRange.To);
        timeSheet.AddEntry(new TimeEntry(Guid.NewGuid(), TestData.SampleDateRange.From, TestData.SampleHours, "Notes"));

        // Act
        timeSheet.Submit();

        // Assert
        timeSheet.Status.Should().Be(TimeSheetStatus.Submitted);
        timeSheet.DomainEvents.Should().ContainSingle(e => e is TimeSheetSubmittedEvent);
    }

    [Fact]
    public void Submit_NoEntries_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var timeSheet = new TimeSheet(Guid.NewGuid(), TestData.SampleDateRange.From, TestData.SampleDateRange.To);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => timeSheet.Submit());
    }

    [Fact]
    public void Approve_FromSubmitted_ShouldChangeStatusAndRaiseEvent()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var from = TestData.SampleDateRange.From;
        var to = TestData.SampleDateRange.To;
        var timeSheet = new TimeSheet(userId, from, to);
        var projectId = Guid.NewGuid();
        var date = TestData.SampleDateRange.From;
        var hours = TestData.SampleHours;
        timeSheet.AddEntry(new TimeEntry(projectId, date, hours, "Notes"));
        timeSheet.Submit();
        timeSheet.ClearDomainEvents();

        // Act
        timeSheet.Approve();

        // Assert
        timeSheet.Status.Should().Be(TimeSheetStatus.Approved);
        timeSheet.DomainEvents.Should().ContainSingle(e => e is TimeSheetApprovedEvent);
    }

    [Fact]
    public void Approve_NotFromSubmitted_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var timeSheet = new TimeSheet(Guid.NewGuid(), TestData.SampleDateRange.From, TestData.SampleDateRange.To);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => timeSheet.Approve());
    }
}