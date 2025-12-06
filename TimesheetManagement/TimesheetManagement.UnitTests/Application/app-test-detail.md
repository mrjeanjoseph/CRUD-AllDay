### Difference Between Unit Tests and Integration Tests

In software testing, especially for layered architectures like your TimesheetManagement application (a DDD-based .NET 8 system with Domain, Application, Infrastructure, and API layers), unit tests and integration tests serve distinct purposes. They complement each other but target different scopes, speeds, and types of issues. Below, I'll explain the key differences, when to use each, and how they apply specifically to your app. This is based on best practices for CQRS, domain-driven design, and .NET testing frameworks like xUnit.

#### 1. **Core Differences**
   - **Scope and Isolation**:
     - **Unit Tests**: Focus on testing a single "unit" of code in isolation. A unit is typically a method, class, or small component (e.g., a domain entity, a command handler, or a validator). Dependencies (e.g., repositories, external services) are mocked or stubbed to eliminate external influences. This ensures the test verifies only the logic within that unit.
     - **Integration Tests**: Test how multiple units or layers interact. They involve real or simulated dependencies (e.g., in-memory databases, actual repositories) to verify end-to-end workflows, data flow, and side effects across boundaries.

   - **Dependencies**:
     - **Unit Tests**: Use mocks (e.g., via Moq) for external dependencies like `IUserRepository`, `IUnitOfWork`, or `IAuditLogWriter`. This isolates the code under test and makes tests fast and deterministic.
     - **Integration Tests**: Use real implementations or lightweight substitutes (e.g., EF Core in-memory database for repositories). This tests actual interactions, such as database writes or event dispatching.

   - **Speed and Execution**:
     - **Unit Tests**: Fast (milliseconds per test), run frequently (e.g., on every code change via CI/CD). They don't require external resources like databases.
     - **Integration Tests**: Slower (seconds to minutes), as they set up/tear down resources. Run less often (e.g., nightly or before releases) to avoid slowing down development.

   - **What They Catch**:
     - **Unit Tests**: Logic bugs, edge cases, and algorithmic errors within a single component (e.g., "Does the `TimeSheet` aggregate correctly validate period overlaps?").
     - **Integration Tests**: Interaction bugs, configuration issues, and data consistency problems (e.g., "Does submitting a time sheet actually save to the database and dispatch events?").

   - **Setup and Maintenance**:
     - **Unit Tests**: Easier to write and maintain; changes to one unit don't break unrelated tests.
     - **Integration Tests**: More complex; require setup for shared state (e.g., seeding test data) and can be brittle if dependencies change.

   - **Frameworks and Tools**:
     - Both use xUnit and FluentAssertions in your setup.
     - Unit tests add Moq for mocking.
     - Integration tests might use EF Core in-memory or TestContainers for databases.

#### 2. **When to Use Each (General Guidelines)**
   - **Use Unit Tests When**:
     - You want to verify pure logic, calculations, or rules without external side effects.
     - Testing is frequent and needs to be fast (e.g., during TDD or refactoring).
     - Isolating bugs: If a test fails, you know exactly where the issue is.
     - Examples: Validating input rules, testing domain invariants, or ensuring a handler processes a command correctly in isolation.

   - **Use Integration Tests When**:
     - You need to verify that components work together as a system.
     - Testing workflows that span layers (e.g., API ? Application ? Domain ? Infrastructure).
     - Ensuring data persistence, transactions, or external integrations (e.g., email sending, audit logging).
     - Examples: Full user registration flow, end-to-end time sheet approval, or verifying that a rejected expense report triggers the correct events and updates.

   - **Balancing Both**:
     - Aim for 70-80% unit tests and 20-30% integration tests. Unit tests provide quick feedback; integration tests catch "real-world" issues.
     - Start with unit tests for new features, then add integration tests for critical paths.
     - Avoid over-relying on integration tests—they're slower and harder to debug.

#### 3. **Application to Your TimesheetManagement App**
Your app is a multi-layered DDD system with CQRS (commands/queries), domain events, and infrastructure concerns (e.g., EF Core repositories, audit logging). Here's how unit and integration tests map to your layers and the test structure in `test-setup.md`:

   - **Unit Tests (Isolated, Fast)**:
     - **Best For**: Domain logic, handler business rules, and validation without touching the database or external services.
     - **Examples in Your App**:
       - **Domain Layer**: Already covered (e.g., `TimeSheetTests.cs` for invariants like "AddEntry throws if outside period").
       - **Application Layer**: Test handlers and validators in isolation (e.g., `RegisterUserHandlerTests.cs` mocks `IUserRepository` and `IUnitOfWork` to verify user creation logic without saving to DB).
         - When: Use for validating command/query logic, such as "Does `CreateTimeSheetHandler` correctly create a `TimeSheet` aggregate and validate the period?"
         - Why: Catches bugs in handler code (e.g., wrong event raising) without DB overhead.
     - **When to Use**: During development for quick iterations. Run via `dotnet test --filter "Application.UnitTests"`.
     - **Pros in Your Context**: Your app has complex domain rules (e.g., time sheet approvals, expense validations)—unit tests ensure these are correct without infrastructure noise.

   - **Integration Tests (End-to-End, Realistic)**:
     - **Best For**: Full workflows involving persistence, events, and cross-layer interactions.
     - **Examples in Your App**:
       - **Application + Infrastructure**: Test handlers with real repositories (e.g., `CreateTimeSheetIntegrationTests.cs` uses an in-memory EF Core context to verify the time sheet is saved and events are dispatched).
         - When: Use for scenarios like "Submit a time sheet, approve it, and verify the status changes in the database."
         - Why: Ensures CQRS commands/queries work with actual data flow (e.g., `IUnitOfWork.Commit()` persists changes, `DomainEventDispatcher` fires events).
       - **API Layer (Future)**: Could extend to test controllers calling handlers, but stick to Application for now.
       - **Cross-Layer**: E.g., `RegisterUserIntegrationTests.cs` tests user creation, role assignment, and audit logging end-to-end.
     - **When to Use**: For critical business flows (e.g., payroll processing via time sheets) or after unit tests pass. Run via `dotnet test --filter "Application.IntegrationTests"`.
     - **Pros in Your Context**: Your app relies on domain events (e.g., `TimeSheetSubmittedEvent`) and audit logs—integration tests verify these propagate correctly. Also crucial for Infrastructure (e.g., EF mappings, migrations).

   - **Specific Scenarios in Your App**:
     - **Unit Test Example**: Testing `SubmitTimeSheetValidator`—mock user context, check if validation fails for empty time sheets.
     - **Integration Test Example**: Full `SubmitTimeSheet` flow—create time sheet, submit, verify DB state, check events logged.
     - **Avoid Overlap**: Don't use integration tests for simple logic (e.g., date range validation—that's unit). Use them for "does the whole pipeline work?"
     - **Edge Cases**: Unit tests for domain invariants (e.g., duplicate team members); integration for authorization (e.g., only admins can approve).

#### 4. **Best Practices for Your App**
   - **Setup**: Use `ApplicationTestHelpers.cs` for shared mocks (unit) and in-memory repo factories (integration). `TestData.cs` for fixtures.
   - **Naming and Structure**: Follow `[Handler]_[Scenario]_Should_[Expected]` for clarity.
   - **CI/CD**: Run unit tests on every push; integration tests on merges.
   - **Pitfalls**: Don't mock everything in integration tests (defeats the purpose). For your .NET 8 app, leverage `Microsoft.EntityFrameworkCore.InMemory` for lightweight DB simulation.
   - **Expansion**: Once Application tests are solid, add API integration tests (e.g., via TestServer) for full HTTP flows.

This setup ensures robust testing for your DDD app. If you implement a specific test (e.g., a unit test for `RegisterUserHandler`), I can help refine it! Let me know if you need code examples.