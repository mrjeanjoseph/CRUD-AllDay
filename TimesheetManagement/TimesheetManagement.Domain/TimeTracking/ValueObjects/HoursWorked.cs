namespace TimesheetManagement.Domain.TimeTracking.ValueObjects;
public readonly record struct HoursWorked
{
    public decimal Value { get; }
    public HoursWorked(decimal value)
    {
        if (value <= 0 || value > 24) throw new ArgumentOutOfRangeException(nameof(value));
        Value = decimal.Round(value, 2);
    }
    public override string ToString() => Value.ToString("0.00");
    public static implicit operator decimal(HoursWorked hw) => hw.Value;
}
