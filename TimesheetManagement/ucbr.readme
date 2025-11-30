Use Cases and Business Workflows (UC) for TimesheetManagement (.NET 8, Razor Pages)

Roles
- Super-Admin
- Admin
- User

Super-Admin Use Cases
- Authenticate to super-admin portal
- View dashboard (system stats: admins, users, projects, activity)
- Create admin accounts
- Create user accounts (global or assign to admins)
- Assign roles and permissions (Admin/User)
- Manage projects and teams globally (create/edit/archive)
- Reset any user/admin password
- Configure global settings (time/expense policies)
- View/export master time sheets (all users)
- View/export master expenses (all users)
- Push global notifications/announcements
- Deactivate/reactivate accounts
- Audit logs (role changes, exports, password resets)

Admin Use Cases
- Authenticate to admin portal
- View admin dashboard (team stats, pending approvals)
- Manage team members (add/remove, assign to projects)
- Configure team-level rules (work hours, approval flow)
- Review submitted timesheets (team scope)
- Approve/Reject timesheets with comments
- Review submitted expenses (team scope)
- Approve/Reject expenses with comments
- Export team timesheets (CSV/Excel/PDF/Print)
- Export team expenses (CSV/Excel/PDF/Print)
- Reset team member passwords
- Send team notifications/announcements
- Generate reports by date range/project/member
- Manage projects (create/edit/archive) within scope

User Use Cases
- Authenticate to user portal
- View user dashboard (status, pending actions)
- Create a new timesheet
- Add time entries (project/task/date/hours/notes)
- Edit/delete draft time entries
- Submit timesheet for approval
- View timesheet history and statuses (Draft/Submitted/Approved/Rejected)
- Respond to rejection (edit and resubmit)
- Create a new expense report
- Add expense items (date/category/amount/receipt upload)
- Edit/delete draft expense items
- Submit expense report for approval
- View expense history and statuses
- Download/export personal timesheets/expenses
- Change password and profile settings
- Receive notifications (approval/rejection/status updates)

Cross-cutting Use Cases
- Authentication & session management (cookies)
- Authorization per role (Super-Admin/Admin/User)
- Data validation (client: jQuery Validate; server-side)
- File upload validation for receipts (size/type; `jquery.documentvalidate.js`)
- Reporting and export (DataTables, buttons)
- Notifications (toastr/in-app alerts)
- Search, filter, pagination (DataTables)
- Audit trail (who approved/rejected, when)
- Error handling, logging

Workflow Outlines
- Timesheet
  - User drafts -> adds entries -> submits
  - Admin reviews -> approves or rejects (comment)
  - User edits per feedback -> resubmits
  - Admin exports team reports; Super-Admin exports master reports
- Expense
  - User drafts -> adds items/uploads receipts -> submits
  - Admin reviews -> approves or rejects (comment)
  - User edits per feedback -> resubmits
  - Admin exports team expenses; Super-Admin exports master expenses
- Team/Project management
  - Super-Admin creates admins and assigns roles
  - Admin creates projects/teams, assigns members
  - Users select assigned projects on timesheets/expenses
- Password & security
  - User changes own password
  - Admin resets team member password
  - Super-Admin resets any password

Future DDD Mapping
- Aggregates: `User`, `Role`, `Team`, `Project`, `TimeSheet` (owns `TimeEntry`), `ExpenseReport` (owns `ExpenseItem`)
- Application commands/queries implement the above use cases with validators and handlers
- Repositories in Infrastructure; Razor Pages call Application handlers only
