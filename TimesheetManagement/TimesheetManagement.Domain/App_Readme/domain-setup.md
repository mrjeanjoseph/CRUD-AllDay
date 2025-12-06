Domain Layer Readme (TimesheetManagement, .NET 8)

Purpose
- Hold core business rules and invariants in pure C# (no framework dependencies)
- Provide the ubiquitous language via aggregates, entities, value objects, and domain events
- Expose repository interfaces only; implementations live in Infrastructure

What belongs here
- Aggregates and entities with behavior (no anemic models)
- Value objects (immutable, equality by value)
- Domain events (records implementing IDomainEvent) raised by aggregates
- Repository interfaces for aggregates
- Stateless domain services (if a behavior does not fit a single aggregate)

What does NOT belong here
- EF Core attributes/configurations, Dapper code, connection strings
- Web, UI, or validation attributes
- DTOs/ViewModels for pages or exports

Current building blocks
- Common
  - Entity (base with Id)
  - IDomainEvent (marker interface)
  - ValueObjects/DateRange
- Identity
  - Aggregates: User, RoleAssignment, Role (enum)
  - Value objects: Email, PasswordHash
  - Events: RoleAssignedEvent, RoleAssignmentDeactivatedEvent
  - Repositories: IUserRepository, IRoleAssignmentRepository
- TimeTracking
  - Aggregates: TimeSheet (owns TimeEntry collection)
  - Value objects: HoursWorked, DateRange (used via Common)
  - Events: TimeSheetSubmittedEvent, TimeSheetApprovedEvent, TimeSheetRejectedEvent
  - Repositories: ITimeSheetRepository
- Expenses
  - Aggregates: ExpenseReport (owns ExpenseItem collection)
  - Value objects: Money
  - Events: ExpenseSubmittedEvent, ExpenseApprovedEvent, ExpenseRejectedEvent
  - Repositories: IExpenseReportRepository
- Projects/Teams
  - Aggregates: Project, Team
  - Repositories: IProjectRepository, ITeamRepository

Aggregate invariants (high level)
- User
  - Username required; Email required; password changes via ChangePassword
- RoleAssignment
  - AdminId != UserId; CreatedBy required; activate/deactivate transitions
- TimeSheet
  - Period is a DateRange; only Draft can be edited; entries must fall within Period
  - Submit requires at least one entry; Approve/Reject only from Submitted
- TimeEntry
  - Hours is HoursWorked (0 < hours ? 24)
- ExpenseReport
  - Period is a DateRange; only Draft can be edited; Submit requires at least one item
- ExpenseItem
  - Category required; Amount is Money (Amount ? 0)
- Project
  - Code/Name/Industry required; archive/restore toggles
- Team
  - Name required; cannot modify members while archived

Domain events
- Aggregates collect IDomainEvent instances internally and expose them via DomainEvents
- Application layer is responsible for dispatching/handling (e.g., notifications, auditing)
- Aggregates provide ClearDomainEvents() after persistence

Repositories (interfaces)
- Return whole aggregates; persistence is an Infrastructure concern
- Typical methods: GetAsync, AddAsync, UpdateAsync, existence/uniqueness checks as needed
- One application handler = one transaction (Unit of Work optional)

Folder structure (recommended)
- Common/
  - Entity.cs
  - IDomainEvent.cs
  - ValueObjects/DateRange.cs
- Identity/
  - User.cs, RoleAssignment.cs, Role.cs
  - Email.cs, PasswordHash.cs
  - Events/*.cs
  - Repositories/*.cs
- TimeTracking/
  - TimeSheet.cs, TimeEntry.cs
  - Events/*.cs, Repositories/*.cs, ValueObjects/HoursWorked.cs
- Expenses/
  - ExpenseReport.cs, ExpenseItem.cs, ValueObjects/Money.cs
  - Events/*.cs, Repositories/*.cs
- Projects/
  - Project.cs, Repositories/*.cs
- Teams/
  - Team.cs, Repositories/*.cs

Coding guidelines
- Use init/private setters and constructors to enforce valid states
- Throw ArgumentException/InvalidOperationException when invariants are violated
- Value objects are immutable and provide strong typing (e.g., Money, HoursWorked)
- Use DateOnly and DateTime.UtcNow where appropriate for dates
- Do not use data annotations; keep domain pure

Legacy model migration
- Legacy POCOs/ViewModels (e.g., TimeSheetMaster, TimeSheetDetails, ProjectMaster, ExpenseModel, NotificationsTB, Registration, Documents, etc.) are marked as reviewed
- They remain temporarily for compatibility and will be retired after Application/Web layers switch to aggregates
- Mapping for legacy DB tables will be handled in Infrastructure (anti-corruption/adapters)

Testing guidance
- Unit-test aggregates and value objects (state transitions, invariants, equality)
- Assert domain events are raised for key transitions (submit/approve/reject, role assignment)

Definition of done for domain changes
- Behavior encapsulated inside aggregates or domain services
- Invariants enforced; value objects introduced where applicable
- Domain events raised for important state changes
- Repository interfaces expose necessary operations
- Unit tests written for new domain behavior

Notes
- Target: .NET 8, C# 12
- Keep this layer free of framework concerns; Infrastructure will handle persistence and event dispatching
