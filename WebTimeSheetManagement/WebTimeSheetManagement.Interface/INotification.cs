using System.Linq;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Interface
{
    public interface INotification
    {
        int AddNotification(NotificationsTB entity);
        void DisableExistingNotifications();
        IQueryable<NotificationsTB_ViewModel> ShowNotifications(string sortColumn, string sortColumnDir, string Search);

        bool DeActivateNotificationByID(int NotificationID);
    }
}
