# API Setup Documentation (TimesheetManagement)

## Overview

This API provides RESTful endpoints for the TimesheetManagement application built on .NET 8 using Clean Architecture principles with Domain-Driven Design (DDD). The API is organized around bounded contexts: **Identity**, **TimeTracking**, **Expenses**, **Projects**, **Teams**, and **Audit**.

**Base URL:** `https://localhost:5001/api`

---

## Core Concepts

- **Commands**: State-changing operations (POST, PUT, DELETE)
- **Queries**: Read-only operations (GET)
- **DTOs**: Data Transfer Objects for request/response payloads
- **Handlers**: Application layer components orchestrating business logic
- **Repositories**: Infrastructure layer components providing data persistence

---

## Authentication & Authorization

All endpoints (except public endpoints) require:
- **Header**: `Authorization: Bearer {token}`
- **Role-based access control** enforced at handler level:
  - `Admin`: Can approve/reject submissions, manage roles
  - `SuperAdmin`: Full system access
  - `User`: Submit timesheets/expenses, view own records

---

## API Endpoints

### Identity Context

#### Users

**1. Get User by ID**
```
GET /api/identity/users/{userId}
```
- **Query**: `GetUserByIdQuery`
- **Handler**: `GetUserByIdHandler`
- **Response**: `UserDto` (Id, Username, Email, Role)
- **Authorization**: Admin or self
- **Status**: 
  - `200 OK`: User found
  - `404 Not Found`: User does not exist
  - `401 Unauthorized`: Invalid token

**2. Get User by Email**
```
GET /api/identity/users/email/{email}
```
- **Query**: `GetUserByEmailQuery`
- **Handler**: `GetUserByEmailHandler`
- **Response**: `UserDto`
- **Authorization**: Admin
- **Status**:
  - `200 OK`: User found
  - `404 Not Found`: User not found
  - `400 Bad Request`: Invalid email format

**3. Register User** (Future)
```
POST /api/identity/users/register
```
- **Command**: `RegisterUserCommand`
- **Handler**: `RegisterUserHandler`
- **Request Body**:
```json
{
  "username": "string (max 64, required)",
  "email": "string (valid email, required)",
  "password": "string (min 8, required)"
}
```
- **Response**: `{ id: Guid }`
- **Authorization**: Public/AllowAnonymous
- **Validation**:
  - Username not empty
  - Email format valid
  - Email unique (via `IUserRepository.ExistsAsync`)
  - Password minimum 8 characters
- **Status**:
  - `201 Created`: User registered
  - `400 Bad Request`: Validation error
  - `409 Conflict`: Username or email already exists

**4. Assign Role** (Future)
```
POST /api/identity/users/{userId}/roles
```
- **Command**: `AssignRoleCommand`
- **Handler**: `AssignRoleHandler`
- **Request Body**:
```json
{
  "role": "Admin | SuperAdmin | User",
  "adminId": "Guid (current user)"
}
```
- **Response**: `{ success: bool }`
- **Authorization**: SuperAdmin only
- **Validation**:
  - UserId not empty
  - Role valid enum
  - AdminId != UserId
- **Status**:
  - `200 OK`: Role assigned
  - `400 Bad Request`: Validation error
  - `404 Not Found`: User not found
  - `403 Forbidden`: Insufficient role

**5. Change Password** (Future)
```
PUT /api/identity/users/{userId}/password
```
- **Command**: `ChangePasswordCommand`
- **Handler**: `ChangePasswordHandler`
- **Request Body**:
```json
{
  "currentPassword": "string",
  "newPassword": "string (min 8)"
}
```
- **Response**: `{ success: bool }`
- **Authorization**: Self or Admin
- **Status**:
  - `200 OK`: Password changed
  - `400 Bad Request`: Invalid current password
  - `401 Unauthorized`: Insufficient privilege

---

### TimeTracking Context

#### TimeSheets

**1. Create TimeSheet**
```
POST /api/timetracking/timesheets
```
- **Command**: `CreateTimeSheetCommand`
- **Handler**: `CreateTimeSheetHandler`
- **Request Body**:
```json
{
  "userId": "Guid",
  "from": "2024-01-01",
  "to": "2024-01-07"
}
```
- **Response**: `{ id: Guid }`
- **Authorization**: Self or Admin
- **Validation** (via `CreateTimeSheetValidator`):
  - `UserId` not empty
  - `From` date not empty
  - `To` >= `From`
  - No submitted timesheet exists for period (via `ITimeSheetRepository.HasSubmittedForRangeAsync`)
- **Domain Invariants**:
  - TimeSheet starts in `Draft` status
  - Period must be valid `DateRange`
- **Status**:
  - `201 Created`: Timesheet created
  - `400 Bad Request`: Validation error
  - `409 Conflict`: Overlapping submitted timesheet exists

**2. Get TimeSheet by ID**
```
GET /api/timetracking/timesheets/{timesheetId}
```
- **Query**: `GetTimeSheetByIdQuery`
- **Handler**: `GetTimeSheetByIdHandler`
- **Response**: `TimeSheetDetailsDto` (Id, UserId, From, To, Status, Comment, Entries[])
- **Authorization**: Owner or Admin
- **Status**:
  - `200 OK`: Timesheet found
  - `404 Not Found`: Timesheet does not exist
  - `403 Forbidden`: Access denied

**3. Get TimeSheetsfor User**
```
GET /api/timetracking/timesheets?userId={userId}&from={from}&to={to}
```
- **Query**: `GetTimeSheetsForUserQuery`
- **Handler**: `GetTimeSheetsForUserHandler`
- **Query Parameters**:
  - `userId`: Guid (required, self or admin can query others)
  - `from`: DateOnly (optional, filters start date)
  - `to`: DateOnly (optional, filters end date)
- **Response**: `IReadOnlyList<TimeSheetSummaryDto>` (Id, UserId, From, To, Status, EntryCount)
- **Authorization**: Self or Admin
- **Status**:
  - `200 OK`: List returned (may be empty)
  - `400 Bad Request`: Invalid date range

**4. Add TimeEntry to TimeSheet**
```
POST /api/timetracking/timesheets/{timesheetId}/entries
```
- **Command**: `AddTimeEntryCommand`
- **Handler**: `AddTimeEntryHandler`
- **Request Body**:
```json
{
  "timesheetId": "Guid",
  "projectId": "Guid",
  "date": "2024-01-02",
  "hours": 8.5,
  "notes": "string (optional)"
}
```
- **Response**: `{ id: Guid }`
- **Authorization**: Timesheet owner or Admin
- **Validation** (via `AddTimeEntryValidator`):
  - `ProjectId` not empty (verified via `IProjectRepository`)
  - `Date` not empty
  - `Hours` > 0 and <= 24
  - TimeSheet exists and is in `Draft` status
  - `Date` falls within TimeSheet period
- **Domain Invariants**:
  - `HoursWorked` enforces 0 < hours <= 24
  - Entry date within `Period` (DateRange)
- **Status**:
  - `201 Created`: Entry added
  - `400 Bad Request`: Validation error
  - `404 Not Found`: Timesheet or project not found
  - `409 Conflict`: Timesheet not in Draft status

**5. Remove TimeEntry**
```
DELETE /api/timetracking/timesheets/{timesheetId}/entries/{entryId}
```
- **Command**: `RemoveTimeEntryCommand`
- **Handler**: `RemoveTimeEntryHandler`
- **Request Body**: (none)
- **Response**: `{ success: bool }`
- **Authorization**: Timesheet owner or Admin
- **Status**:
  - `204 No Content`: Entry removed
  - `404 Not Found`: Timesheet or entry not found
  - `409 Conflict`: Timesheet not in Draft status

**6. Submit TimeSheet**
```
POST /api/timetracking/timesheets/{timesheetId}/submit
```
- **Command**: `SubmitTimeSheetCommand`
- **Handler**: `SubmitTimeSheetHandler`
- **Request Body**:
```json
{
  "timesheetId": "Guid"
}
```
- **Response**: `{ success: bool }`
- **Authorization**: Timesheet owner or Admin
- **Domain Logic**:
  - Status transitions from `Draft` ? `Submitted`
  - Raises `TimeSheetSubmittedEvent` (for audit)
  - Requires at least one entry
- **Status**:
  - `200 OK`: Submitted
  - `400 Bad Request`: No entries or invalid state
  - `404 Not Found`: Timesheet not found
  - `409 Conflict`: Not in Draft status

**7. Approve TimeSheet**
```
POST /api/timetracking/timesheets/{timesheetId}/approve
```
- **Command**: `ApproveTimeSheetCommand`
- **Handler**: `ApproveTimeSheetHandler`
- **Request Body**:
```json
{
  "timesheetId": "Guid",
  "comment": "string (optional)",
  "adminId": "Guid (current user)"
}
```
- **Response**: `{ success: bool }`
- **Authorization**: Admin/SuperAdmin only
- **Domain Logic**:
  - Status: `Submitted` ? `Approved`
  - Raises `TimeSheetApprovedEvent`
  - Records approval by adminId
- **Status**:
  - `200 OK`: Approved
  - `404 Not Found`: Timesheet not found
  - `409 Conflict`: Not in Submitted status
  - `403 Forbidden`: Insufficient role

**8. Reject TimeSheet**
```
POST /api/timetracking/timesheets/{timesheetId}/reject
```
- **Command**: `RejectTimeSheetCommand`
- **Handler**: `RejectTimeSheetHandler`
- **Request Body**:
```json
{
  "timesheetId": "Guid",
  "comment": "string (required)",
  "adminId": "Guid"
}
```
- **Response**: `{ success: bool }`
- **Authorization**: Admin/SuperAdmin only
- **Domain Logic**:
  - Status: `Submitted` ? `Rejected`
  - Raises `TimeSheetRejectedEvent`
  - Comment required for rejection reason
- **Status**:
  - `200 OK`: Rejected
  - `400 Bad Request`: Comment empty
  - `404 Not Found`: Timesheet not found
  - `409 Conflict`: Not in Submitted status

**9. Edit TimeSheet After Rejection** (Future)
```
PUT /api/timetracking/timesheets/{timesheetId}/edit-after-rejection
```
- **Command**: `EditAfterRejectionCommand`
- **Handler**: `EditAfterRejectionHandler`
- **Request Body**:
```json
{
  "timesheetId": "Guid"
}
```
- **Response**: `{ success: bool }`
- **Authorization**: Timesheet owner
- **Domain Logic**:
  - Status: `Rejected` ? `Draft`
  - Allows re-editing entries
- **Status**:
  - `200 OK`: Re-opened
  - `404 Not Found`: Timesheet not found
  - `409 Conflict`: Not in Rejected status

---

### Expenses Context

#### ExpenseReports

**1. Create ExpenseReport**
```
POST /api/expenses/reports
```
- **Command**: `CreateExpenseReportCommand`
- **Handler**: `CreateExpenseReportHandler`
- **Request Body**:
```json
{
  "userId": "Guid",
  "from": "2024-01-01",
  "to": "2024-01-31"
}
```
- **Response**: `{ id: Guid }`
- **Authorization**: Self or Admin
- **Validation**:
  - `UserId` not empty
  - Date range valid
  - No submitted report for period
- **Status**:
  - `201 Created`: Report created
  - `400 Bad Request`: Validation error
  - `409 Conflict`: Overlapping submitted report exists

**2. Get ExpenseReport by ID**
```
GET /api/expenses/reports/{reportId}
```
- **Query**: `GetExpenseReportByIdQuery`
- **Handler**: `GetExpenseReportByIdHandler`
- **Response**: `ExpenseReportDetailsDto` (Id, UserId, From, To, Status, Comment, Items[])
- **Authorization**: Owner or Admin
- **Status**:
  - `200 OK`: Report found
  - `404 Not Found`: Report does not exist

**3. Get ExpenseReports for User**
```
GET /api/expenses/reports?userId={userId}&from={from}&to={to}
```
- **Query**: `GetExpenseReportsForUserQuery`
- **Handler**: `GetExpenseReportsForUserHandler`
- **Query Parameters**:
  - `userId`: Guid (required)
  - `from`: DateOnly (optional)
  - `to`: DateOnly (optional)
- **Response**: `IReadOnlyList<ExpenseReportSummaryDto>`
- **Authorization**: Self or Admin
- **Status**:
  - `200 OK`: List returned

**4. Add ExpenseItem to Report**
```
POST /api/expenses/reports/{reportId}/items
```
- **Command**: `AddExpenseItemCommand`
- **Handler**: `AddExpenseItemHandler`
- **Request Body**:
```json
{
  "reportId": "Guid",
  "date": "2024-01-05",
  "category": "Travel | Meals | Supplies | Other",
  "amount": 150.50,
  "currency": "USD",
  "receiptPath": "string (optional)",
  "notes": "string (optional)"
}
```
- **Response**: `{ id: Guid }`
- **Authorization**: Report owner or Admin
- **Validation**:
  - Category not empty
  - `Amount` > 0
  - Date within report period
- **Status**:
  - `201 Created`: Item added
  - `400 Bad Request`: Validation error
  - `404 Not Found`: Report not found

**5. Remove ExpenseItem**
```
DELETE /api/expenses/reports/{reportId}/items/{itemId}
```
- **Command**: `RemoveExpenseItemCommand`
- **Handler**: `RemoveExpenseItemHandler`
- **Authorization**: Report owner or Admin
- **Status**:
  - `204 No Content`: Item removed
  - `404 Not Found`: Not found
  - `409 Conflict`: Report not in Draft status

**6. Submit ExpenseReport**
```
POST /api/expenses/reports/{reportId}/submit
```
- **Command**: `SubmitExpenseReportCommand`
- **Handler**: `SubmitExpenseReportHandler`
- **Response**: `{ success: bool }`
- **Authorization**: Report owner or Admin
- **Domain Logic**:
  - Status: `Draft` ? `Submitted`
  - Raises `ExpenseSubmittedEvent`
  - Requires at least one item
- **Status**:
  - `200 OK`: Submitted
  - `400 Bad Request`: No items
  - `409 Conflict`: Not in Draft status

**7. Approve ExpenseReport**
```
POST /api/expenses/reports/{reportId}/approve
```
- **Command**: `ApproveExpenseReportCommand`
- **Handler**: `ApproveExpenseReportHandler`
- **Request Body**:
```json
{
  "reportId": "Guid",
  "comment": "string (optional)",
  "adminId": "Guid"
}
```
- **Response**: `{ success: bool }`
- **Authorization**: Admin/SuperAdmin
- **Status**:
  - `200 OK`: Approved
  - `409 Conflict`: Not in Submitted status

**8. Reject ExpenseReport**
```
POST /api/expenses/reports/{reportId}/reject
```
- **Command**: `RejectExpenseReportCommand`
- **Handler**: `RejectExpenseReportHandler`
- **Request Body**:
```json
{
  "reportId": "Guid",
  "comment": "string (required)",
  "adminId": "Guid"
}
```
- **Response**: `{ success: bool }`
- **Authorization**: Admin/SuperAdmin
- **Status**:
  - `200 OK`: Rejected
  - `400 Bad Request`: Comment empty

**9. Edit ExpenseReport After Rejection**
```
PUT /api/expenses/reports/{reportId}/edit-after-rejection
```
- **Command**: `EditExpenseAfterRejectionCommand`
- **Handler**: `EditExpenseAfterRejectionHandler`
- **Authorization**: Report owner
- **Status**:
  - `200 OK`: Re-opened
  - `409 Conflict`: Not in Rejected status

---

### Projects Context

#### Projects

**1. Create Project**
```
POST /api/projects
```
- **Command**: `CreateProjectCommand`
- **Handler**: `CreateProjectHandler`
- **Request Body**:
```json
{
  "code": "string (max 64, unique, required)",
  "name": "string (max 128, unique, required)",
  "industry": "string (max 128, required)"
}
```
- **Response**: `{ id: Guid }`
- **Authorization**: Admin/SuperAdmin
- **Validation** (via `CreateProjectValidator`):
  - Code not empty, max 64 chars, unique (via `IProjectRepository.CodeExistsAsync`)
  - Name not empty, max 128 chars, unique (via `IProjectRepository.NameExistsAsync`)
  - Industry not empty
- **Status**:
  - `201 Created`: Project created
  - `400 Bad Request`: Validation error
  - `409 Conflict`: Code or name already exists

**2. Get All Projects**
```
GET /api/projects
```
- **Query**: `GetAllProjectsQuery`
- **Handler**: `GetAllProjectsHandler`
- **Response**: `IReadOnlyList<ProjectDto>` (Id, Code, Name, Industry, IsArchived)
- **Authorization**: All authenticated users
- **Status**:
  - `200 OK`: List returned (may be empty)

**3. Get Project by Code**
```
GET /api/projects/{code}
```
- **Query**: `GetProjectByCodeQuery`
- **Handler**: `GetProjectByCodeHandler`
- **Response**: `ProjectDto`
- **Authorization**: All authenticated users
- **Status**:
  - `200 OK`: Project found
  - `404 Not Found`: Project not found

**4. Archive Project**
```
PUT /api/projects/{projectId}/archive
```
- **Command**: `ArchiveProjectCommand`
- **Handler**: `ArchiveProjectHandler`
- **Request Body**: `{ projectId: Guid }`
- **Response**: `{ success: bool }`
- **Authorization**: Admin/SuperAdmin
- **Domain Logic**:
  - Sets `IsArchived` to `true`
  - Prevents use in new timesheets/expenses
- **Status**:
  - `200 OK`: Archived
  - `404 Not Found`: Project not found

**5. Restore Project**
```
PUT /api/projects/{projectId}/restore
```
- **Command**: `RestoreProjectCommand`
- **Handler**: `RestoreProjectHandler`
- **Authorization**: Admin/SuperAdmin
- **Status**:
  - `200 OK`: Restored
  - `404 Not Found`: Project not found

---

### Teams Context

#### Teams

**1. Create Team**
```
POST /api/teams
```
- **Command**: `CreateTeamCommand`
- **Handler**: `CreateTeamHandler`
- **Request Body**:
```json
{
  "name": "string (max 128, unique, required)"
}
```
- **Response**: `{ id: Guid }`
- **Authorization**: Admin/SuperAdmin
- **Validation**:
  - Name not empty, unique
- **Status**:
  - `201 Created`: Team created
  - `409 Conflict`: Name already exists

**2. Get All Teams**
```
GET /api/teams
```
- **Query**: `GetAllTeamsQuery`
- **Handler**: `GetAllTeamsHandler`
- **Response**: `IReadOnlyList<TeamDto>` (Id, Name, IsArchived, MemberIds[])
- **Authorization**: All authenticated users
- **Status**:
  - `200 OK`: List returned

**3. Get Team by ID**
```
GET /api/teams/{teamId}
```
- **Query**: `GetTeamByIdQuery`
- **Handler**: `GetTeamByIdHandler`
- **Response**: `TeamDto`
- **Authorization**: All authenticated users
- **Status**:
  - `200 OK`: Team found
  - `404 Not Found`: Team not found

**4. Add Team Member**
```
POST /api/teams/{teamId}/members
```
- **Command**: `AddTeamMemberCommand`
- **Handler**: `AddTeamMemberHandler`
- **Request Body**:
```json
{
  "teamId": "Guid",
  "userId": "Guid"
}
```
- **Response**: `{ success: bool }`
- **Authorization**: Admin/SuperAdmin
- **Validation**:
  - UserId not empty
  - Team not archived
  - User not already a member
- **Status**:
  - `200 OK`: Member added
  - `404 Not Found`: Team or user not found
  - `409 Conflict`: Team archived or user already member

**5. Remove Team Member**
```
DELETE /api/teams/{teamId}/members/{userId}
```
- **Command**: `RemoveTeamMemberCommand`
- **Handler**: `RemoveTeamMemberHandler`
- **Authorization**: Admin/SuperAdmin
- **Status**:
  - `204 No Content`: Member removed
  - `404 Not Found`: Not found
  - `409 Conflict`: Team archived

**6. Archive Team**
```
PUT /api/teams/{teamId}/archive
```
- **Command**: `ArchiveTeamCommand`
- **Handler**: `ArchiveTeamHandler`
- **Authorization**: Admin/SuperAdmin
- **Status**:
  - `200 OK`: Archived
  - `404 Not Found`: Team not found

**7. Restore Team**
```
PUT /api/teams/{teamId}/restore
```
- **Command**: `RestoreTeamCommand`
- **Handler**: `RestoreTeamHandler`
- **Authorization**: Admin/SuperAdmin
- **Status**:
  - `200 OK`: Restored
  - `404 Not Found`: Team not found

---

### Audit Context (Optional)

#### AuditLogs

**1. Get Audit Logs**
```
GET /api/audit/logs?userId={userId}&startDate={startDate}&endDate={endDate}&action={action}
```
- **Query**: `GetAuditLogsQuery`
- **Handler**: `GetAuditLogsHandler`
- **Query Parameters**:
  - `userId`: Guid (optional, filter by user)
  - `startDate`: DateTime (optional)
  - `endDate`: DateTime (optional)
  - `action`: string (optional, e.g., "TimeSheetSubmitted", "Approved")
- **Response**: `IReadOnlyList<AuditLogDto>` (Id, UserId, Action, EntityId, EntityType, Timestamp, Details)
- **Authorization**: Admin/SuperAdmin
- **Status**:
  - `200 OK`: List returned

---

## Common HTTP Status Codes

| Code | Meaning |
|------|---------|
| `200 OK` | Successful GET, PUT, or DELETE |
| `201 Created` | Successful POST |
| `204 No Content` | Successful DELETE with no response body |
| `400 Bad Request` | Validation error or malformed request |
| `401 Unauthorized` | Missing or invalid token |
| `403 Forbidden` | Insufficient role/permission |
| `404 Not Found` | Resource not found |
| `409 Conflict` | Business rule violation (e.g., duplicate, invalid state) |
| `422 Unprocessable Entity` | Validation failure details |
| `500 Internal Server Error` | Unexpected server error |

---

## Error Response Format

All error responses follow this format:

```json
{
  "statusCode": 400,
  "message": "Validation failed",
  "errors": [
    {
      "field": "hours",
      "message": "Hours must be between 0 and 24"
    },
    {
      "field": "projectId",
      "message": "Project does not exist"
    }
  ],
  "timestamp": "2024-12-05T10:30:00Z"
}
```

---

## Request/Response Examples

### Example 1: Create TimeSheet

**Request:**
```http
POST /api/timetracking/timesheets
Authorization: Bearer {token}
Content-Type: application/json

{
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "from": "2024-12-01",
  "to": "2024-12-07"
}
```

**Response (201 Created):**
```json
{
  "id": "660e8400-e29b-41d4-a716-446655440001"
}
```

### Example 2: Get TimeSheet by ID

**Request:**
```http
GET /api/timetracking/timesheets/660e8400-e29b-41d4-a716-446655440001
Authorization: Bearer {token}
```

**Response (200 OK):**
```json
{
  "id": "660e8400-e29b-41d4-a716-446655440001",
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "from": "2024-12-01",
  "to": "2024-12-07",
  "status": "Draft",
  "comment": null,
  "entries": [
    {
      "id": "770e8400-e29b-41d4-a716-446655440002",
      "projectId": "880e8400-e29b-41d4-a716-446655440003",
      "date": "2024-12-02",
      "hours": 8.0,
      "notes": "Development work"
    }
  ]
}
```

---

## Validation & Business Rules

### TimeSheet Creation
- Period must have `To >= From`
- No submitted timesheet can overlap with the requested period
- Period enforced by `DateRange` value object

### TimeEntry
- Hours must be `0 < hours <= 24` (enforced by `HoursWorked` value object)
- Date must fall within timesheet period
- Project must exist and not be archived

### Expense Item
- Amount must be `> 0` (enforced by `Money` value object)
- Category required
- Date must fall within report period

### Projects
- Code and Name must be unique
- Code max 64 chars, Name max 128 chars

### Teams
- Name must be unique
- Cannot modify members while archived

---

## State Transitions

### TimeSheet Workflow
```
Draft ? Submitted ? Approved
  ?        ?
  ??? Rejected ? Draft (re-edit)
```

### ExpenseReport Workflow
```
Draft ? Submitted ? Approved
  ?        ?
  ??? Rejected ? Draft (re-edit)
```

### Project Lifecycle
```
Active ? Archived
```

### Team Lifecycle
```
Active ? Archived
```

---

## DI Registration (Program.cs)

```csharp
builder.Services
    .AddApplication()           // Commands, Queries, Handlers, Validators
    .AddInfrastructure(config) // DbContext, Repositories, UnitOfWork
    .AddControllers();

builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options => { /* JWT config */ });
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

---

## Future Enhancements

1. **Pagination**: Add `pageNumber` and `pageSize` query parameters to list endpoints
2. **Filtering**: Add `status`, `from`, `to` filters to list endpoints
3. **Sorting**: Support `orderBy` and `orderDirection` query parameters
4. **Bulk Operations**: Batch delete/update endpoints
5. **Export**: Generate PDF/Excel exports of timesheets/expenses
6. **Notifications**: Real-time updates via SignalR for approval/rejection
7. **Attachments**: Upload and manage expense receipts as file storage
8. **Recurring**: Support recurring timesheet/expense templates

---

## Testing

- **Unit Tests**: Handler and validator tests with mocked repositories
- **Integration Tests**: EF Core tests against SQL Server or SQLite
- **API Tests**: Postman collections for manual verification
- **Performance**: Benchmark critical queries (GetTimeSheetsForUser, GetExpenseReportsForUser)

---

## References

- Domain Layer: `TimesheetManagement.Domain/App_Readme/app-target.md`
- Application Layer: `TimesheetManagement.Application/App_Readme/app-target.md`
- Infrastructure Layer: `TimesheetManagement.Infrastructure/App_Readme/app-target.md`
- DDD Pattern: Vaughn Vernon's "Implementing Domain-Driven Design"

---

**Last Updated**: December 5, 2024
**Version**: 1.0.0
**Target Framework**: .NET 8