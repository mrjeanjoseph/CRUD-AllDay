using System;
using TimesheetManagement.Domain.Common.ValueObjects;
using TimesheetManagement.Domain.Expenses.ValueObjects;
using TimesheetManagement.Domain.TimeTracking.ValueObjects;

namespace TimesheetManagement.Tests.TestHelpers;

public static class TestData
{
    public static DateRange SampleDateRange => new(new DateOnly(2023, 1, 1), new DateOnly(2023, 1, 31));
    public static HoursWorked SampleHours => new(8.0m);
    public static Money SampleMoney => new(100.0m, "USD");
}