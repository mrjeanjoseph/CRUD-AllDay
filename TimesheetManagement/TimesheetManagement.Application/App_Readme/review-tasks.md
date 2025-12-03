# Infrastructure review summary

## Implemented:
-	DbContext with DbSets for new aggregates.
-	Fluent configurations for User, RoleAssignment, TimeSheet, TimeEntry, ExpenseReport, ExpenseItem, Project, Team.
-	Repositories for all aggregate interfaces.
-	UnitOfWork and DI registration.
-	Connection string updated and working.

## Gaps / recommendations (per infra.readme and UCBR use cases):
1.	Domain events dispatch
-	No event collection flush/dispatch after SaveChanges. Add lightweight dispatcher (Application) or an EF Core SaveChanges interceptor.
2.	Team members mapping
-	Stored as JSON column; infra.readme target suggested join table TeamMembers. For querying/filtering and membership constraints a junction table is preferable. Consider refactor before production.
3.	Indexes / performance
-	Removed composite indexes on (UserId, FromDate, ToDate) for TimeSheet and ExpenseReport due to ComplexProperty mapping issues. Reintroduce by adding shadow properties or explicit owned mapping with index for overlap queries.
-	Add indexes on Status where overlap / listing queries frequent.
4.	Value object consistency
-	ExpenseItem currency hard-coded to “USD” in conversion earlier (now proper mapping). Ensure dynamic currency persisted (currently separate Currency column exists; verify Money mapping returns correct currency).
5.	Legacy EF6 artifacts
-	EntityFramework (EF6) package and legacy concretes (*Concrete.cs) still in project; can conflict with EF Core. Plan removal after migration of controllers. Remove EF6 package to avoid accidental context mix.
6.	Legacy tables (Audit, Notifications, Description, Export masters)
-	Not mapped. If still needed for reporting / audit:
-	Introduce adapters: IAuditLogRepository, INotificationRepository interfacing with new domain events / application handlers.
-	Or implement Dapper readers for export use cases (time sheet master, expense master) as read-only queries.
7.	Security / multi-tenancy
-	No soft-delete or row-level filtering implemented. If roles should scope queries (Admin vs SuperAdmin), enforce in Application handlers; not in infra currently.
8.	Concurrency
-	No optimistic concurrency tokens (rowversion) on high-traffic tables (TimeSheets, ExpenseReports). Consider adding.
9.	Transaction boundaries
-	Single DbContext per request via DI is fine. If domain events dispatch added, ensure events only published after commit success.
10.	Repository completeness
-	Lack of methods for listing submitted/pending approvals (Needed for admin dashboards). Consider query-focused repository methods or separate query services using Dapper.
11.	Testing / reliability
-	InMemory provider caused navigation issues with ComplexProperty. Keep tests focused on domain logic; add EF integration tests using SQLite or SQL Server container for mapping validation later.
12.	Notifications
-	UCBR mentions push (SignalR) and persistence; currently only a Hub exists. Add infra abstraction INotificationSender (SignalR implementation) and optionally persist notification history.
13.	Audit trail
-	UCBR requires audit (role changes, approvals/rejections). Capture domain events (TimeSheetSubmitted, Approved, Rejected, Expense equivalents, RoleAssigned) and persist audit records in an Audit table via dispatcher.

## Next prioritized tasks:
1.	Add domain event dispatcher (simple interface + Application implementation; Infra optional).
2.	Reintroduce indices for overlap queries with explicit shadow properties.
3.	Replace Team members JSON with junction table mapping.
4.	Add IAuditLogRepository and INotificationSender.
5.	Remove EF6 package and legacy concretes after migrating controllers.
6.	Introduce Dapper read model interfaces for exports.
7.	Add rowversion concurrency token to TimeSheet and ExpenseReport (optional).
8.	Add repository methods for pending approvals (filter Status == Submitted by user scope).