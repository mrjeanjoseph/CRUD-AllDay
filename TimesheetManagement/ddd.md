Domain-Driven Design (DDD) target for TimesheetManagement (.NET 8, Razor Pages)

Goals
- Isolate business rules in a pure Domain layer
- Keep Web (Razor Pages) thin and infrastructure replaceable
- Improve testability and maintainability via clear boundaries

Target solution structure (Clean Architecture flavor)
- Domain (pure)
  - Aggregates, entities, value objects
  - Domain events and handlers (interfaces only)
  - Repository interfaces (e.g., `ITimeSheetRepository`, `IExpenseReportRepository`, `IUserRepository`)
  - Domain services (stateless, business-centric behavior)
- Application
  - Use cases (commands/queries) and their handlers
  - DTOs/ViewModels specific to use cases
  - Validators (e.g., FluentValidation)
  - Mapping profiles (e.g., AutoMapper/Mapster)
  - Orchestration of domain behavior and transactions
- Infrastructure
  - EF Core DbContext, entity configurations, migrations
  - Repository implementations for domain interfaces
  - External services (email, file storage, queues) adapters
  - Dapper usage, if any, hidden behind interfaces
- Web (Razor Pages)
  - PageModels depend only on Application layer
  - Authentication/Authorization, request/response shaping
  - No data access or domain logic

Proposed bounded contexts and aggregates
- Identity
  - Aggregate roots: `User`, `Role`
  - Value objects: `Email`, `PasswordHash`
- TimeTracking
  - Aggregate root: `TimeSheet` (owns `TimeEntry` collection)
  - States: Draft, Submitted, Approved, Rejected
  - Events: `TimeSheetSubmitted`, `TimeSheetApproved`, `TimeSheetRejected`
  - Value objects: `HoursWorked`, `DateRange`
- Expenses
  - Aggregate root: `ExpenseReport` (owns `ExpenseItem` collection)
  - Value objects: `Money`, `Currency`
  - Events: `ExpenseSubmitted`, `ExpenseApproved`, `ExpenseRejected`
- Projects/Teams
  - Aggregate roots: `Project`, `Team`, `Membership`

Key DDD building blocks
- Entities and Aggregates enforce invariants via behavior methods (no anemic models)
- Value objects are immutable and equality-based (e.g., `Money`, `Email`)
- Domain events publish important state changes; handlers coordinate side effects
- Repositories return aggregates; persistence concerns are hidden
- Unit of Work = EF Core DbContext per use case boundary

Application layer patterns
- Optional CQRS: separate commands (state changes) and queries (reads)
- Validation via FluentValidation
- Mapping from aggregates to DTOs via AutoMapper/Mapster
- Transactional boundaries: one handler = one transaction

Infrastructure notes
- EF Core 8 (target .NET 8)
- Use configurations (Fluent API) to map aggregates without leaking persistence to Domain
- Migrations live here
- Keep Dapper usage behind repository interfaces where necessary

Web (Razor Pages) guidelines
- Inject and call Application use case handlers from PageModels
- Keep PageModels free of data access and domain logic
- Use `@section Scripts` for page JS; layout loads global scripts at end of body

Incremental migration plan
1) Create new projects
   - Domain, Application, Infrastructure; keep Web as-is initially
2) Define core aggregates and value objects
   - Start with `TimeSheet` and `ExpenseReport`; add behavior methods enforcing invariants
3) Move interfaces
   - Move repository contracts from `TimesheetManagement.Application` into Domain
   - Mark the current `*.Infrastructure` as Infrastructure and adapt or migrate into the new Infrastructure project
4) Introduce EF Core DbContext in Infrastructure
   - Register via DI in Web `Program.cs`
   - Replace `ConfigurationManager` with `IConfiguration.GetConnectionString(...)`
5) Implement repositories in Infrastructure
   - Remove `new DatabaseContext()` from code paths; use DI for DbContext
6) Add Application use cases
   - Examples: SubmitTimeSheet, ApproveTimeSheet, CreateExpense, ApproveExpense, RegisterUser, ChangePassword
7) Wire Web to Application
   - PageModels call Application handlers; map inputs/outputs
8) Add domain events where needed
   - Raise events from aggregate methods and handle in Application
9) Retire legacy paths
   - Remove direct data access from controllers/pages; delete unused concrete classes

Data and configuration
- Connection strings in `appsettings.json` under `ConnectionStrings`
- Register DbContext and repositories with DI in `Program.cs`
- Avoid `ConfigurationManager.ConnectionStrings[...]` patterns

Testing strategy
- Domain: pure unit tests for aggregates and value objects
- Application: handler tests with mocked repositories
- Infrastructure: EF Core integration tests against a test database or SQLite in-memory
- Web: minimal PageModel tests where needed

Risks and mitigations
- Mixed legacy and new paths: use adapters/anti-corruption layer temporarily
- Schema constraints vs. aggregates: refine mappings and DB constraints iteratively
- Performance regressions: profile critical queries; allow read-side Dapper behind interfaces when needed

Definition of done per feature
- Domain behavior covered by unit tests
- Application handler implemented and validated
- Repository methods implemented and integration-tested
- Web wired to handler; manual walk-through
- Docs updated (this file)

Naming and folders (suggested)
- Domain: `Aggregates/TimeTracking/TimeSheet.cs`, `ValueObjects/Money.cs`, `Events/TimeSheetSubmitted.cs`, `Repositories/ITimeSheetRepository.cs`
- Application: `TimeTracking/Commands/SubmitTimeSheet`, `Expenses/Queries/GetExpenseReport` etc.
- Infrastructure: `Persistence/AppDbContext.cs`, `Configurations/TimeSheetConfig.cs`, `Repositories/TimeSheetRepository.cs`
- Web: PageModels grouped by feature folders
