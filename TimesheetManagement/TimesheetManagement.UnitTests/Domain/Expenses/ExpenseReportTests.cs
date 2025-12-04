using FluentAssertions;
using System;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Expenses.Events;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Domain.Expenses;

public class ExpenseReportTests
{
    [Fact]
    public void Constructor_ValidParameters_ShouldCreate()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var from = TestData.SampleDateRange.From;
        var to = TestData.SampleDateRange.To;

        // Act
        var report = new ExpenseReport(userId, from, to);

        // Assert
        report.UserId.Should().Be(userId);
        report.Period.From.Should().Be(from);
        report.Period.To.Should().Be(to);
        report.Status.Should().Be(ExpenseStatus.Draft);
        report.Items.Should().BeEmpty();
    }

    [Fact]
    public void AddItem_ValidItem_ShouldAdd()
    {
        // Arrange
        var report = new ExpenseReport(Guid.NewGuid(), TestData.SampleDateRange.From, TestData.SampleDateRange.To);
        var item = new ExpenseItem(TestData.SampleDateRange.From, "Travel", TestData.SampleMoney, null, null);

        // Act
        report.AddItem(item);

        // Assert
        report.Items.Should().Contain(item);
    }

    [Fact]
    public void AddItem_DateOutsidePeriod_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var report = new ExpenseReport(Guid.NewGuid(), TestData.SampleDateRange.From, TestData.SampleDateRange.To);
        var item = new ExpenseItem(new DateOnly(2022, 1, 1), "Travel", TestData.SampleMoney, null, null);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => report.AddItem(item));
    }

    [Fact]
    public void Submit_WithItems_ShouldChangeStatusAndRaiseEvent()
    {
        // Arrange
        var report = new ExpenseReport(Guid.NewGuid(), TestData.SampleDateRange.From, TestData.SampleDateRange.To);
        report.AddItem(new ExpenseItem(TestData.SampleDateRange.From, "Travel", TestData.SampleMoney, null, null));

        // Act
        report.Submit();

        // Assert
        report.Status.Should().Be(ExpenseStatus.Submitted);
        report.DomainEvents.Should().ContainSingle(e => e is ExpenseSubmittedEvent);
    }

    [Fact]
    public void Submit_NoItems_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var report = new ExpenseReport(Guid.NewGuid(), TestData.SampleDateRange.From, TestData.SampleDateRange.To);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => report.Submit());
    }

    [Fact]
    public void Approve_FromSubmitted_ShouldChangeStatusAndRaiseEvent()
    {
        // Arrange
        var report = new ExpenseReport(Guid.NewGuid(), TestData.SampleDateRange.From, TestData.SampleDateRange.To);
        report.AddItem(new ExpenseItem(TestData.SampleDateRange.From, "Travel", TestData.SampleMoney, null, null));
        report.Submit();
        report.ClearDomainEvents();

        // Act
        report.Approve();

        // Assert
        report.Status.Should().Be(ExpenseStatus.Approved);
        report.DomainEvents.Should().ContainSingle(e => e is ExpenseApprovedEvent);
    }
}