using AdventureWorks.Domain.Models;

namespace AdventureWorks.ServiceAPI.Models; 
public class DepartmentDTO {

    public short DepartmentId { get; set; }
    public string Name { get; set; } = null!;
    public string GroupName { get; set; } = null!;
    public DateTime ModifiedDate { get; set; }
    public virtual ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; set; } = new List<EmployeeDepartmentHistory>();

}
