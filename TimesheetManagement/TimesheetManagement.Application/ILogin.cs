using TimesheetManagement.Models;

namespace TimesheetManagement.Application
{
    public interface ILogin
    {
        Registration ValidateUser(string userName, string passWord);
        bool UpdatePassword(string NewPassword, int UserID);
        string GetPasswordbyUserID(int UserID);
    }
}
