# Sample Test Suite Structure for Domain Layer

Based on the DDD aggregates and value objects implemented in `TimesheetManagement.Domain`, here's a high-level test suite structure using xUnit (common for .NET 8). Focus on unit tests for invariants, behaviors, and domain events. Place tests in `TimesheetManagement.Tests` project, organized by namespace.

## Test Project Setup

**Framework:** xUnit with FluentAssertions for readable assertions.

**Directory Structure:**

```
TimesheetManagement.Tests/
├── Domain/
│   ├── Common/
│   │   ├── EntityTests.cs
│   │   └── ValueObjects/
│   │       ├── DateRangeTests.cs
│   │       ├── HoursWorkedTests.cs
│   │       └── MoneyTests.cs
│   ├── Identity/
│   │   ├── UserTests.cs
│   │   ├── RoleAssignmentTests.cs
│   │   └── ValueObjects/
│   │       ├── EmailTests.cs
│   │       └── PasswordHashTests.cs
│   ├── TimeTracking/
│   │   ├── TimeSheetTests.cs
│   │   └── TimeEntryTests.cs
│   ├── Expenses/
│   │   ├── ExpenseReportTests.cs
│   │   └── ExpenseItemTests.cs
│   ├── Projects/
│   │   └── ProjectTests.cs
│   ├── Teams/
│   │   └── TeamTests.cs
│   └── Audit/
│       └── AuditLogTests.cs
└── TestHelpers/
    └── TestData.cs  // Shared test data/fixtures
```

## Key Test Examples (Sample, Not Exhaustive)

### Common/EntityTests.cs
- Test base entity ID generation.

### Common/ValueObjects/DateRangeTests.cs
- **Constructor:** Valid dates, throws on invalid (To < From).
- **Properties:** From/To access, TotalDays calculation.
- **Methods:** Contains(DateOnly) true/false.

### Identity/UserTests.cs
- **Constructor:** Requires username/email, throws on invalid.
- **Methods:** ChangePassword validates hash.

### TimeTracking/TimeSheetTests.cs
- **Constructor:** Sets period, status Draft.
- **Invariants:** AddEntry throws if outside period or not Draft.
- **Submit:** Requires entries, raises `TimeSheetSubmittedEvent`, changes status.
- **Approve/Reject:** Only from Submitted, raises events.

### Expenses/ExpenseReportTests.cs
- Similar to TimeSheet: Draft edits, Submit/Approve/Reject with events.
- **AddItem:** Validates period, raises no event.

### Teams/TeamTests.cs
- **AddMember:** Prevents duplicates, throws if archived.
- **Archive:** Clears members? (If invariant).

### Audit/AuditLogTests.cs
- **Constructor:** Sets action, entityType, entityId, userId, details, and timestamp.

### Identity/ValueObjects/EmailTests.cs
- **Constructor:** Valid email, throws on null/empty or invalid format.
- **ToString:** Returns Value.

### Identity/ValueObjects/PasswordHashTests.cs
- **Constructor:** Sets Value.
- **Empty:** Returns empty string.

## Guidelines

- **Naming:** `[Aggregate]_[Scenario]_Should_[Expected]`
- **Arrange-Act-Assert:** Test one behavior per test.
- **Events:** Assert domain events are raised (e.g., `aggregate.DomainEvents.Should().ContainSingle(e => e is TimeSheetSubmittedEvent)`).
- **Coverage:** Aim for invariants, state transitions, exceptions.
- **Fixtures:** Use `TestData.cs` for sample entities (e.g., valid User, DateRange).

This structure covers core domain logic. Expand to Application/Integration tests later (e.g., handlers, repositories with in-memory DB). 

**Run with:** `dotnet test`

**Alternatively, Run with:** `cd D:\Workspace\DemoProjectEx\TimesheetManagement && dotnet test --filter "Common"`**Alternatively, Run with:** `cd D:\Workspace\DemoProjectEx\TimesheetManagement && dotnet test --filter "Common"`