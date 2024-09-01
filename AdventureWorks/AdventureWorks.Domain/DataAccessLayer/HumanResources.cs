using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Domain.DataAccessLayer;

public partial class AdWDbContext : DbContext {
    #region HumanResources Entities
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; set; }
    public virtual DbSet<EmployeePayHistory> EmployeePayHistories { get; set; }
    public virtual DbSet<JobCandidate> JobCandidates { get; set; }
    public virtual DbSet<Shift> Shifts { get; set; }
    public virtual DbSet<VEmployee> VEmployees { get; set; }
    public virtual DbSet<VEmployeeDepartment> VEmployeeDepartments { get; set; }
    public virtual DbSet<VEmployeeDepartmentHistory> VEmployeeDepartmentHistories { get; set; }
    public virtual DbSet<VJobCandidate> VJobCandidates { get; set; }
    public virtual DbSet<VJobCandidateEducation> VJobCandidateEducations { get; set; }
    public virtual DbSet<VJobCandidateEmployment> VJobCandidateEmployments { get; set; }

    #endregion

}
