using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.IO;
using System.Linq;
using WebTimeSheetManagement.Concrete;
using WebTimeSheetManagement.Filters;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Controllers
{
    [ValidateAdminSession]
    public class ExpenseExportController : Controller
    {
        IExpenseExport _IExpenseExport;
        public ExpenseExportController()
        {
            _IExpenseExport = new ExpenseExportConcrete();
        }

        // GET: ExpenseExport
        public ActionResult Report()
        {
            return View(new ExpenseExcelExportModel());
        }

        // GET: TimeSheetExport
        [HttpPost]
        public ActionResult ExportToExcel(ExpenseExcelExportModel objexpense)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {

                if (!ModelState.IsValid)
                {
                    return View("Report", objexpense);
                }

                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("ProjectName", typeof(string));
                dt.Columns.Add("PurposeorReason", typeof(string));
                dt.Columns.Add("TotalAmount", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("Comment", typeof(string));
                dt.Columns.Add("FromDate", typeof(string));
                dt.Columns.Add("ToDate", typeof(string));
                dt.Columns.Add("VoucherID", typeof(string));
                dt.Columns.Add("HotelBills", typeof(string));
                dt.Columns.Add("TravelBills", typeof(string));
                dt.Columns.Add("MealsBills", typeof(string));
                dt.Columns.Add("LandLineBills", typeof(string));
                dt.Columns.Add("TransportBills", typeof(string));
                dt.Columns.Add("MobileBills", typeof(string));
                dt.Columns.Add("Miscellaneous", typeof(string));
                dt.Columns.Add("CreatedOn", typeof(string));

                var adminUserBytes = HttpContext.Session.Get("AdminUser");
                int adminUserId = adminUserBytes != null ? BitConverter.ToInt32(adminUserBytes, 0) : 0;
                var singlelist = _IExpenseExport.GetReportofExpense(objexpense.FromDate, objexpense.ToDate, adminUserId);

                if (singlelist != null && singlelist.Tables.Count > 0 && singlelist.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow sourceRow in singlelist.Tables[0].Rows)
                    {
                        DataRow row = dt.NewRow();
                        row["Name"] = Convert.ToString(sourceRow["Name"]);
                        row["ProjectName"] = Convert.ToString(sourceRow["ProjectName"]);
                        row["PurposeorReason"] = Convert.ToString(sourceRow["PurposeorReason"]);
                        row["Status"] = Convert.ToString(sourceRow["Status"]);
                        row["Comment"] = Convert.ToString(sourceRow["Comment"]);
                        row["FromDate"] = Convert.ToString(sourceRow["FromDate"]);
                        row["ToDate"] = Convert.ToString(sourceRow["ToDate"]);
                        row["VoucherID"] = Convert.ToString(sourceRow["VoucherID"]);
                        row["HotelBills"] = Convert.ToString(sourceRow["HotelBills"]);
                        row["TravelBills"] = Convert.ToString(sourceRow["TravelBills"]);
                        row["MealsBills"] = Convert.ToString(sourceRow["MealsBills"]);
                        row["LandLineBills"] = Convert.ToString(sourceRow["LandLineBills"]);
                        row["TransportBills"] = Convert.ToString(sourceRow["TransportBills"]);
                        row["MobileBills"] = Convert.ToString(sourceRow["MobileBills"]);
                        row["Miscellaneous"] = Convert.ToString(sourceRow["Miscellaneous"]);
                        row["TotalAmount"] = Convert.ToString(sourceRow["TotalAmount"]);
                        row["CreatedOn"] = Convert.ToString(sourceRow["CreatedOn"]);
                        dt.Rows.Add(row);
                    }
                    ds.Tables.Add(dt);
                    using var sw = new StringWriter();
                    sw.WriteLine(string.Join(",", dt.Columns.Cast<System.Data.DataColumn>().Select(c => c.ColumnName)));
                    foreach (DataRow r in dt.Rows)
                    {
                        sw.WriteLine(string.Join(",", r.ItemArray.Select(f => f?.ToString()?.Replace(",", " "))));
                    }
                    var bytes = System.Text.Encoding.UTF8.GetBytes(sw.ToString());
                    return File(bytes, "text/csv", "ExpenseSheet.csv");
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
    }
}