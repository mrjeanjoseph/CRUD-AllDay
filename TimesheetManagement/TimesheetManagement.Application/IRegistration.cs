using System.Linq;
using TimesheetManagement.Models;

namespace TimesheetManagement.Application
{
    public interface IRegistration
    {
        bool CheckUserNameExists(string Username);
        int AddUser(Registration entity);
        IQueryable<Registration> ListofRegisteredUser(string sortColumn, string sortColumnDir, string Search);
        bool UpdatePassword(string RegistrationID, string Password);
    }
}
