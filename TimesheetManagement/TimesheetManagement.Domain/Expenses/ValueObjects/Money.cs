namespace TimesheetManagement.Domain.Expenses.ValueObjects;
public readonly record struct Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency required", nameof(currency));
        Amount = amount;
        Currency = currency.ToUpperInvariant();
    }
    public override string ToString() => $"{Currency} {Amount:0.00}";
}
