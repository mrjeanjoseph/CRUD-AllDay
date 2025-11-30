using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebTimeSheetManagement.Concrete;
using WebTimeSheetManagement.Filters;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Controllers
{
    [ValidateSuperAdminSession]
    public class AddNotificationController : Controller
    {
        INotification _INotification;
        public AddNotificationController() => _INotification = new NotificationConcrete();

        [HttpGet]
        public ActionResult Add() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAsync(NotificationsTB NotificationsTB,
            [FromServices] IHubContext<Hubs.MyNotificationHub> hubContext)
        {
            try
            {
                if (!ModelState.IsValid) return View(NotificationsTB);
                _INotification.DisableExistingNotifications();
                var Notifications = new NotificationsTB
                {
                    CreatedOn = DateTime.Now,
                    Message = NotificationsTB.Message,
                    NotificationsID = 0,
                    Status = "A",
                    FromDate = NotificationsTB.FromDate,
                    ToDate = NotificationsTB.ToDate
                };
                _INotification.AddNotification(Notifications);
                await hubContext.Clients.All.SendAsync("displayStatus");
                return RedirectToAction("Add", "AddNotification");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult AllNotification() => View();

        public ActionResult LoadNotificationData()
        {
            try
            {
                var draw = Request.Form.TryGetValue("draw", out var drawValues) ? drawValues.FirstOrDefault() : null;
                var start = Request.Form.TryGetValue("start", out var startValues) ? startValues.FirstOrDefault() : null;
                var length = Request.Form.TryGetValue("length", out var lengthValues) ? lengthValues.FirstOrDefault() : null;
                var orderColumnIndex = Request.Form.TryGetValue("order[0][column]", out var orderColumnIndexValues) ? orderColumnIndexValues.FirstOrDefault() : null;
                var sortColumnKey = $"columns[{orderColumnIndex}][name]";
                var sortColumn = Request.Form.TryGetValue(sortColumnKey, out var sortColumnValues) ? sortColumnValues.FirstOrDefault() : null;
                var sortColumnDir = Request.Form.TryGetValue("order[0][dir]", out var sortColumnDirValues) ? sortColumnDirValues.FirstOrDefault() : null;
                var searchValue = Request.Form.TryGetValue("search[value]", out var searchValueValues) ? searchValueValues.FirstOrDefault() : null;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var notificationdata = _INotification.ShowNotifications(sortColumn, sortColumnDir, searchValue);
                var recordsTotal = notificationdata.Count();
                var data = notificationdata.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult DeActivateNotification(string NotificationID)
        {
            try
            {
                if (string.IsNullOrEmpty(NotificationID)) return Json("Error");
                var result = _INotification.DeActivateNotificationByID(Convert.ToInt32(NotificationID));
                return Json(result ? true : false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}