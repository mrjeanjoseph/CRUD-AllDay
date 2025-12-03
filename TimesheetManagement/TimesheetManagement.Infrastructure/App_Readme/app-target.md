Infrastructure Layer Readme (TimesheetManagement, .NET 8)

Purpose
- Provide persistence and external adapter implementations for the Domain and Application layers
- Keep business rules out of Infrastructure; implement repository patterns, DbContext, and adapters only

What belongs here
- EF Core 8 DbContext and configurations (Fluent API)
- Repository implementations for Domain repository interfaces
- IUnitOfWork implementation (backed by DbContext)
- External adapters (email, file storage for receipts) hidden behind interfaces
- Optional Dapper-based read models (behind interfaces)

What does NOT belong here
- Domain rules or state transitions
- UI/PageModel logic
- Direct usage of legacy DataContext patterns or ConfigurationManager in new code

Core components to implement
1) DbContext
- File: `Persistence/AppDbContext.cs`
- Responsibilities:
  - Expose DbSet for aggregates: `Users`, `RoleAssignments`, `TimeSheets`, `TimeEntries`, `ExpenseReports`, `ExpenseItems`, `Projects`, `Teams`
  - Track and expose domain events (optional: via SaveChanges interceptor)
  - Apply entity type configurations in `OnModelCreating`
- Notes:
  - EF Core 8 supports `DateOnly`. Ensure provider supports it (SQL Server does via conversions). If needed, add converters.
  - Use UTC for `DateTime` columns.

2) Entity configurations (Fluent API)
- Directory: `Persistence/Configurations`
- Identity
  - `UserConfig`: key `Id` (GUID), required `Username`, `Email.Value` as `Email` column, `Role` enum string or int (choose one). Add unique index on `Email` and optionally on `Username`.
  - `RoleAssignmentConfig`: keys, `AdminId`, `UserId`, `CreatedBy`, `Status` enum. Add unique index on `(AdminId, UserId)` if required.
- TimeTracking
  - `TimeSheetConfig`: key `Id`, `UserId`, `From` and `To` from `DateRange` as `FromDate`/`ToDate` shadow columns (configure owned value object or separate columns), `Status`, `Comment`.
  - `TimeEntryConfig`: key `Id`, `ProjectId`, `Date`, `Hours.Value` as `Hours`, `Notes`, FK to `TimeSheet` with cascade delete.
- Expenses
  - `ExpenseReportConfig`: key `Id`, `UserId`, `FromDate`/`ToDate` from `DateRange`, `Status`, `Comment`.
  - `ExpenseItemConfig`: key `Id`, `Date`, `Category`, `Amount.Amount` and `Amount.Currency`, `ReceiptPath`, `Notes`, FK to `ExpenseReport` with cascade delete.
- Projects
  - `ProjectConfig`: key `Id`, `Code` unique, `Name`, `Industry`, `IsArchived`.
- Teams
  - `TeamConfig`: key `Id`, `Name` unique, `IsArchived`, serialize `_memberIds` as a separate table `TeamMembers` with composite key `(TeamId, UserId)`.

3) Value object mappings
- `DateRange`: map as owned type with `From` and `To` columns or configure two separate properties on the aggregate and hide with backing fields.
- `HoursWorked`: map `Value` to `decimal(5,2)`.
- `Money`: map to two columns `Amount decimal(18,2)`, `Currency char(3)`.
- `Email`, `PasswordHash`: map `.Value` to `nvarchar` columns.

4) Repository implementations
- Directory: `Repositories`
- Implement for each Domain interface:
  - `Identity`: `UserRepository`, `RoleAssignmentRepository`
  - `TimeTracking`: `TimeSheetRepository`
  - `Expenses`: `ExpenseReportRepository`
  - `Projects`: `ProjectRepository`
  - `Teams`: `TeamRepository`
- Guidelines:
  - Return aggregates, not EF tracking proxies beyond handler scope
  - No business decisions here; just CRUD and query filters required by handlers
  - Implement overlap checks e.g., `HasSubmittedForRangeAsync` efficiently (indexes on `UserId`, `FromDate`, `ToDate`, `Status`)

5) Unit of Work
- File: `Persistence/UnitOfWork.cs`
- Implement `IUnitOfWork` (Application) by delegating to DbContext `SaveChangesAsync`
- Optional: Dispatch domain events after successful commit (see next)

6) Domain events dispatching
- Option A (simple): Application reads `aggregate.DomainEvents` and dispatches via an injected dispatcher; Infrastructure does nothing special
- Option B (infra-based): implement a `SaveChanges` interceptor to capture events from tracked aggregates and publish via an event dispatcher abstraction
- Choose A initially to keep infra simple. Provide a `IDomainEventDispatcher` in Application later if needed.

7) DI registration
- File: `DependencyInjection.cs`
- Provide `AddInfrastructure(IConfiguration config)` method to:
  - Configure `AppDbContext` with `UseSqlServer(config.GetConnectionString("Default"))` (or your provider)
  - Register repositories `IUserRepository` -> `UserRepository`, etc.
  - Register `IUnitOfWork` -> `UnitOfWork`
- Web `Program.cs` calls `services.AddInfrastructure(Configuration)`

8) Migrations and database
- Create migrations in the Infrastructure project: `dotnet ef migrations add Initial --project TimesheetManagement.Infrastructure --startup-project TimesheetManagement`
- Place migration files under `Persistence/Migrations`
- Connection string in Web `appsettings.json`, key `ConnectionStrings:Default`

9) Bridging legacy to new (anti-corruption)
- Legacy concretes present:
  - `ProjectConcrete`, `TimeSheetConcrete`, `ExpenseConcrete`, `AssignRolesConcrete`, `UsersConcrete`, `RolesConcrete`, `LoginConcrete`, `NotificationConcrete`, `AuditConcrete`, `DocumentConcrete`, `ExpenseExportConcrete`, `TimeSheetExportConcrete`
- Strategy:
  - Do not modify these for new code. Keep them to avoid breaking existing pages while migrating.
  - Optionally add adapters that implement these legacy interfaces by delegating to new repositories/handlers to reduce duplicated logic.
  - Mark legacy services as obsolete once a page is migrated.
  - Remove after all call sites are switched.

10) File storage (receipts)
- If receipts are stored as files, add an abstraction (e.g., `IReceiptStorage`) in Application and implement here (local disk or cloud blob). For now, keep Infrastructure implementation simple and configurable.

11) Dapper usage (optional)
- For heavy read queries (exports), you can provide optimized readers behind new query interfaces. Keep them in Infrastructure and return DTOs. Do not expose Dapper directly to Application.

12) Performance and reliability
- Add indexes on foreign keys and query columns (`UserId`, `FromDate`, `ToDate`, `Status`, `Project.Code`, `Team.Name`)
- Configure cascade deletes carefully to avoid accidental data loss
- Consider optimistic concurrency with a `rowversion` column on aggregates that are frequently updated

13) Testing
- Integration tests project can reference Infrastructure with a test DbContext using SQLite in-memory or Testcontainers
- Seed minimal data per test; verify repository behaviors and mappings

14) Provider-specific notes (SQL Server)
- Use `HasConversion` only if needed for `DateOnly`; EF Core 8 supports it
- Use `decimal(18,2)` for money values
- Set `IsUnicode(false)` for ISO currency code columns

Suggested file layout
- `Persistence/AppDbContext.cs`
- `Persistence/Configurations/UserConfig.cs`
- `Persistence/Configurations/RoleAssignmentConfig.cs`
- `Persistence/Configurations/TimeSheetConfig.cs`
- `Persistence/Configurations/TimeEntryConfig.cs`
- `Persistence/Configurations/ExpenseReportConfig.cs`
- `Persistence/Configurations/ExpenseItemConfig.cs`
- `Persistence/Configurations/ProjectConfig.cs`
- `Persistence/Configurations/TeamConfig.cs`
- `Repositories/UserRepository.cs`
- `Repositories/RoleAssignmentRepository.cs`
- `Repositories/TimeSheetRepository.cs`
- `Repositories/ExpenseReportRepository.cs`
- `Repositories/ProjectRepository.cs`
- `Repositories/TeamRepository.cs`
- `Persistence/UnitOfWork.cs`
- `DependencyInjection.cs`

Data model mapping hints
- TimeSheet
  - Table: `TimeSheets` (`Id` PK, `UserId` GUID, `FromDate` date, `ToDate` date, `Status` int, `Comment` nvarchar)
  - Entries: `TimeEntries` (`Id` PK, `TimeSheetId` FK, `ProjectId` GUID, `Date` date, `Hours` decimal(5,2), `Notes` nvarchar)
- ExpenseReport
  - Table: `ExpenseReports` (`Id` PK, `UserId` GUID, `FromDate` date, `ToDate` date, `Status` int, `Comment` nvarchar)
  - Items: `ExpenseItems` (`Id` PK, `ExpenseReportId` FK, `Date` date, `Category` nvarchar(128), `Amount` decimal(18,2), `Currency` char(3), `ReceiptPath` nvarchar, `Notes` nvarchar)
- Project
  - Table: `Projects` (`Id` PK, `Code` nvarchar(64) unique, `Name` nvarchar(128), `Industry` nvarchar(128), `IsArchived` bit)
- Team
  - Table: `Teams` (`Id` PK, `Name` nvarchar(128) unique, `IsArchived` bit)
  - Members: `TeamMembers` (`TeamId` FK, `UserId` GUID, PK(TeamId, UserId))
- Identity
  - Users: `Users` (`Id` PK, `Username` nvarchar(64), `Email` nvarchar(256) unique, `PasswordHash` nvarchar(512), `Role` int)
  - RoleAssignments: `RoleAssignments` (`Id` PK, `AdminId` GUID, `UserId` GUID, `Status` int, `CreatedBy` GUID, `CreatedOn` datetime2)

Migration and rollout plan
- Phase 1: Implement DbContext, configs, repositories, DI registration
- Phase 2: Wire Web pages for Identity, TimeTracking, Expenses to new Application handlers
- Phase 3: Migrate reporting/export to query handlers or Dapper readers; remove legacy export concretes
- Phase 4: Remove legacy Application interfaces and Infrastructure concretes

Notes
- Target .NET 8 and EF Core 8
- Replace all `ConfigurationManager.ConnectionStrings[...]` with dependency-injected `IConfiguration`
- Keep Infrastructure strictly adapter/persistence; push business logic up to Application/Domain
