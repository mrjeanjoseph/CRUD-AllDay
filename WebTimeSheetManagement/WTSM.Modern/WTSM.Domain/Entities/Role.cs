using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WTSM.Domain.Entities;

[Table("Roles")]
public class Roles
{
    [Key]
    public int RoleID { get; set; }
    public string Rolename { get; set; }
}
