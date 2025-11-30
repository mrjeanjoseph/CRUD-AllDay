using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.IO;
using System.Linq;
using TimesheetManagement.Infrastructure;
using TimesheetManagement.Filters;
using TimesheetManagement.Helpers;
using TimesheetManagement.Application;
using TimesheetManagement.Models;

namespace TimesheetManagement.Controllers
{
    [ValidateSuperAdminSession]
    public class TimeSheetMasterExportController : Controller
    {
        ITimeSheetExport _ITimeSheetExport;
        ITimeSheet _ITimeSheet;
        public TimeSheetMasterExportController()
        {
            _ITimeSheetExport = new TimeSheetExportConcrete();
            _ITimeSheet = new TimeSheetConcrete();
        }

        [HttpGet]
        public ActionResult Report() => View(new TimeSheetExportUserModel());

        [HttpPost]
        public ActionResult Report(TimeSheetExportUserModel objtimesheet)
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
                var filename = "TimesheetMaster";
                var timesheetdata = _ITimeSheetExport.GetTimeSheetMasterIDTimeSheet(objtimesheet.FromDate, objtimesheet.ToDate);
                if (timesheetdata != null && timesheetdata.Tables.Count > 0 && timesheetdata.Tables[0].Rows.Count > 0)
                {
                    for (int k = 0; k < timesheetdata.Tables[0].Rows.Count; k++)
                    {
                        var timesheetID = Convert.ToInt32(timesheetdata.Tables[0].Rows[k]["TimeSheetMasterID"]);
                        var data = _ITimeSheet.GetPeriodsbyTimeSheetMasterID(timesheetID);
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
                    sw.WriteLine(string.Join(",", dt.Columns.Cast<System.Data.DataColumn>().Select(c => c.ColumnName)));
                    foreach (DataRow r in dt.Rows)
                    {
                        sw.WriteLine(string.Join(",", r.ItemArray.Select(f => f?.ToString()?.Replace(",", " "))));
                    }
                    var bytes = System.Text.Encoding.UTF8.GetBytes(sw.ToString());
                    return File(bytes, "text/csv", filename.Trim() + ".csv");
                }
                TempData["NoExportMessage"] = "No Data to Export";
                return View("Report", new TimeSheetExportUserModel());
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
    }
}