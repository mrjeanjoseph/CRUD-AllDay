using System;
using TimesheetManagement.Domain.Common.ValueObjects;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Expenses.ValueObjects;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.Domain.Projects;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.ValueObjects;

namespace TimesheetManagement.UnitTests.TestHelpers;

public static class TestData
{
    public static DateRange SampleDateRange => new(new DateOnly(2023, 1, 1), new DateOnly(2023, 1, 31));
    public static HoursWorked SampleHours => new(8.0m);
    public static Money SampleMoney => new(100.0m, "USD");

    public static User CreateSampleUser(Guid? id = null)
    {
        var user = new User("testuser", new Email("test@example.com"));
        if (id.HasValue)
        {
            typeof(User).GetProperty("Id", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(user, id.Value);
        }
        return user;
    }

    public static TimeSheet CreateSampleTimeSheet(Guid? userId = null, Guid? id = null)
    {
        var ts = new TimeSheet(userId ?? Guid.NewGuid(), new DateOnly(2023, 1, 1), new DateOnly(2023, 1, 31));
        if (id.HasValue)
        {
            typeof(TimeSheet).GetProperty("Id", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(ts, id.Value);
        }
        return ts;
    }

    public static ExpenseReport CreateSampleExpenseReport(Guid? userId = null, Guid? id = null)
    {
        var report = new ExpenseReport(userId ?? Guid.NewGuid(), new DateOnly(2023, 1, 1), new DateOnly(2023, 1, 31));
        if (id.HasValue)
        {
            typeof(ExpenseReport).GetProperty("Id", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(report, id.Value);
        }
        return report;
    }

    public static Project CreateSampleProject(Guid? id = null)
    {
        var project = new Project("PROJ001", "Test Project", "Tech");
        if (id.HasValue)
        {
            typeof(Project).GetProperty("Id", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(project, id.Value);
        }
        return project;
    }
}