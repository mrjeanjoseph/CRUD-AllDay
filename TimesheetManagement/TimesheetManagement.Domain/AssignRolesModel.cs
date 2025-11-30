using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimesheetManagement.Domain
{
    // Legacy DTO-style models slated to move to Application layer. Kept for transitional compilation only.
    public class AssignRolesModel
    {
        public List<AdminModel> ListofAdmins { get; set; } = new();
        public int RegistrationID { get; set; }
        public List<UserModel> ListofUser { get; set; } = new();
        public int? AssignToAdmin { get; set; }
        public int? CreatedBy { get; set; }
    }

    public class AdminModel
    {
        public string RegistrationID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class UserModel
    {
        public int RegistrationID { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool selectedUsers { get; set; }
        public string AssignToAdmin { get; set; } = string.Empty;
    }
}
