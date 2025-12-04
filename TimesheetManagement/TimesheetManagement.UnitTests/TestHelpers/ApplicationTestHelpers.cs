using Moq;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Identity.Repositories;
using TimesheetManagement.Domain.TimeTracking.Repositories;
using TimesheetManagement.Domain.Expenses.Repositories;

namespace TimesheetManagement.UnitTests.TestHelpers;

public static class ApplicationTestHelpers
{
    public static Mock<IUserRepository> CreateUserRepository()
    {
        return new Mock<IUserRepository>();
    }

    public static Mock<IUserContext> CreateUserContext()
    {
        return new Mock<IUserContext>();
    }

    public static Mock<ITimeSheetRepository> CreateTimeSheetRepository()
    {
        return new Mock<ITimeSheetRepository>();
    }

    public static Mock<IExpenseReportRepository> CreateExpenseReportRepository()
    {
        return new Mock<IExpenseReportRepository>();
    }

    // Add more as needed for other repositories or services
}