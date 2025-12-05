# Integration Test Suite Structure for Infrastructure Layer

Based on the Infrastructure components implemented in `TimesheetManagement.Infrastructure` (e.g., repositories, DbContext, UnitOfWork), here's a recommended test suite structure using xUnit (common for .NET 8). Focus on integration tests that validate persistence, queries, and external interactions with a real or simulated database. Place tests in `TimesheetManagement.IntegrationTests` project, organized by component.

## Test Project Setup

**Framework:** xUnit with FluentAssertions for readable assertions, Testcontainers (or Respawn) for database isolation, EF Core in-memory or SQLite for lightweight tests.

**Integration Tests:** Test Infrastructure components against a real database (e.g., SQL Server via Testcontainers) to verify mappings, queries, transactions, and performance. Use a test database per test class or suite to ensure isolation.

**Directory Structure:**

```
TimesheetManagement.IntegrationTests/
├── Repositories/
│   ├── UserRepositoryTests.cs
│   ├── RoleAssignmentRepositoryTests.cs
│   ├── TimeSheetRepositoryTests.cs
│   ├── ExpenseReportRepositoryTests.cs
│   ├── ProjectRepositoryTests.cs
│   └── TeamRepositoryTests.cs
├── Persistence/
│   ├── AppDbContextTests.cs  // Test migrations, configurations, and basic CRUD
│   └── UnitOfWorkTests.cs    // Test transaction handling and event dispatching
├── Configurations/
│   ├── UserConfigTests.cs
│   ├── TimeSheetConfigTests.cs
│   └── (Other config tests as needed)
└── TestHelpers/
    ├── DatabaseFixture.cs    // Shared DB setup/teardown
    ├── TestDataSeeder.cs     // Seed test data
    └── IntegrationTestHelpers.cs  // Mocks for external services if any
```

## Key Test Examples

### Repository Integration Tests

#### Repositories/TimeSheetRepositoryTests.cs

- **AddAndRetrieveTimeSheet_WithEntries:** Adds a timesheet with entries, commits via UnitOfWork, retrieves and verifies data integrity (e.g., cascade deletes, includes).
- **HasSubmittedForRangeAsync_OverlapExists:** Seeds overlapping timesheets, verifies query logic and indexes.
- **ConcurrencyHandling:** Tests optimistic concurrency with rowversion (if implemented).

#### Repositories/ProjectRepositoryTests.cs

- **AddProject_CodeExists:** Adds project, checks uniqueness constraint throws exception.
- **GetByCodeAsync_Found:** Retrieves project by code, verifies all properties.
- **ArchiveAndRestore:** Tests state changes and updates.

#### Persistence/AppDbContextTests.cs

- **MigrationsApplied:** Ensures all migrations run without errors, tables created with correct schemas.
- **OwnedTypesMapped:** Verifies value objects (e.g., DateRange, Money) map correctly.
- **CascadeDeletes:** Tests FK relationships prevent or allow deletes as configured.

#### Persistence/UnitOfWorkTests.cs

- **SaveChanges_CommitsTransaction:** Adds entities, calls SaveChanges, verifies persistence.
- **RollbackOnFailure:** Simulates failure, ensures no partial commits.
- **DomainEventsDispatched:** If implemented, verifies events are published post-commit.

## Testing Guidelines

- **Database Setup:** Use Testcontainers for a real SQL Server container per test run. Alternatively, use EF in-memory for speed, but prefer real DB for accuracy (e.g., constraints, indexes).
- **Isolation:** Reset DB state per test (e.g., via Respawn or custom scripts). Avoid shared state.
- **Test Data:** Seed minimal data using `TestDataSeeder.cs`. Use factories for entities.
- **Naming Convention:** `[Component]_[Scenario]_Should_[Expected]`.
- **Test Structure:** Arrange (setup DB/data), Act (execute operation), Assert (verify DB state/results).
- **Coverage:** Focus on CRUD, constraints, queries, transactions. Include edge cases like concurrency, large datasets.
- **Performance:** Add basic perf tests for heavy queries (e.g., GetAll with 1000+ records).

## Recommended Tools and Setup

- **Testcontainers:** For real DB isolation. Add NuGet `Testcontainers.SqlServer` and configure in `DatabaseFixture.cs`.
- **Respawn:** For fast DB resets. NuGet `Respawn`.
- **EF Core Tools:** Run migrations in tests if needed.
- **Connection String:** Use a test-specific string (e.g., local SQL instance or container).

## Running Tests

**Run All Integration Tests:**

```bash
dotnet test TimesheetManagement.IntegrationTests --filter "IntegrationTests"
```

**Run Specific Repository Tests:**

```bash
dotnet test TimesheetManagement.IntegrationTests --filter "Repositories"
```

## Recommendations and Rationale

- **Why Integration for Infrastructure?** Infrastructure components (e.g., repositories) depend on external systems (DB). Unit tests with mocks don't validate real mappings/queries. Integration tests ensure correctness in production-like environments.
- **Database Choice:** Prefer Testcontainers for realism (tests real SQL Server behavior). In-memory EF is faster but misses provider-specific issues (e.g., DateOnly conversions).
- **Scope:** Test only Infrastructure; avoid Application logic (that's for UnitTests). Focus on data persistence, not business rules.
- **CI/CD:** These tests are slower; run them in a separate pipeline stage. Use parallel execution if possible.
- **Expansion:** Add API integration tests later (e.g., full HTTP requests to TimesheetManagement.API).
- **Alignment with app-target.md:** Tests validate DbContext configs, repository implementations, UnitOfWork, and migrations as outlined. Ensures indexes, cascades, and mappings work as specified.

This structure provides robust coverage for Infrastructure reliability. Update as components evolve.
