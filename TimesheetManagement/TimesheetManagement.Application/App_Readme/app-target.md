Application Layer Readme (TimesheetManagement, .NET 8)

Purpose
- Implement use cases (commands/queries) orchestrating domain behavior
- Depend only on Domain layer abstractions (repositories, aggregates, value objects)
- Keep Web and Infrastructure thin (no data access or domain logic here)

Guiding principles
- One handler = one use case = one transaction
- Validate inputs before calling domain behavior
- Keep handlers small, delegating to Domain aggregates for rules
- Return DTOs from queries; return IDs or simple results from commands

Recommended folder structure
- Common/
  - Abstractions/ (ICommand, IQuery, IHandler, IUnitOfWork [optional])
  - Behaviors/ (pipeline behaviors if added later)
  - Mapping/ (mapping profiles if using AutoMapper/Mapster)
  - Validation/ (FluentValidation validators)
- Identity/
  - Commands/ RegisterUser, AssignRole, ChangePassword
  - Queries/ GetUserByEmail, GetUserById
- TimeTracking/
  - Commands/ CreateTimeSheet, AddTimeEntry, RemoveTimeEntry, SubmitTimeSheet, ApproveTimeSheet, RejectTimeSheet, EditAfterRejection
  - Queries/ GetTimeSheetsForUser, GetTimeSheetById
- Expenses/
  - Commands/ CreateExpenseReport, AddExpenseItem, RemoveExpenseItem, SubmitExpense, ApproveExpense, RejectExpense, EditAfterRejection
  - Queries/ GetExpenseReportsForUser, GetExpenseReportById
- Projects/
  - Commands/ CreateProject, ArchiveProject, RestoreProject
  - Queries/ GetAllProjects, GetProjectByCode
- Teams/
  - Commands/ CreateTeam, AddMember, RemoveMember, ArchiveTeam, RestoreTeam
  - Queries/ GetAllTeams, GetTeamById

Dependencies
- References Domain layer only.
- Handlers depend on Domain repositories:
  - Identity: IUserRepository, IRoleAssignmentRepository
  - TimeTracking: ITimeSheetRepository
  - Expenses: IExpenseReportRepository
  - Projects: IProjectRepository
  - Teams: ITeamRepository

Validation
- Prefer FluentValidation for request DTOs (optional dependency). Examples:
  - RegisterUser: Username not empty; Email format; Email unique via IUserRepository.ExistsAsync
  - CreateTimeSheet: Date range valid; no overlapping submitted sheets via ITimeSheetRepository.HasSubmittedForRangeAsync
  - CreateProject: Unique code and name via IProjectRepository
- Domain invariants remain enforced inside aggregates (HoursWorked range, DateRange checks, status transitions, etc.).

Transactions
- Expose IUnitOfWork (optional) to commit after handler success.
- If not using IUnitOfWork, ensure Infra repositories save changes atomically per handler.

Mapping
- Use lightweight manual mapping or add AutoMapper/Mapster (optional) for query DTOs.

Command/Query shapes (suggested)
- Commands: immutable request records with handler classes returning Result/Id
- Queries: immutable request records with handler classes returning DTOs or paged lists

Example use cases (high level)
- Identity.RegisterUser
  - Validate username/email
  - Check email uniqueness (IUserRepository)
  - Create User aggregate (hashing performed outside domain, pass PasswordHash value object)
  - Save via IUserRepository
- Identity.AssignRole
  - Option A: set User.Role
  - Option B: create RoleAssignment aggregate (AdminId -> UserId)
  - Save via IRoleAssignmentRepository
- TimeTracking.CreateTimeSheet
  - Validate range and ensure no overlap (repository check)
  - new TimeSheet(userId, from, to)
  - Save via ITimeSheetRepository
- TimeTracking.SubmitTimeSheet
  - Load aggregate, call Submit(), persist and publish domain events (Application handles events if needed)
- Expenses.CreateExpenseReport / SubmitExpense
  - Same pattern as TimeTracking; validate overlap and totals if needed
- Projects.CreateProject
  - Validate unique Code and Name
  - new Project(code, name, industry)
  - Save via IProjectRepository

Replacing legacy Application interfaces
- IProject, IAssignRoles, ITimeSheet, IExpense, IUsers, ILogin, INotification, IAudit, IDocument, IExpenseExport, ITimeSheetExport, IRoles
- Replace with commands/queries. Keep them temporarily for compatibility and migrate call sites to new handlers.

Error handling
- Fail fast for validation errors (return error DTOs or throw custom exceptions that Web converts to friendly messages)
- Keep exceptions domain-meaningful (e.g., InvalidOperationException for invalid transitions)

Authorization
- Handlers receive current user context (e.g., IUserContext) injected from Web; enforce role checks here.

Caching and performance
- Apply caching on query handlers where helpful (e.g., projects list). Keep it optional and behind an abstraction.

Testing
- Unit test handlers using in-memory/mocked repositories
- Verify domain events raised by aggregates are handled if Application subscribes to them

Migration steps for current codebase
1) Introduce Common abstractions and base handler patterns
2) Add Identity.RegisterUser, TimeTracking.CreateTimeSheet, Expenses.CreateExpenseReport as first commands
3) Add essential queries: GetTimeSheetsForUser, GetExpenseReportsForUser, GetAllProjects
4) Wire Web PageModels to these handlers
5) Implement Infra repositories to satisfy Domain interfaces
6) Retire direct usages of legacy Application interfaces progressively

Definition of done per use case
- Request/response DTO defined and validated
- Handler implemented using Domain repositories and aggregates
- Transaction committed and domain events dispatched (if applicable)
- Tests added (unit), Web wired
- Docs updated (this file)

Notes
- Avoid adding libraries unless the benefit is clear. FluentValidation and a mapping library are optional.
- Keep Application layer free of EF/Dapper and any UI concerns.
