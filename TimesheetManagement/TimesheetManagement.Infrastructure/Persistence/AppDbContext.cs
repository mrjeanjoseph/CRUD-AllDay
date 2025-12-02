using Microsoft.EntityFrameworkCore;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.Domain.Projects;
using TimesheetManagement.Domain.Teams;
using TimesheetManagement.Domain.TimeTracking;

namespace TimesheetManagement.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<RoleAssignment> RoleAssignments => Set<RoleAssignment>();

    public DbSet<TimeSheet> TimeSheets => Set<TimeSheet>();
    public DbSet<TimeEntry> TimeEntries => Set<TimeEntry>();

    public DbSet<ExpenseReport> ExpenseReports => Set<ExpenseReport>();
    public DbSet<ExpenseItem> ExpenseItems => Set<ExpenseItem>();

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<Team> Teams => Set<Team>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
