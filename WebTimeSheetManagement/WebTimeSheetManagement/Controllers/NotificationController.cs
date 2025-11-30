using Microsoft.AspNetCore.Mvc;
using System;
using WebTimeSheetManagement.Filters;
using WebTimeSheetManagement.Service;

namespace WebTimeSheetManagement.Controllers
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