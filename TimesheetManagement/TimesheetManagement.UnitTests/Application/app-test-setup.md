# Sample Test Suite Structure for Application Layer

Based on the CQRS commands, queries, handlers, and validators implemented in `TimesheetManagement.Application`, here's a high-level test suite structure using xUnit (common for .NET 8). Focus on unit tests for handlers, validators, and business logic in isolation using mocks. Place tests in `TimesheetManagement.UnitTests` project, organized by namespace.

## Test Project Setup

**Framework:** xUnit with FluentAssertions for readable assertions, Moq for mocking.

**Unit Tests:** Test individual components (handlers, validators) in isolation using mocks.

**Directory Structure:**

```
TimesheetManagement.UnitTests/
├── Application/
│   ├── Common/
│   │   ├── DomainEventDispatcherTests.cs
│   │   └── ValidatorBaseTests.cs
│   ├── Identity/
│   │   ├── Commands/
│   │   │   ├── RegisterUser/
│   │   │   │   ├── RegisterUserHandlerTests.cs
│   │   │   │   └── RegisterUserValidatorTests.cs
│   │   │   ├── AssignRole/
│   │   │   │   ├── AssignRoleHandlerTests.cs
│   │   │   │   └── AssignRoleValidatorTests.cs
│   │   │   └── ChangePassword/
│   │   │       ├── ChangePasswordHandlerTests.cs
│   │   │       └── ChangePasswordValidatorTests.cs
│   │   └── Queries/
│   │       ├── GetUserById/
│   │       │   └── GetUserByIdHandlerTests.cs
│   │       └── GetUserByEmail/
│   │           └── GetUserByEmailHandlerTests.cs
│   ├── TimeTracking/
│   │   ├── Commands/
│   │   │   ├── CreateTimeSheet/
│   │   │   │   ├── CreateTimeSheetHandlerTests.cs
│   │   │   │   └── CreateTimeSheetValidatorTests.cs
│   │   │   ├── AddTimeEntry/
│   │   │   │   ├── AddTimeEntryHandlerTests.cs
│   │   │   │   └── AddTimeEntryValidatorTests.cs
│   │   │   ├── SubmitTimeSheet/
│   │   │   │   ├── SubmitTimeSheetHandlerTests.cs
│   │   │   │   └── SubmitTimeSheetValidatorTests.cs
│   │   │   ├── ApproveTimeSheet/
│   │   │   │   ├── ApproveTimeSheetHandlerTests.cs
│   │   │   │   └── ApproveTimeSheetValidatorTests.cs
│   │   │   ├── RejectTimeSheet/
│   │   │   │   ├── RejectTimeSheetHandlerTests.cs
│   │   │   │   └── RejectTimeSheetValidatorTests.cs
│   │   │   ├── RemoveTimeEntry/
│   │   │   │   └── RemoveTimeEntryHandlerTests.cs
│   │   │   └── EditAfterRejection/
│   │   │       └── EditAfterRejectionHandlerTests.cs
│   │   └── Queries/
│   │       ├── GetTimeSheetById/
│   │       │   └── GetTimeSheetByIdHandlerTests.cs
│   │       └── GetTimeSheetsForUser/
│   │           └── GetTimeSheetsForUserHandlerTests.cs
│   ├── Expenses/
│   │   ├── Commands/
│   │   │   ├── CreateExpenseReport/
│   │   │   │   ├── CreateExpenseReportHandlerTests.cs
│   │   │   │   └── CreateExpenseReportValidatorTests.cs
│   │   │   ├── AddExpenseItem/
│   │   │   │   ├── AddExpenseItemHandlerTests.cs
│   │   │   │   └── AddExpenseItemValidatorTests.cs
│   │   │   ├── SubmitExpenseReport/
│   │   │   │   ├── SubmitExpenseReportHandlerTests.cs
│   │   │   │   └── SubmitExpenseReportValidatorTests.cs
│   │   │   ├── ApproveExpenseReport/
│   │   │   │   ├── ApproveExpenseReportHandlerTests.cs
│   │   │   │   └── ApproveExpenseReportValidatorTests.cs
│   │   │   ├── RejectExpenseReport/
│   │   │   │   ├── RejectExpenseReportHandlerTests.cs
│   │   │   │   └── RejectExpenseReportValidatorTests.cs
│   │   │   ├── RemoveExpenseItem/
│   │   │   │   └── RemoveExpenseItemHandlerTests.cs
│   │   │   └── EditExpenseAfterRejection/
│   │   │       └── EditExpenseAfterRejectionHandlerTests.cs
│   │   └── Queries/
│   │       ├── GetExpenseReportById/
│   │       │   └── GetExpenseReportByIdHandlerTests.cs
│   │       └── GetExpenseReportsForUser/
│   │           └── GetExpenseReportsForUserHandlerTests.cs
│   ├── Projects/
│   │   ├── Commands/
│   │   │   ├── CreateProject/
│   │   │   │   ├── CreateProjectHandlerTests.cs
│   │   │   │   └── CreateProjectValidatorTests.cs
│   │   │   ├── ArchiveProject/
│   │   │   │   └── ArchiveProjectHandlerTests.cs
│   │   │   └── RestoreProject/
│   │   │       └── RestoreProjectHandlerTests.cs
│   │   └── Queries/
│   │       ├── GetAllProjects/
│   │       │   └── GetAllProjectsHandlerTests.cs
│   │       └── GetProjectByCode/
│       │   └── GetProjectByCodeHandlerTests.cs
│   └── Teams/
│       ├── Commands/
│       │   ├── CreateTeam/
│       │   │   └── CreateTeamHandlerTests.cs
│       │   ├── AddTeamMember/
│       │   │   └── AddTeamMemberHandlerTests.cs
│       │   ├── RemoveTeamMember/
│       │   │   └── RemoveTeamMemberHandlerTests.cs
│       │   ├── ArchiveTeam/
│       │   │   └── ArchiveTeamHandlerTests.cs
│       │   └── RestoreTeam/
│       │   └── RestoreTeamHandlerTests.cs
│       └── Queries/
│           ├── GetAllTeams/
│           │   └── GetAllTeamsHandlerTests.cs
│           └── GetTeamById/
│               └── GetTeamByIdHandlerTests.cs
└── TestHelpers/
    ├── ApplicationTestHelpers.cs  // Mocks, in-memory repos, unit of work setup
    └── TestData.cs  // Shared fixtures
```

## Key Test Examples

### Unit Tests

#### Identity/Commands/RegisterUser/RegisterUserHandlerTests.cs

- **Handle:** Creates user, assigns role, raises events, calls audit log.
- **Handle_InvalidData:** Throws validation exception.

#### Identity/Commands/RegisterUser/RegisterUserValidatorTests.cs

- **Validate:** Passes for valid email/password, fails for invalid.

#### TimeTracking/Commands/CreateTimeSheet/CreateTimeSheetHandlerTests.cs

- **Handle:** Creates time sheet, validates period, saves via unit of work.

#### TimeTracking/Queries/GetTimeSheetById/GetTimeSheetByIdHandlerTests.cs

- **Handle:** Retrieves time sheet, throws if not found.

## Testing Guidelines

- **Unit Tests:** Mock repositories, unit of work, user context. Test handler logic, validation, exceptions.
- **Naming Convention:** `[Handler]_[Scenario]_Should_[Expected]` or `[Feature]_[Scenario]_Should_[Expected]`.
- **Test Structure:** Follow Arrange-Act-Assert pattern. One behavior per test.
- **Fixtures:** Use `ApplicationTestHelpers.cs` for setup, `TestData.cs` for entities.
- **Coverage:** Aim for happy paths, error cases, authorization (via user context).

This structure covers application logic. Expand to API/End-to-End tests later.

## Running Tests

**Run Unit Tests:**

```bash
dotnet test --filter "Application.UnitTests"
