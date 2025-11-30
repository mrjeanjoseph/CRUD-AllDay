using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.IO;
using System.Linq;
using WebTimeSheetManagement.Concrete;
using WebTimeSheetManagement.Filters;
using WebTimeSheetManagement.Helpers;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Controllers
{
    [ValidateAdminSession]
    public class TimeSheetExportController : Controller
    {
        ITimeSheetExport _ITimeSheetExport;
        ITimeSheet _ITimeSheet;
        public TimeSheetExportController()
        {
            _ITimeSheetExport = new TimeSheetExportConcrete();
            _ITimeSheet = new TimeSheetConcrete();
        }

        [HttpGet]
        public ActionResult Report() => View(new TimeSheetExcelExportModel());

        [HttpPost]
        public ActionResult ExportToExcel(TimeSheetExcelExportModel objtimesheet)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (!ModelState.IsValid) return View("Report", objtimesheet);

                dt.Columns.Add("TotalHours", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("ProjectName", typeof(string));
                dt.Columns.Add("DaysofWeek", typeof(string));
                dt.Columns.Add("Hours", typeof(string));
                dt.Columns.Add("Period", typeof(string));
                dt.Columns.Add("CreatedOn", typeof(string));

                DataSet singlelist = null;
                if (HttpContext.Session.TryGetValue("AdminUser", out var adminUserBytes))
                {
                    var adminUserId = BitConverter.ToInt32(adminUserBytes, 0);
                    singlelist = _ITimeSheetExport.GetReportofTimeSheet(objtimesheet.FromDate, objtimesheet.ToDate, adminUserId);
                }
                else
                {
                    TempData["NoExportMessage"] = "Session expired or AdminUser not found.";
                    return View("Report", objtimesheet);
                }

                if (singlelist != null && singlelist.Tables.Count > 0 && singlelist.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < singlelist.Tables[0].Rows.Count; i++)
                    {
                        int TimeSheetMasterID = Convert.ToInt32(singlelist.Tables[0].Rows[i]["TimeSheetMasterID"]);
                        var multi = _ITimeSheetExport.GetWeekTimeSheetDetails(TimeSheetMasterID);
                        DataRow row = dt.NewRow();
                        row["TotalHours"] = Convert.ToString(singlelist.Tables[0].Rows[i]["TotalHours"]);
                        row["Name"] = Convert.ToString(singlelist.Tables[0].Rows[i]["Name"]);
                        dt.Rows.Add(row);
                        DataRow spacer = dt.NewRow();
                        dt.Rows.Add(spacer);
                        for (int j = 0; j < multi.Tables[0].Rows.Count; j++)
                        {
                            DataRow row2 = dt.NewRow();
                            row2["ProjectName"] = Convert.ToString(multi.Tables[0].Rows[j]["ProjectName"]);
                            row2["DaysofWeek"] = Convert.ToString(multi.Tables[0].Rows[j]["DaysofWeek"]);
                            row2["Hours"] = Convert.ToString(multi.Tables[0].Rows[j]["Hours"]);
                            row2["Period"] = Convert.ToString(multi.Tables[0].Rows[j]["Period"]);
                            row2["CreatedOn"] = Convert.ToString(multi.Tables[0].Rows[j]["CreatedOn"]);
                            dt.Rows.Add(row2);
                        }
                    }
                    ds.Tables.Add(dt);
                    using var sw = new StringWriter();
                    sw.WriteLine(string.Join(",", dt.Columns.OfType<System.Data.DataColumn>().Select(c => c.ColumnName)));
                    foreach (DataRow r in dt.Rows)
                    {
                        sw.WriteLine(string.Join(",", r.ItemArray.Select(f => f?.ToString()?.Replace(",", " "))));
                    }
                    var bytes = System.Text.Encoding.UTF8.GetBytes(sw.ToString());
                    return File(bytes, "text/csv", "TimeSheetDetails.csv");
                }

                TempData["NoExportMessage"] = "No Data to Export";
                return View("Report");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ds.Dispose();
            }
        }

        [HttpGet]
        public ActionResult TimeSheetReport() => View(new TimeSheetExportUserModel());

        [HttpPost]
        public ActionResult TimeSheetReport(TimeSheetExportUserModel objtimesheet)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("ProjectName", typeof(string));
                dt.Columns.Add("Sunday", typeof(string));
                dt.Columns.Add("Monday", typeof(string));
                dt.Columns.Add("Tuesday", typeof(string));
                dt.Columns.Add("Wednesday", typeof(string));
                dt.Columns.Add("Thursday", typeof(string));
                dt.Columns.Add("Friday", typeof(string));
                dt.Columns.Add("Saturday", typeof(string));
                dt.Columns.Add("Total", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                var filename = _ITimeSheetExport.GetUsernamebyRegistrationID(objtimesheet.RegistrationID);
                var timesheetdata = _ITimeSheetExport.GetTimeSheetMasterIDTimeSheet(objtimesheet.FromDate, objtimesheet.ToDate, objtimesheet.RegistrationID);
                if (timesheetdata != null && timesheetdata.Tables.Count > 0 && timesheetdata.Tables[0].Rows.Count > 0)
                {
                    for (int k = 0; k < timesheetdata.Tables[0].Rows.Count; k++)
                    {
                        var timesheetID = Convert.ToInt32(timesheetdata.Tables[0].Rows[k]["TimeSheetMasterID"]);
                        var data = _ITimeSheet.GetPeriodsbyTimeSheetMasterID(Convert.ToInt32(timesheetID));
                        DataRow spacer = dt.NewRow();
                        dt.Rows.Add(spacer);
                        DataRow headerRow = dt.NewRow();
                        headerRow["Sunday"] = data[0].Period;
                        headerRow["Monday"] = data[1].Period;
                        headerRow["Tuesday"] = data[2].Period;
                        headerRow["Wednesday"] = data[3].Period;
                        headerRow["Thursday"] = data[4].Period;
                        headerRow["Friday"] = data[5].Period;
                        headerRow["Saturday"] = data[6].Period;
                        dt.Rows.Add(headerRow);
                        var ListofProjectNames = _ITimeSheet.GetProjectNamesbyTimeSheetMasterID(timesheetID);
                        for (int i = 0; i < ListofProjectNames.Count(); i++)
                        {
                            var ListofHours = MethodonViews.GetHoursbyTimeSheetMasterID(timesheetID, ListofProjectNames[i].ProjectID);
                            var ListofDescription = MethodonViews.GetDescriptionbyTimeSheetMasterID(timesheetID, ListofProjectNames[i].ProjectID);
                            DataRow row1 = dt.NewRow();
                            row1["ProjectName"] = ListofProjectNames[i].ProjectName;
                            row1["Sunday"] = ListofHours[0].Hours;
                            row1["Monday"] = ListofHours[1].Hours;
                            row1["Tuesday"] = ListofHours[2].Hours;
                            row1["Wednesday"] = ListofHours[3].Hours;
                            row1["Thursday"] = ListofHours[4].Hours;
                            row1["Friday"] = ListofHours[5].Hours;
                            row1["Saturday"] = ListofHours[6].Hours;
                            row1["Total"] = ListofHours[7].Hours;
                            row1["Description"] = Convert.ToString(ListofDescription);
                            dt.Rows.Add(row1);
                        }
                    }
                    ds.Tables.Add(dt);
                    using var sw = new StringWriter();
                    sw.WriteLine(string.Join(",", dt.Columns.OfType<System.Data.DataColumn>().Select(c => c.ColumnName)));
                    foreach (DataRow r in dt.Rows)
                    {
                        sw.WriteLine(string.Join(",", r.ItemArray.Select(f => f?.ToString()?.Replace(",", " "))));
                    }
                    var bytes = System.Text.Encoding.UTF8.GetBytes(sw.ToString());
                    return File(bytes, "text/csv", filename.Trim() + ".csv");
                }
                TempData["NoExportMessage"] = "No Data to Export";
                return View("TimeSheetReport", new TimeSheetExportUserModel());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ds.Dispose();
            }
        }

        public JsonResult ListofEmployees()
        {
            try
            {
                int adminUserId = 0;
                if (HttpContext.Session.TryGetValue("AdminUser", out var adminUserBytes))
                {
                    adminUserId = BitConverter.ToInt32(adminUserBytes, 0);
                }
                var ListofEmployees = _ITimeSheetExport.ListofEmployees(adminUserId);
                return Json(ListofEmployees);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}