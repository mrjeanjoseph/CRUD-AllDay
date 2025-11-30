using System;
using TimesheetManagement.Domain.Identity;

namespace TimesheetManagement.Domain
{
    // Legacy EF-mapped POCO retained temporarily for migration. New domain aggregate: RoleAssignment in Identity folder.
    public class AssignedRoles // Removed data annotations to keep Domain pure; mapping will move to Infrastructure.
    {
        public int AssignedRolesID { get; set; }
        public int? AssignToAdmin { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int RegistrationID { get; set; }
        public string Status { get; set; }
    }
}
