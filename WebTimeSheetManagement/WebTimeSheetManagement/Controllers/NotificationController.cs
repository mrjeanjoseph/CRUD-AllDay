using System;
using System.Web.Mvc;
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
                return Json(NotificationService.GetNotification(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}