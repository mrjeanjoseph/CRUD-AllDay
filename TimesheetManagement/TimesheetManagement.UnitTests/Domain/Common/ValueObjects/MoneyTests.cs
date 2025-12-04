using FluentAssertions;
using System;
using TimesheetManagement.Domain.Expenses.ValueObjects;
using Xunit;

namespace TimesheetManagement.UnitTests.Domain.Common.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Constructor_ValidAmountAndCurrency_ShouldCreate()
    {
        // Arrange
        decimal amount = 100.50m;
        string currency = "USD";

        // Act
        var money = new Money(amount, currency);

        // Assert
        money.Amount.Should().Be(amount);
        money.Currency.Should().Be(currency);
    }

    [Theory]
    [InlineData(-1.0)]
    [InlineData(0.0)]
    public void Constructor_InvalidAmount_ShouldThrowArgumentOutOfRangeException(decimal amount)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Money(amount, "USD"));
    }

    [Fact]
    public void Constructor_InvalidCurrency_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Money(100m, ""));
    }

    [Fact]
    public void Equality_SameAmountAndCurrency_ShouldBeEqual()
    {
        // Arrange
        var money1 = new Money(100m, "USD");
        var money2 = new Money(100m, "USD");

        // Act & Assert
        money1.Should().Be(money2);
        (money1 == money2).Should().BeTrue();
    }

    [Fact]
    public void Equality_DifferentAmount_ShouldNotBeEqual()
    {
        // Arrange
        var money1 = new Money(100m, "USD");
        var money2 = new Money(200m, "USD");

        // Act & Assert
        money1.Should().NotBe(money2);
        (money1 != money2).Should().BeTrue();
    }

    [Fact]
    public void Equality_DifferentCurrency_ShouldNotBeEqual()
    {
        // Arrange
        var money1 = new Money(100m, "USD");
        var money2 = new Money(100m, "EUR");

        // Act & Assert
        money1.Should().NotBe(money2);
    }
}