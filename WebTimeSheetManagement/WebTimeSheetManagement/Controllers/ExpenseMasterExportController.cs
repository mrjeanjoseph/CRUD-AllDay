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
    [ValidateSuperAdminSession]
    public class ExpenseMasterExportController : Controller
    {
        IExpenseExport _IExpenseExport;
        public ExpenseMasterExportController()
        {
            _IExpenseExport = new ExpenseExportConcrete();
        }

        public ActionResult Report() => View(new ExpenseExcelExportModel());

        [HttpPost]
        public ActionResult ExportToExcel(ExpenseExcelExportModel objexpense)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (!ModelState.IsValid) return View("Report", objexpense);

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

                var singlelist = _IExpenseExport.GetAllReportofExpense(objexpense.FromDate, objexpense.ToDate);
                if (singlelist != null && singlelist.Tables.Count > 0 && singlelist.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < singlelist.Tables[0].Rows.Count; i++)
                    {
                        DataRow row = dt.NewRow();
                        row["Name"] = Convert.ToString(singlelist.Tables[0].Rows[i]["Name"]);
                        row["ProjectName"] = Convert.ToString(singlelist.Tables[0].Rows[i]["ProjectName"]);
                        row["PurposeorReason"] = Convert.ToString(singlelist.Tables[0].Rows[i]["PurposeorReason"]);
                        row["Status"] = Convert.ToString(singlelist.Tables[0].Rows[i]["Status"]);
                        row["Comment"] = Convert.ToString(singlelist.Tables[0].Rows[i]["Comment"]);
                        row["FromDate"] = Convert.ToString(singlelist.Tables[0].Rows[i]["FromDate"]);
                        row["ToDate"] = Convert.ToString(singlelist.Tables[0].Rows[i]["ToDate"]);
                        row["VoucherID"] = Convert.ToString(singlelist.Tables[0].Rows[i]["VoucherID"]);
                        row["HotelBills"] = Convert.ToString(singlelist.Tables[0].Rows[i]["HotelBills"]);
                        row["TravelBills"] = Convert.ToString(singlelist.Tables[0].Rows[i]["TravelBills"]);
                        row["MealsBills"] = Convert.ToString(singlelist.Tables[0].Rows[i]["MealsBills"]);
                        row["LandLineBills"] = Convert.ToString(singlelist.Tables[0].Rows[i]["LandLineBills"]);
                        row["TransportBills"] = Convert.ToString(singlelist.Tables[0].Rows[i]["TransportBills"]);
                        row["MobileBills"] = Convert.ToString(singlelist.Tables[0].Rows[i]["MobileBills"]);
                        row["Miscellaneous"] = Convert.ToString(singlelist.Tables[0].Rows[i]["Miscellaneous"]);
                        row["TotalAmount"] = Convert.ToString(singlelist.Tables[0].Rows[i]["TotalAmount"]);
                        row["CreatedOn"] = Convert.ToString(singlelist.Tables[0].Rows[i]["CreatedOn"]);
                        dt.Rows.Add(row);
                    }
                    ds.Tables.Add(dt);

                    // Simplified CSV export for ASP.NET Core
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