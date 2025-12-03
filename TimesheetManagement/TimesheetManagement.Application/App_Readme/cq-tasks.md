Application Commands & Queries Tasks (TimesheetManagement, .NET 8)

Identity
- Commands
  - AssignRoleCommand (UserId, Role)
  - ChangePasswordCommand (UserId, NewPasswordHash)
- Queries
  - GetUserByEmailQuery (Email)
  - GetUserByIdQuery (UserId)

TimeTracking
- Commands
  - SubmitTimeSheetCommand (TimeSheetId)
  - ApproveTimeSheetCommand (TimeSheetId, Comment, AdminId)
  - RejectTimeSheetCommand (TimeSheetId, Comment, AdminId)
  - AddTimeEntryCommand (TimeSheetId, ProjectId, Date, HoursWorked, Notes)
  - RemoveTimeEntryCommand (TimeSheetId, EntryId)
  - EditAfterRejectionCommand (TimeSheetId)
- Queries
  - GetTimeSheetsForUserQuery (UserId, From?, To?)
  - GetTimeSheetByIdQuery (TimeSheetId)

Expenses
- Commands
  - SubmitExpenseReportCommand (ExpenseReportId)
  - ApproveExpenseReportCommand (ExpenseReportId, Comment, AdminId)
  - RejectExpenseReportCommand (ExpenseReportId, Comment, AdminId)
  - AddExpenseItemCommand (ExpenseReportId, Date, Category, Money, ReceiptPath?, Notes?)
  - RemoveExpenseItemCommand (ExpenseReportId, ItemId)
  - EditExpenseAfterRejectionCommand (ExpenseReportId)
- Queries
  - GetExpenseReportsForUserQuery (UserId, From?, To?)
  - GetExpenseReportByIdQuery (ExpenseReportId)

Projects
- Commands
  - CreateProjectCommand (Code, Name, Industry)
  - ArchiveProjectCommand (ProjectId)
  - RestoreProjectCommand (ProjectId)
- Queries
  - GetAllProjectsQuery ()
  - GetProjectByCodeQuery (Code)

Teams
- Commands
  - CreateTeamCommand (Name)
  - AddTeamMemberCommand (TeamId, UserId)
  - RemoveTeamMemberCommand (TeamId, UserId)
  - ArchiveTeamCommand (TeamId)
  - RestoreTeamCommand (TeamId)
- Queries
  - GetAllTeamsQuery ()
  - GetTeamByIdQuery (TeamId)

Validator notes
- Use FluentValidation (optional) for DTO shape and auth checks:
  - AssignRole: ensure requester has Admin/SuperAdmin role
  - Submit/Approve/Reject: ensure correct status transitions; admin scope validation
  - AddTimeEntry: ensure date inside period and project exists (IProjectRepository)
  - CreateProject: ensure unique code/name (IProjectRepository)
- Use repository checks for existence/uniqueness and overlap:
  - TimeSheets: HasSubmittedForRangeAsync
  - ExpenseReports: HasSubmittedForRangeAsync

Definition of done per task
- DTO defined and validator implemented (if using FluentValidation)
- Handler implemented (uses Domain repositories and aggregates)
- Transaction committed via IUnitOfWork
- Unit tests added for handler logic
