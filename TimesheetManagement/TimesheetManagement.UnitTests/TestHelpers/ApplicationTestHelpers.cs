using Moq;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Identity.Repositories;
using TimesheetManagement.Domain.TimeTracking.Repositories;
using TimesheetManagement.Domain.Expenses.Repositories;
using TimesheetManagement.Domain.Projects.Repositories;
using TimesheetManagement.Domain.Teams.Repositories;

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

    public static Mock<IProjectRepository> CreateProjectRepository()
    {
        return new Mock<IProjectRepository>();
    }

    public static Mock<ITeamRepository> CreateTeamRepository()
    {
        return new Mock<ITeamRepository>();
    }

    // Add more as needed for other repositories or services
}