using System.Collections.Generic;
using System.Linq;
using TimesheetManagement.Domain;

namespace TimesheetManagement.Application
{
    public interface IAssignRoles
    {
        List<AdminModel> ListofAdmins();
        List<UserModel> ListofUser();
        int UpdateAssigntoAdmin(string AssignToAdminID, string UserID);
        IQueryable<UserModel> ShowallRoles(string sortColumn, string sortColumnDir, string Search);
        bool RemovefromUserRole(string RegistrationID);
        List<UserModel> GetListofUnAssignedUsers();
        bool SaveAssignedRoles(AssignRolesModel AssignRolesModel);
        bool CheckIsUserAssignedRole(int RegistrationID);
    }
}
