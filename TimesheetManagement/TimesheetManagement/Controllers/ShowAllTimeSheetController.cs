using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TimesheetManagement.Infrastructure;
using TimesheetManagement.Filters;
using TimesheetManagement.Application;
using TimesheetManagement.Models;

namespace TimesheetManagement.Controllers
{
    [ValidateAdminSession]
    public class ShowAllTimeSheetController : Controller
    {
        IProject _IProject;
        IUsers _IUsers;
        ITimeSheet _ITimeSheet;
        public ShowAllTimeSheetController()
        {
            _IProject = new ProjectConcrete();
            _ITimeSheet = new TimeSheetConcrete();
            _IUsers = new UsersConcrete();
        }

        public ActionResult TimeSheet() => View();

        private int GetAdminUserId()
        {
            if (HttpContext.Session.TryGetValue("AdminUser", out var bytes))
            {
                return BitConverter.ToInt32(bytes, 0);
            }
            return 0;
        }

        public ActionResult LoadTimeSheetData()
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
                int recordsTotal = 0;
                var timesheetdata = _ITimeSheet.ShowAllTimeSheet(sortColumn, sortColumnDir, searchValue, GetAdminUserId());
                recordsTotal = timesheetdata.Count();
                var data = timesheetdata.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Details(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) return RedirectToAction("TimeSheet", "AllTimeSheet");
                MainTimeSheetView objMT = new MainTimeSheetView
                {
                    ListTimeSheetDetails = _ITimeSheet.TimesheetDetailsbyTimeSheetMasterID(Convert.ToInt32(id)),
                    ListofProjectNames = _ITimeSheet.GetProjectNamesbyTimeSheetMasterID(Convert.ToInt32(id)),
                    ListofPeriods = _ITimeSheet.GetPeriodsbyTimeSheetMasterID(Convert.ToInt32(id)),
                    ListoDayofWeek = DayofWeek(),
                    TimeSheetMasterID = Convert.ToInt32(id)
                };
                return View(objMT);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [NonAction]
        public List<string> DayofWeek() => new List<string> { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Total" };

        public ActionResult Approval(TimeSheetApproval TimeSheetApproval)
        {
            try
            {
                if (TimeSheetApproval.Comment == null) return Json(false);
                if (TimeSheetApproval.TimeSheetMasterID <= 0) return Json(false);
                _ITimeSheet.UpdateTimeSheetStatus(TimeSheetApproval, 2);
                if (_ITimeSheet.IsTimesheetALreadyProcessed(TimeSheetApproval.TimeSheetMasterID))
                {
                    _ITimeSheet.UpdateTimeSheetAuditStatus(TimeSheetApproval.TimeSheetMasterID, TimeSheetApproval.Comment, 2);
                }
                else
                {
                    _ITimeSheet.InsertTimeSheetAuditLog(InsertTimeSheetAudit(TimeSheetApproval, 2));
                }
                return Json(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Rejected(TimeSheetApproval TimeSheetApproval)
        {
            try
            {
                if (TimeSheetApproval.Comment == null) return Json(false);
                if (TimeSheetApproval.TimeSheetMasterID <= 0) return Json(false);
                _ITimeSheet.UpdateTimeSheetStatus(TimeSheetApproval, 3);
                if (_ITimeSheet.IsTimesheetALreadyProcessed(TimeSheetApproval.TimeSheetMasterID))
                {
                    _ITimeSheet.UpdateTimeSheetAuditStatus(TimeSheetApproval.TimeSheetMasterID, TimeSheetApproval.Comment, 3);
                }
                else
                {
                    _ITimeSheet.InsertTimeSheetAuditLog(InsertTimeSheetAudit(TimeSheetApproval, 3));
                }
                return Json(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private TimeSheetAuditTB InsertTimeSheetAudit(TimeSheetApproval TimeSheetApproval, int Status)
        {
            try
            {
                return new TimeSheetAuditTB
                {
                    ApprovalTimeSheetLogID = 0,
                    TimeSheetID = TimeSheetApproval.TimeSheetMasterID,
                    Status = Status,
                    CreatedOn = DateTime.Now,
                    Comment = TimeSheetApproval.Comment,
                    ApprovalUser = GetAdminUserId(),
                    ProcessedDate = DateTime.Now,
                    UserID = _IUsers.GetUserIDbyTimesheetID(TimeSheetApproval.TimeSheetMasterID)
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult Delete(int TimeSheetMasterID)
        {
            try
            {
                if (TimeSheetMasterID <= 0) return Json("Error");
                var data = _ITimeSheet.DeleteTimesheetByOnlyTimeSheetMasterID(TimeSheetMasterID);
                return Json(data > 0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult SubmittedTimeSheet() => View();
        public ActionResult ApprovedTimeSheet() => View();
        public ActionResult RejectedTimeSheet() => View();

        // Adjusted loader type to match ITimeSheet methods returning IQueryable<TimeSheetMasterView>
        private JsonResult LoadByStatus(Func<string, string, string, int, IQueryable<TimeSheetMasterView>> loader)
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
            var timesheetdata = loader(sortColumn, sortColumnDir, searchValue, GetAdminUserId());
            var recordsTotal = timesheetdata.Count();
            var data = timesheetdata.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
        }

        public ActionResult LoadSubmittedTData() => LoadByStatus(_ITimeSheet.ShowAllSubmittedTimeSheet);
        public ActionResult LoadRejectedData() => LoadByStatus(_ITimeSheet.ShowAllRejectTimeSheet);
        public ActionResult LoadApprovedData() => LoadByStatus(_ITimeSheet.ShowAllApprovedTimeSheet);
    }
}