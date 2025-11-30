using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimesheetManagement.Domain
{
    [Table("Roles")]
    public class Roles
    {
        [Key]
        public int RoleID { get; set; }
        public string Rolename { get; set; }
    }
}
