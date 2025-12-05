# Integration Test Suite Structure for Infrastructure Layer

Based on the Infrastructure components implemented in `TimesheetManagement.Infrastructure` (e.g., repositories, DbContext, UnitOfWork), here's a recommended test suite structure using xUnit (common for .NET 8). This guide implements a **hybrid testing strategy** using both in-memory databases (for speed) and Testcontainers (for production fidelity).

## Test Project Setup

**Framework:** xUnit with FluentAssertions for readable assertions, Testcontainers for real database integration, EF Core in-memory for fast feedback loops.

**Integration Tests:** Test Infrastructure components against both in-memory and real SQL Server databases to balance speed and accuracy. Use test database isolation per test class or suite.

**Testing Strategy:**
- **In-Memory Tests**: Fast, lightweight tests for basic CRUD operations and developer feedback during TDD.
- **SQL Server Tests (Testcontainers)**: Production-faithful tests that validate constraints, indexes, migrations, and provider-specific behaviors.

## Directory Structure

```
TimesheetManagement.IntegrationTests/
├── Repositories/
│   ├── InMemory/
│   │   ├── UserRepositoryInMemoryTests.cs
│   │   ├── RoleAssignmentRepositoryInMemoryTests.cs
│   │   ├── TimeSheetRepositoryInMemoryTests.cs
│   │   ├── ExpenseReportRepositoryInMemoryTests.cs
│   │   ├── ProjectRepositoryInMemoryTests.cs
│   │   └── TeamRepositoryInMemoryTests.cs
│   └── SqlServer/
│       ├── UserRepositorySqlServerTests.cs
│       ├── RoleAssignmentRepositorySqlServerTests.cs
│       ├── TimeSheetRepositorySqlServerTests.cs
│       ├── ExpenseReportRepositorySqlServerTests.cs
│       ├── ProjectRepositorySqlServerTests.cs
│       └── TeamRepositorySqlServerTests.cs
├── Persistence/
│   ├── InMemory/
│   │   ├── AppDbContextInMemoryTests.cs
│   │   └── UnitOfWorkInMemoryTests.cs
│   └── SqlServer/
│       ├── AppDbContextSqlServerTests.cs
│       ├── UnitOfWorkSqlServerTests.cs
│       └── MigrationTests.cs
├── Configurations/
│   ├── UserConfigTests.cs
│   ├── RoleAssignmentConfigTests.cs
│   ├── TimeSheetConfigTests.cs
│   ├── TimeEntryConfigTests.cs
│   ├── ExpenseReportConfigTests.cs
│   ├── ExpenseItemConfigTests.cs
│   ├── ProjectConfigTests.cs
│   └── TeamConfigTests.cs
└── TestHelpers/
    ├── InMemoryDatabaseFixture.cs    // Fast in-memory DB setup
    ├── SqlServerDatabaseFixture.cs   // Testcontainers SQL Server setup
    ├── TestDataSeeder.cs             // Seed test data
    └── IntegrationTestHelpers.cs     // Mocks for external services if any
```

## Key Test Examples

### Repository Integration Tests

#### InMemory Tests (Fast Feedback)

**Repositories/InMemory/TimeSheetRepositoryInMemoryTests.cs**
- **AddAndRetrieveTimeSheet_WithEntries:** Adds timesheet with entries, verifies persistence.
- **GetForUserAsync_ReturnsCorrectTimesheets:** Tests query filtering.
- **UpdateTimeSheet_ShouldPersistChanges:** Validates entity updates.

**Repositories/InMemory/ProjectRepositoryInMemoryTests.cs**
- **AddProject_ShouldPersist:** Basic CRUD validation.
- **GetByCodeAsync_Found:** Retrieves project by code.
- **ArchiveAndRestore:** Tests state changes.

#### SqlServer Tests (Production Fidelity)

**Repositories/SqlServer/ProjectRepositorySqlServerTests.cs**
- **AddProject_DuplicateCode_ShouldThrowConstraintException:** Validates unique constraint enforcement.
- **AddProject_CodeExists_VerifyUniqueIndex:** Ensures database index is properly configured.
- **ConcurrentUpdates_ShouldHandleOptimisticConcurrency:** Tests rowversion/concurrency tokens.

**Repositories/SqlServer/TimeSheetRepositorySqlServerTests.cs**
- **HasSubmittedForRangeAsync_OverlapExists:** Verifies complex query with owned types (DateRange).
- **CascadeDelete_RemovesEntries:** Tests FK cascade behavior.
- **IndexPerformance_OnUserIdAndStatus:** Validates query performance with indexes.

### Persistence Tests

#### InMemory Tests

**Persistence/InMemory/AppDbContextInMemoryTests.cs**
- **BasicCRUD_ShouldWork:** Validates DbContext can save and retrieve entities.
- **OwnedTypes_ShouldMap:** Tests value object mappings (DateRange, Money, Email).

**Persistence/InMemory/UnitOfWorkInMemoryTests.cs**
- **SaveChanges_CommitsTransaction:** Adds entities, verifies persistence.
- **MultipleRepositories_ShareSameContext:** Ensures UnitOfWork coordinates repos.

#### SqlServer Tests

**Persistence/SqlServer/AppDbContextSqlServerTests.cs**
- **MigrationsApplied_TablesCreated:** Ensures all migrations run without errors.
- **OwnedTypesMapped_CorrectColumns:** Verifies value objects create correct DB columns.
- **CascadeDeletes_ConfiguredCorrectly:** Tests FK relationships and delete behavior.

**Persistence/SqlServer/UnitOfWorkSqlServerTests.cs**
- **SaveChanges_CommitsTransaction:** Validates real DB transaction commit.
- **RollbackOnFailure_NoPartialCommits:** Simulates exception, verifies rollback.
- **DomainEventsDispatched_AfterCommit:** If implemented, verifies events published post-save.

**Persistence/SqlServer/MigrationTests.cs**
- **ApplyMigrations_ShouldSucceed:** Validates all migrations apply cleanly.
- **RollbackMigration_ShouldRevert:** Tests migration rollback scenarios.
- **SeedData_AfterMigration:** Verifies seed data scripts work.

### Configuration Tests

**Configurations/UserConfigTests.cs**
- **UserConfig_MapsEmailAsOwnedType:** Validates Email value object configuration.
- **UserConfig_CreatesUniqueIndexOnEmail:** Ensures unique constraint on Email column.
- **UserConfig_PasswordHashMappedCorrectly:** Tests PasswordHash mapping.

**Configurations/TimeSheetConfigTests.cs**
- **TimeSheetConfig_MapsDateRangeAsOwnedType:** Validates Period (DateRange) configuration.
- **TimeSheetConfig_CascadeDeletesEntries:** Ensures TimeEntry cascade delete on TimeSheet delete.

**Configurations/ProjectConfigTests.cs**
- **ProjectConfig_UniqueIndexOnCode:** Validates unique constraint on Code.
- **ProjectConfig_UniqueIndexOnName:** Validates unique constraint on Name.

## Testing Guidelines

### Test Categorization (Traits)
Use xUnit traits to categorize tests:
```csharp
[Trait("Category", "InMemory")]  // Fast, local development
[Trait("Category", "Integration")] // SQL Server, CI/CD
[Trait("Category", "Migration")]  // Migration-specific tests
```

### Database Setup
- **In-Memory**: Use `InMemoryDatabaseFixture` for fast, isolated tests. Each test class gets a fresh DB.
- **Testcontainers**: Use `SqlServerDatabaseFixture` for real SQL Server container. Slower but production-accurate.
- **Isolation**: Each fixture creates a new DB instance (in-memory uses unique name, container uses fresh schema).

### Test Data
- **Seed Minimal Data**: Use `TestDataSeeder.cs` for common entities (users, projects).
- **Test-Specific Data**: Create entities inline for unique scenarios.
- **Cleanup**: Fixtures handle cleanup via `IAsyncLifetime.DisposeAsync()`.

### Naming Convention
`[Component]_[Scenario]_Should_[Expected]`
- Examples: `AddProject_DuplicateCode_ShouldThrowConstraintException`, `GetTimeSheetById_NotFound_ShouldReturnNull`

### Test Structure
Follow **Arrange-Act-Assert (AAA)** pattern:
```csharp
[Fact]
public async Task AddProject_ValidData_ShouldPersist()
{
    // Arrange
    var repo = new ProjectRepository(_fixture.DbContext);
    var project = new Project("PROJ001", "Test Project", "Tech");

    // Act
    await repo.AddAsync(project);
    await _fixture.DbContext.SaveChangesAsync();

    // Assert
    var loaded = await repo.GetByCodeAsync("PROJ001");
    loaded.Should().NotBeNull();
    loaded!.Name.Should().Be("Test Project");
}
```

### Coverage Focus
- **CRUD**: Add, retrieve, update, delete operations.
- **Constraints**: Unique indexes, foreign keys, required fields.
- **Queries**: Complex filters, includes, projections.
- **Transactions**: Commit, rollback, isolation.
- **Edge Cases**: Concurrency, large datasets, boundary values.
- **Performance**: Basic perf checks for heavy queries (e.g., GetAll with 1000+ records).

## Recommended Tools and Setup

### NuGet Packages
```xml
<PackageReference Include="xunit" Version="2.5.3" />
<PackageReference Include="FluentAssertions" Version="8.8.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="8.0.22" />
<PackageReference Include="Testcontainers.MsSql" Version="3.9.0" />
<PackageReference Include="Respawn" Version="6.2.1" /> <!-- Optional for DB resets -->
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
```

### Fixture Setup Examples

**InMemoryDatabaseFixture.cs**
```csharp
public class InMemoryDatabaseFixture : IAsyncLifetime
{
    public AppDbContext DbContext { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        DbContext = new AppDbContext(options);
        await DbContext.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync() => await DbContext.DisposeAsync();
}
```

**SqlServerDatabaseFixture.cs**
```csharp
public class SqlServerDatabaseFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _container;
    public AppDbContext DbContext { get; private set; } = null!;

    public SqlServerDatabaseFixture()
    {
        _container = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("Strong!Passw0rd")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(_container.GetConnectionString())
            .Options;
        DbContext = new AppDbContext(options);
        await DbContext.Database.MigrateAsync(); // Apply migrations
    }

    public async Task DisposeAsync()
    {
        await DbContext.DisposeAsync();
        await _container.StopAsync();
    }
}
```

## Running Tests

### Run All Tests
```bash
dotnet test TimesheetManagement.IntegrationTests
```

### Run In-Memory Tests Only (Fast)
```bash
dotnet test TimesheetManagement.IntegrationTests --filter "Category=InMemory"
```

### Run SQL Server Tests Only (Thorough)
```bash
dotnet test TimesheetManagement.IntegrationTests --filter "Category=Integration"
```

### Run Specific Component
```bash
dotnet test TimesheetManagement.IntegrationTests --filter "Repositories"
dotnet test TimesheetManagement.IntegrationTests --filter "Persistence"
```

### Run Migration Tests
```bash
dotnet test TimesheetManagement.IntegrationTests --filter "Category=Migration"
```

## Recommendations and Rationale

### Hybrid Strategy Benefits
- **Speed**: In-memory tests run in milliseconds, ideal for TDD and local development.
- **Confidence**: SQL Server tests catch real DB issues (constraints, indexes, migrations).
- **Flexibility**: Run fast tests locally, thorough tests in CI/CD.

### When to Use Each
- **In-Memory**: 80% of tests (basic CRUD, happy paths, quick feedback).
- **SQL Server**: 20% of tests (constraints, migrations, complex queries, edge cases).

### CI/CD Pipeline Strategy
```yaml
# Example GitHub Actions workflow
- name: Run Fast Tests
  run: dotnet test --filter "Category=InMemory"
  
- name: Run Integration Tests
  run: dotnet test --filter "Category=Integration"
  if: github.event_name == 'push' && github.ref == 'refs/heads/main'
```

### Why Both Are Necessary
- **In-Memory Limitations**: Doesn't enforce constraints, missing provider-specific behavior, no real indexes.
- **Testcontainers Overhead**: Slower startup (~10-20s per container), requires Docker.
- **Best of Both**: Fast feedback + production accuracy.

### Scope Boundaries
- **Test Only Infrastructure**: Focus on repositories, DbContext, UnitOfWork, configurations.
- **Avoid Application Logic**: Business rules belong in `TimesheetManagement.UnitTests`.
- **No UI/API Tests Here**: Save for separate E2E test project.

### Alignment with app-target.md
- Validates DbContext configurations match specifications.
- Ensures repository implementations follow domain patterns.
- Tests UnitOfWork transaction handling.
- Verifies migrations create correct schema.
- Confirms indexes, cascades, and owned types work as designed.

### Expansion Path
1. **Phase 1** (Current): Repository and persistence tests.
2. **Phase 2**: Add configuration-specific tests for all entity types.
3. **Phase 3**: Performance benchmarks for critical queries.
4. **Phase 4**: API integration tests (full HTTP requests in separate project).

## Performance Considerations

### In-Memory Performance
- **Startup**: ~50ms per test class (fixture creation).
- **Test Execution**: ~10-100ms per test.
- **Best For**: Local development, PR checks.

### Testcontainers Performance
- **Startup**: ~10-20s per container (one-time per test run).
- **Test Execution**: ~100-500ms per test (real I/O).
- **Best For**: Pre-merge validation, nightly builds.

### Optimization Tips
- **Reuse Containers**: Use collection fixtures to share container across test classes.
- **Parallel Execution**: Configure xUnit for parallel test runs.
- **Selective Running**: Use filters to run only relevant tests during development.

This structure provides comprehensive coverage for Infrastructure reliability while balancing speed and accuracy. Update as components evolve and add new test categories as needed.
