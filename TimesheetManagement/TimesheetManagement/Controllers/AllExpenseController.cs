using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TimesheetManagement.Infrastructure;
using TimesheetManagement.Filters;
using TimesheetManagement.Application;

namespace TimesheetManagement.Controllers
{
    [ValidateUserSession]
    public class AllExpenseController : Controller
    {
        IExpense _IExpense;
        IDocument _IDocument;
        public AllExpenseController()
        {
            _IExpense = new ExpenseConcrete();
            _IDocument = new DocumentConcrete();
        }
        public ActionResult Expense() => View();

        public ActionResult LoadExpenseData()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault(); // fixed bracket typo
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var v = _IExpense.ShowExpense(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(HttpContext.Session.GetInt32("UserID")));
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult Delete(int ExpenseID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(ExpenseID))) return Json("Error");
                var dataSubmitted = _IExpense.IsExpenseSubmitted(ExpenseID, Convert.ToInt32(HttpContext.Session.GetInt32("UserID")));
                if (dataSubmitted == true)
                {
                    var data = _IExpense.DeleteExpensetByExpenseID(ExpenseID, Convert.ToInt32(HttpContext.Session.GetInt32("UserID")));
                    return Json(data > 0);
                }
                return Json("Cannot");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Details(int ExpenseID)
        {
            try
            {
                var ExpenseDetails = _IExpense.ExpenseDetailsbyExpenseID(ExpenseID);
                ViewBag.documents = _IDocument.GetListofDocumentByExpenseID(ExpenseID);
                return PartialView("_Details", ExpenseDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Download(string ExpenseID, int DocumentID)
        {
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ExpenseID)) && !string.IsNullOrEmpty(Convert.ToString(DocumentID)))
                {
                    var document = _IDocument.GetDocumentByExpenseID(Convert.ToInt32(ExpenseID), Convert.ToInt32(DocumentID));
                    return File(document.DocumentBytes, System.Net.Mime.MediaTypeNames.Application.Octet, document.DocumentName);
                }
                return RedirectToAction("Expense", "ShowAllExpense");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult SubmittedExpense() => View();
        public ActionResult ApprovedExpense() => View();
        public ActionResult RejectedExpense() => View();

        public ActionResult LoadSubmittedExpenseData()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var v = _IExpense.ShowExpenseStatus(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(HttpContext.Session.GetInt32("UserID")), 1);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult LoadApprovedExpenseData()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var v = _IExpense.ShowExpenseStatus(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(HttpContext.Session.GetInt32("UserID")), 2);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult LoadRejectedExpenseData()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var v = _IExpense.ShowExpenseStatus(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(HttpContext.Session.GetInt32("UserID")), 3);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}