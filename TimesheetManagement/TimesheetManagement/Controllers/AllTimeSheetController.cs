using Microsoft.AspNetCore.Http;
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
    [ValidateUserSession]
    public class AllTimeSheetController : Controller
    {
        IProject _IProject;
        ITimeSheet _ITimeSheet;
        public AllTimeSheetController()
        {
            _IProject = new ProjectConcrete();
            _ITimeSheet = new TimeSheetConcrete();
        }


        // GET: AllTimeSheet
        public ActionResult TimeSheet()
        {
            return View();
        }

        public ActionResult LoadTimeSheetData()
        {
            try
            {
                Microsoft.Extensions.Primitives.StringValues drawValues;
                Request.Form.TryGetValue("draw", out drawValues);
                var draw = drawValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues startValues;
                Request.Form.TryGetValue("start", out startValues);
                var start = startValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues lengthValues;
                Request.Form.TryGetValue("length", out lengthValues);
                var length = lengthValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues orderColumnValues;
                Request.Form.TryGetValue("order[0][column]", out orderColumnValues);
                var orderColumn = orderColumnValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues sortColumnValues;
                Request.Form.TryGetValue($"columns[{orderColumn}][name]", out sortColumnValues);
                var sortColumn = sortColumnValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues sortColumnDirValues;
                Request.Form.TryGetValue("order[0][dir]", out sortColumnDirValues);
                var sortColumnDir = sortColumnDirValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues searchValueValues;
                Request.Form.TryGetValue("search[value]", out searchValueValues);
                var searchValue = searchValueValues.FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;
                var v = _ITimeSheet.ShowTimeSheet(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(HttpContext.Session.GetString("UserID")));
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
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
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("TimeSheet", "AllTimeSheet");
                }
                MainTimeSheetView objMT = new MainTimeSheetView();
                objMT.ListTimeSheetDetails = _ITimeSheet.TimesheetDetailsbyTimeSheetMasterID(Convert.ToInt32(HttpContext.Session.GetString("UserID")), Convert.ToInt32(id));
                objMT.ListofProjectNames = _ITimeSheet.GetProjectNamesbyTimeSheetMasterID(Convert.ToInt32(id));
                objMT.ListofPeriods = _ITimeSheet.GetPeriodsbyTimeSheetMasterID(Convert.ToInt32(id));
                objMT.ListoDayofWeek = DayofWeek();
                objMT.TimeSheetMasterID = Convert.ToInt32(id);
                return View(objMT);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [NonAction]
        public List<string> DayofWeek()
        {
            List<string> li = new List<string>();
            li.Add("Sunday");
            li.Add("Monday");
            li.Add("Tuesday");
            li.Add("Wednesday");
            li.Add("Thursday");
            li.Add("Friday");
            li.Add("Saturday");
            li.Add("Total");
            return li;
        }


        public ActionResult SubmittedTimeSheet()
        {
            return View();
        }

        public ActionResult ApprovedTimeSheet()
        {
            return View();
        }

        public ActionResult RejectedTimeSheet()
        {
            return View();
        }


        public ActionResult LoadSubmittedTimeSheetData()
        {
            try
            {
                Microsoft.Extensions.Primitives.StringValues drawValues;
                Request.Form.TryGetValue("draw", out drawValues);
                var draw = drawValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues startValues;
                Request.Form.TryGetValue("start", out startValues);
                var start = startValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues lengthValues;
                Request.Form.TryGetValue("length", out lengthValues);
                var length = lengthValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues orderColumnValues;
                Request.Form.TryGetValue("order[0][column]", out orderColumnValues);
                var orderColumn = orderColumnValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues sortColumnValues;
                Request.Form.TryGetValue($"columns[{orderColumn}][name]", out sortColumnValues);
                var sortColumn = sortColumnValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues sortColumnDirValues;
                Request.Form.TryGetValue("order[0][dir]", out sortColumnDirValues);
                var sortColumnDir = sortColumnDirValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues searchValueValues;
                Request.Form.TryGetValue("search[value]", out searchValueValues);
                var searchValue = searchValueValues.FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;
                var v = _ITimeSheet.ShowTimeSheetStatus(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(HttpContext.Session.GetString("UserID")), 1);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult LoadRejectedTimeSheetData()
        {
            try
            {
                Microsoft.Extensions.Primitives.StringValues drawValues;
                Request.Form.TryGetValue("draw", out drawValues);
                var draw = drawValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues startValues;
                Request.Form.TryGetValue("start", out startValues);
                var start = startValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues lengthValues;
                Request.Form.TryGetValue("length", out lengthValues);
                var length = lengthValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues orderColumnValues;
                Request.Form.TryGetValue("order[0][column]", out orderColumnValues);
                var orderColumn = orderColumnValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues sortColumnValues;
                Request.Form.TryGetValue($"columns[{orderColumn}][name]", out sortColumnValues);
                var sortColumn = sortColumnValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues sortColumnDirValues;
                Request.Form.TryGetValue("order[0][dir]", out sortColumnDirValues);
                var sortColumnDir = sortColumnDirValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues searchValueValues;
                Request.Form.TryGetValue("search[value]", out searchValueValues);
                var searchValue = searchValueValues.FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;
                var v = _ITimeSheet.ShowTimeSheetStatus(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(HttpContext.Session.GetString("UserID")), 3);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult LoadApprovedTimeSheetData()
        {
            try
            {
                Microsoft.Extensions.Primitives.StringValues drawValues;
                Request.Form.TryGetValue("draw", out drawValues);
                var draw = drawValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues startValues;
                Request.Form.TryGetValue("start", out startValues);
                var start = startValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues lengthValues;
                Request.Form.TryGetValue("length", out lengthValues);
                var length = lengthValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues orderColumnValues;
                Request.Form.TryGetValue("order[0][column]", out orderColumnValues);
                var orderColumn = orderColumnValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues sortColumnValues;
                Request.Form.TryGetValue($"columns[{orderColumn}][name]", out sortColumnValues);
                var sortColumn = sortColumnValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues sortColumnDirValues;
                Request.Form.TryGetValue("order[0][dir]", out sortColumnDirValues);
                var sortColumnDir = sortColumnDirValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues searchValueValues;
                Request.Form.TryGetValue("search[value]", out searchValueValues);
                var searchValue = searchValueValues.FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;
                var v = _ITimeSheet.ShowTimeSheetStatus(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(HttpContext.Session.GetString("UserID")), 2);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}