# Project Setup (High-Level)

This document outlines a high-level folder structure and essential files for the TimesheetManagement solution following Clean Architecture and DDD, targeting .NET 8.

## Solution Structure

- `TimesheetManagement.Domain`
  - Purpose: Pure domain (aggregates, entities, value objects, domain events, repository interfaces)
  - Folders:
    - `Common/`
      - `Entity.cs`, `IDomainEvent.cs`, `IHasDomainEvents.cs`
      - `ValueObjects/DateRange.cs`, `ValueObjects/Money.cs`, `ValueObjects/HoursWorked.cs`
    - `Identity/`
      - `User.cs`, `RoleAssignment.cs`, `Role.cs`
      - `Repositories/IUserRepository.cs`, `Repositories/IRoleAssignmentRepository.cs`
    - `TimeTracking/`
      - `TimeSheet.cs`, `TimeEntry.cs`, `TimeSheetStatus.cs`
      - `Events/TimeSheetSubmittedEvent.cs`, `Events/TimeSheetApprovedEvent.cs`, `Events/TimeSheetRejectedEvent.cs`
      - `Repositories/ITimeSheetRepository.cs`
    - `Expenses/`
      - `ExpenseReport.cs`, `ExpenseItem.cs`, `ExpenseStatus.cs`
      - `Events/ExpenseSubmittedEvent.cs`, `Events/ExpenseApprovedEvent.cs`, `Events/ExpenseRejectedEvent.cs`
      - `Repositories/IExpenseReportRepository.cs`
    - `Projects/`
      - `Project.cs`
      - `Repositories/IProjectRepository.cs`
    - `Teams/`
      - `Team.cs`, `TeamMember.cs`
      - `Repositories/ITeamRepository.cs`

- `TimesheetManagement.Application`
  - Purpose: Use cases (commands/queries), handlers, validators, DTOs, mappings
  - Folders:
    - `Common/`
      - `Abstractions/ICommand.cs`, `Abstractions/IQuery.cs`
      - `Abstractions/ICommandHandler.cs`, `Abstractions/IQueryHandler.cs`
      - `Abstractions/IUnitOfWork.cs`
      - `Validation/ValidatorBase.cs`
    - `Identity/`
      - `Queries/GetUserById/` (Query, Handler, Dto)
      - `Queries/GetUserByEmail/` (Query, Handler, Dto)
      - `Commands/RegisterUser/` (Command, Handler, Validator)
      - `Commands/AssignRole/` (Command, Handler, Validator)
      - `Commands/ChangePassword/` (Command, Handler, Validator)
    - `TimeTracking/`
      - `Commands/CreateTimeSheet/` (Command, Handler, Validator)
      - `Commands/AddTimeEntry/` (Command, Handler, Validator)
      - `Commands/SubmitTimeSheet/` (Command, Handler)
      - `Commands/ApproveTimeSheet/` (Command, Handler)
      - `Commands/RejectTimeSheet/` (Command, Handler)
      - `Commands/EditAfterRejection/` (Command, Handler)
      - `Queries/GetTimeSheetById/` (Query, Handler, Dto)
      - `Queries/GetTimeSheetsForUser/` (Query, Handler, Dto)
    - `Expenses/`
      - `Commands/CreateExpenseReport/`
      - `Commands/AddExpenseItem/`
      - `Commands/SubmitExpenseReport/`
      - `Commands/ApproveExpenseReport/`
      - `Commands/RejectExpenseReport/`
      - `Commands/EditExpenseAfterRejection/`
      - `Queries/GetExpenseReportById/`
      - `Queries/GetExpenseReportsForUser/`
    - `Projects/`
      - `Commands/CreateProject/` (Command, Handler, Validator)
      - `Commands/ArchiveProject/` (Command, Handler)
      - `Commands/RestoreProject/` (Command, Handler)
      - `Queries/GetAllProjects/` (Query, Handler, Dto)
      - `Queries/GetProjectByCode/` (Query, Handler, Dto)
    - `Teams/`
      - `Commands/CreateTeam/`
      - `Commands/AddTeamMember/`
      - `Commands/RemoveTeamMember/`
      - `Commands/ArchiveTeam/`
      - `Commands/RestoreTeam/`
      - `Queries/GetAllTeams/`
      - `Queries/GetTeamById/`

- `TimesheetManagement.Infrastructure`
  - Purpose: Persistence and external adapters (EF Core DbContext, repository implementations, UnitOfWork)
  - Folders:
    - `Persistence/`
      - `AppDbContext.cs`
      - `Configurations/` (EF Core entity type configs)
        - `Identity/UserConfig.cs`, `Identity/RoleAssignmentConfig.cs`
        - `TimeTracking/TimeSheetConfig.cs`, `TimeTracking/TimeEntryConfig.cs`
        - `Expenses/ExpenseReportConfig.cs`, `Expenses/ExpenseItemConfig.cs`
        - `Projects/ProjectConfig.cs`
        - `Teams/TeamConfig.cs`, `Teams/TeamMemberConfig.cs`
      - `Migrations/` (EF Core migrations)
      - `UnitOfWork.cs`
    - `Repositories/`
      - `Identity/UserRepository.cs`, `Identity/RoleAssignmentRepository.cs`
      - `TimeTracking/TimeSheetRepository.cs`
      - `Expenses/ExpenseReportRepository.cs`
      - `Projects/ProjectRepository.cs`
      - `Teams/TeamRepository.cs`
    - `DependencyInjection.cs` (AddInfrastructure extension)
    - `Adapters/` (optional; email, files, notifications)

- `TimesheetManagement.API`
  - Purpose: HTTP layer (controllers/endpoints, DI wiring, filters, versioning, Swagger)
  - Folders:
    - `Controllers/`
      - `IdentityController.cs`
      - `TimeSheetsController.cs`
      - `ExpenseReportsController.cs`
      - `ProjectsController.cs`
      - `TeamsController.cs`
      - `AuditController.cs` (optional)
    - `Filters/`
      - `ExceptionHandlingFilter.cs` (maps exceptions to ProblemDetails)
      - `ValidationFilter.cs` (optional)
    - `Mappings/` (optional, if mapping in API)
    - `App_Readme/`
      - `app-setup.md`
      - `api-setup.md`
      - `project-setup.md`
    - `Program.cs` (DI setup, middleware, `MapControllers()`)
    - `appsettings.json` (connection strings, auth, logging)

- `TimesheetManagement.UnitTests`
  - Purpose: Unit tests for Domain and Application
  - Folders:
    - `Domain/` (aggregate/value object tests)
    - `Application/` (handler/validator tests)

- `TimesheetManagement.IntegrationTests`
  - Purpose: Integration tests for Infrastructure (EF Core mappings, repositories, migrations)
  - Folders:
    - `Repositories/InMemory/`
    - `Repositories/SqlServer/`
    - `Persistence/` (DbContext, UnitOfWork tests)
    - `Configurations/` (entity config tests)

## Essential Cross-Cutting Files

- `.editorconfig` (code style)
- `Directory.Build.props` (shared MSBuild settings)
- `NuGet.Config` (optional)
- `README.md` (root solution overview)
- `global.json` (SDK pinning)

## Notes

- Keep Domain pure (no EF attributes, no framework dependencies).
- Application should reference Domain only; no direct EF/Dapper.
- Infrastructure references Domain and Application (for abstractions like `IUnitOfWork`).
- API references Application; bind requests to commands/queries and return DTOs.
- Start with the above folders; add detail as features mature.
