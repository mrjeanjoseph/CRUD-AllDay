using FluentAssertions;
using System;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Expenses.ValueObjects;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Domain.Expenses;

public class ExpenseItemTests
{
    [Fact]
    public void Constructor_ValidParameters_ShouldCreate()
    {
        // Arrange
        var date = new DateOnly(2023, 1, 1);
        var category = "Travel";
        var amount = TestData.SampleMoney;
        var receiptPath = "path/to/receipt.jpg";
        var notes = "Test notes";

        // Act
        var item = new ExpenseItem(date, category, amount, receiptPath, notes);

        // Assert
        item.Date.Should().Be(date);
        item.Category.Should().Be(category);
        item.Amount.Should().Be(amount);
        item.ReceiptPath.Should().Be(receiptPath);
        item.Notes.Should().Be(notes);
    }

    [Fact]
    public void Constructor_InvalidCategory_ShouldThrowArgumentException()
    {
        // Arrange
        var date = new DateOnly(2023, 1, 1);
        var amount = TestData.SampleMoney;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new ExpenseItem(date, "", amount, null, null));
    }

    [Fact]
    public void Update_ValidParameters_ShouldUpdate()
    {
        // Arrange
        var item = new ExpenseItem(new DateOnly(2023, 1, 1), "Travel", TestData.SampleMoney, null, null);
        var newCategory = "Meals";
        var newAmount = new Money(50m, "USD");

        // Act
        item.Update(newCategory, newAmount, "newpath", "newnotes");

        // Assert
        item.Category.Should().Be(newCategory);
        item.Amount.Should().Be(newAmount);
        item.ReceiptPath.Should().Be("newpath");
        item.Notes.Should().Be("newnotes");
    }
}