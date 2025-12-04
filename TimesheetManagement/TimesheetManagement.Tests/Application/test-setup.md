# Sample Test Suite Structure for Application Layer

Based on the CQRS commands, queries, handlers, and validators implemented in `TimesheetManagement.Application`, here's a high-level test suite structure using xUnit (common for .NET 8). Focus on unit tests for handlers, validators, and business logic, and integration tests for end-to-end scenarios with in-memory repositories. Place tests in `TimesheetManagement.Tests` project, organized by namespace.

## Test Project Setup

**Framework:** xUnit with FluentAssertions for readable assertions, Moq for mocking.

**Unit Tests:** Test individual components (handlers, validators) in isolation using mocks.

**Integration Tests:** Test workflows with in-memory repositories and unit of work to verify interactions.

**Directory Structure:**

```
TimesheetManagement.Tests/
├── Application/
│   ├── UnitTests/
│   │   ├── Common/
│   │   │   ├── DomainEventDispatcherTests.cs
│   │   │   └── ValidatorBaseTests.cs
│   │   ├── Identity/
│   │   │   ├── Commands/
│   │   │   │   ├── RegisterUser/
│   │   │   │   │   ├── RegisterUserHandlerTests.cs
│   │   │   │   │   └── RegisterUserValidatorTests.cs
│   │   │   │   ├── AssignRole/
│   │   │   │   │   ├── AssignRoleHandlerTests.cs
│   │   │   │   │   └── AssignRoleValidatorTests.cs
│   │   │   │   └── ChangePassword/
│   │   │   │       ├── ChangePasswordHandlerTests.cs
│   │   │   │       └── ChangePasswordValidatorTests.cs
│   │   │   └── Queries/
│   │   │       ├── GetUserById/
│   │   │       │   └── GetUserByIdHandlerTests.cs
│   │   │       └── GetUserByEmail/
│   │   │           └── GetUserByEmailHandlerTests.cs
│   │   ├── TimeTracking/
│   │   │   ├── Commands/
│   │   │   │   ├── CreateTimeSheet/
│   │   │   │   │   ├── CreateTimeSheetHandlerTests.cs
│   │   │   │   │   └── CreateTimeSheetValidatorTests.cs
│   │   │   │   ├── AddTimeEntry/
│   │   │   │   │   ├── AddTimeEntryHandlerTests.cs
│   │   │   │   │   └── AddTimeEntryValidatorTests.cs
│   │   │   │   ├── SubmitTimeSheet/
│   │   │   │   │   ├── SubmitTimeSheetHandlerTests.cs
│   │   │   │   │   └── SubmitTimeSheetValidatorTests.cs
│   │   │   │   ├── ApproveTimeSheet/
│   │   │   │   │   ├── ApproveTimeSheetHandlerTests.cs
│   │   │   │   │   └── ApproveTimeSheetValidatorTests.cs
│   │   │   │   ├── RejectTimeSheet/
│   │   │   │   │   ├── RejectTimeSheetHandlerTests.cs
│   │   │   │   │   └── RejectTimeSheetValidatorTests.cs
│   │   │   │   ├── RemoveTimeEntry/
│   │   │   │   │   └── RemoveTimeEntryHandlerTests.cs
│   │   │   │   └── EditAfterRejection/
│   │   │   │       └── EditAfterRejectionHandlerTests.cs
│   │   │   └── Queries/
│   │   │       ├── GetTimeSheetById/
│   │   │       │   └── GetTimeSheetByIdHandlerTests.cs
│   │   │       └── GetTimeSheetsForUser/
│   │   │           └── GetTimeSheetsForUserHandlerTests.cs
│   │   ├── Expenses/
│   │   │   ├── Commands/
│   │   │   │   ├── CreateExpenseReport/
│   │   │   │   │   ├── CreateExpenseReportHandlerTests.cs
│   │   │   │   │   └── CreateExpenseReportValidatorTests.cs
│   │   │   │   ├── AddExpenseItem/
│   │   │   │   │   ├── AddExpenseItemHandlerTests.cs
│   │   │   │   │   └── AddExpenseItemValidatorTests.cs
│   │   │   │   ├── SubmitExpenseReport/
│   │   │   │   │   ├── SubmitExpenseReportHandlerTests.cs
│   │   │   │   │   └── SubmitExpenseReportValidatorTests.cs
│   │   │   │   ├── ApproveExpenseReport/
│   │   │   │   │   ├── ApproveExpenseReportHandlerTests.cs
│   │   │   │   │   └── ApproveExpenseReportValidatorTests.cs
│   │   │   │   ├── RejectExpenseReport/
│   │   │   │   │   ├── RejectExpenseReportHandlerTests.cs
│   │   │   │   │   └── RejectExpenseReportValidatorTests.cs
│   │   │   │   ├── RemoveExpenseItem/
│   │   │   │   │   └── RemoveExpenseItemHandlerTests.cs
│   │   │   │   └── EditExpenseAfterRejection/
│   │   │   │       └── EditExpenseAfterRejectionHandlerTests.cs
│   │   │   └── Queries/
│   │   │       ├── GetExpenseReportById/
│   │   │       │   └── GetExpenseReportByIdHandlerTests.cs
│   │   │       └── GetExpenseReportsForUser/
│   │   │           └── GetExpenseReportsForUserHandlerTests.cs
│   │   ├── Projects/
│   │   │   ├── Commands/
│   │   │   │   ├── CreateProject/
│   │   │   │   │   ├── CreateProjectHandlerTests.cs
│   │   │   │   │   └── CreateProjectValidatorTests.cs
│   │   │   │   ├── ArchiveProject/
│   │   │   │   │   └── ArchiveProjectHandlerTests.cs
│   │   │   │   └── RestoreProject/
│   │   │   │       └── RestoreProjectHandlerTests.cs
│   │   │   └── Queries/
│   │   │       ├── GetAllProjects/
│   │   │       │   └── GetAllProjectsHandlerTests.cs
│   │   │       └── GetProjectByCode/
│   │   │           └── GetProjectByCodeHandlerTests.cs
│   │   └── Teams/
│   │       ├── Commands/
│   │       │   ├── CreateTeam/
│   │       │   │   └── CreateTeamHandlerTests.cs
│   │       │   ├── AddTeamMember/
│   │       │   │   └── AddTeamMemberHandlerTests.cs
│   │       │   ├── RemoveTeamMember/
│   │       │   │   └── RemoveTeamMemberHandlerTests.cs
│   │       │   ├── ArchiveTeam/
│   │       │   │   └── ArchiveTeamHandlerTests.cs
│   │       │   └── RestoreTeam/
│   │       │       └── RestoreTeamHandlerTests.cs
│   │       └── Queries/
│   │           ├── GetAllTeams/
│   │           │   └── GetAllTeamsHandlerTests.cs
│   │           └── GetTeamById/
│   │               └── GetTeamByIdHandlerTests.cs
│   └── IntegrationTests/
│       ├── Identity/
│       │   ├── RegisterUserIntegrationTests.cs
│       │   ├── AssignRoleIntegrationTests.cs
│       │   └── ChangePasswordIntegrationTests.cs
│       ├── TimeTracking/
│       │   ├── CreateTimeSheetIntegrationTests.cs
│       │   ├── SubmitTimeSheetIntegrationTests.cs
│       │   └── ApproveTimeSheetIntegrationTests.cs
│       ├── Expenses/
│       │   ├── CreateExpenseReportIntegrationTests.cs
│       │   ├── SubmitExpenseReportIntegrationTests.cs
│       │   └── ApproveExpenseReportIntegrationTests.cs
│       ├── Projects/
│       │   ├── CreateProjectIntegrationTests.cs
│       │   └── ArchiveProjectIntegrationTests.cs
│       └── Teams/
│           ├── CreateTeamIntegrationTests.cs
│           └── AddTeamMemberIntegrationTests.cs
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

### Integration Tests

#### Identity/RegisterUserIntegrationTests.cs

- **RegisterUser_Success:** Registers user, verifies in repo, events dispatched.
- **RegisterUser_DuplicateEmail:** Throws exception.

#### TimeTracking/CreateTimeSheetIntegrationTests.cs

- **CreateTimeSheet_Valid:** Creates and saves time sheet, verifies state.

## Testing Guidelines

- **Unit Tests:** Mock repositories, unit of work, user context. Test handler logic, validation, exceptions.
- **Integration Tests:** Use in-memory repositories (e.g., EF Core in-memory), real unit of work. Test full workflows, data persistence, event dispatching.
- **Naming Convention:** `[Handler]_[Scenario]_Should_[Expected]` or `[Feature]_[Scenario]_Should_[Expected]`.
- **Test Structure:** Follow Arrange-Act-Assert pattern. One behavior per test.
- **Fixtures:** Use `ApplicationTestHelpers.cs` for setup, `TestData.cs` for entities.
- **Coverage:** Aim for happy paths, error cases, authorization (via user context).

This structure covers application logic. Expand to API/End-to-End tests later.

## Running Tests

**Run Unit Tests:**

```bash
dotnet test --filter "Application.UnitTests"
```

**Run Integration Tests:**

```bash
dotnet test --filter "Application.IntegrationTests"
```
