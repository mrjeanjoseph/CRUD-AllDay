using AdventureWorks.Domain.Models;

namespace AdventureWorks.ServiceAPI.Models;

public class DepartmentDTO
{
    public short DepartmentId { get; set; }
    public string Name { get; set; } = null!;
    public string GroupName { get; set; } = null!;
    public DateTime ModifiedDate { get; set; }
    public virtual ICollection<EmployeeDepartmentHistoryDto> EmployeeDepartmentHistories { get; set; } = new List<EmployeeDepartmentHistoryDto>();
}


public class EmployeeDepartmentHistoryDto
{
    public short DepartmentId { get; set; }
    public int BusinessEntityId { get; set; }
    public virtual ICollection<EmpDepartmentDetailDTO> EmployeeDetails { get; set; } = new List<EmpDepartmentDetailDTO>();
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}

public class EmpDepartmentDetailDTO
{
    public int BusinessEntityId { get; set; }
    public string LoginId { get; set; } = null!;
    public string JobTitle { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
}