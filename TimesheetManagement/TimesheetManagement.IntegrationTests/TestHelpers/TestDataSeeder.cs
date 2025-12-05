using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.Domain.Projects;
using TimesheetManagement.Domain.Teams;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.ValueObjects;
using TimesheetManagement.Infrastructure.Persistence;

namespace TimesheetManagement.IntegrationTests.TestHelpers;

public static class TestDataSeeder
{
    public static async Task SeedBasicDataAsync(AppDbContext dbContext)
    {
        // Seed users
        var user1 = new User("testuser1", new Email("user1@example.com"));
        var user2 = new User("testuser2", new Email("user2@example.com"));
        await dbContext.Users.AddRangeAsync(user1, user2);

        // Seed projects
        var project1 = new Project("PROJ001", "Test Project 1", "Tech");
        var project2 = new Project("PROJ002", "Test Project 2", "Finance");
        await dbContext.Projects.AddRangeAsync(project1, project2);

        // Seed teams
        var team1 = new Team("Team Alpha");
        team1.AddMember(user1.Id);
        var team2 = new Team("Team Beta");
        await dbContext.Teams.AddRangeAsync(team1, team2);

        // Seed timesheets
        var ts1 = new TimeSheet(user1.Id, new DateOnly(2023, 1, 1), new DateOnly(2023, 1, 7));
        ts1.AddEntry(new TimeEntry(Guid.NewGuid(), new DateOnly(2023, 1, 2), new HoursWorked(8), "Work on PROJ001"));
        await dbContext.TimeSheets.AddAsync(ts1);

        // Seed expense reports
        var er1 = new ExpenseReport(user1.Id, new DateOnly(2023, 1, 1), new DateOnly(2023, 1, 31));
        await dbContext.ExpenseReports.AddAsync(er1);

        await dbContext.SaveChangesAsync();
    }

    public static async Task ClearDataAsync(AppDbContext dbContext)
    {
        // Use Respawn or manual delete for reset
        dbContext.TimeSheets.RemoveRange(dbContext.TimeSheets);
        dbContext.ExpenseReports.RemoveRange(dbContext.ExpenseReports);
        dbContext.Projects.RemoveRange(dbContext.Projects);
        dbContext.Teams.RemoveRange(dbContext.Teams);
        dbContext.Users.RemoveRange(dbContext.Users);
        await dbContext.SaveChangesAsync();
    }
}