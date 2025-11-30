using Microsoft.AspNetCore.Mvc;
using System;
using TimesheetManagement.Filters;
using TimesheetManagement.Service;

namespace TimesheetManagement.Controllers
{
    [ValidateUserSession]
    public class NotificationController : Controller
    {
        public JsonResult GetNotification()
        {
            try
            {
                return Json(NotificationService.GetNotification());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}