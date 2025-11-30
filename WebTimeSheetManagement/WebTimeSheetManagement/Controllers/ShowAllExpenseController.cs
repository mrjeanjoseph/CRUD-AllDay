using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebTimeSheetManagement.Concrete;
using WebTimeSheetManagement.Filters;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Controllers
{
    [ValidateAdminSession]
    public class ShowAllExpenseController : Controller
    {

        IExpense _IExpense;
        IDocument _IDocument;
        IUsers _IUsers;
        public ShowAllExpenseController()
        {
            _IExpense = new ExpenseConcrete();
            _IDocument = new DocumentConcrete();
            _IUsers = new UsersConcrete();
        }
        // GET: ShowAllExpense
        public ActionResult Expense()
        {
            return View();
        }

        public ActionResult LoadExpenseData()
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

                var expensedata = _IExpense.ShowAllExpense(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(HttpContext.Session.GetString("AdminUser")));
                recordsTotal = expensedata.Count();
                var data = expensedata.Skip(skip).Take(pageSize).ToList();

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
                    return RedirectToAction("Expense", "ShowAllExpense");
                }
                var data = _IExpense.ExpenseDetailsbyExpenseID(Convert.ToInt32(id));
                ViewBag.documents = _IDocument.GetListofDocumentByExpenseID(Convert.ToInt32(id));
                return View(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Approval(ExpenseApprovalModel expenseapprovalmodel)
        {
            try
            {

                if (expenseapprovalmodel.Comment == null)
                {
                    return Json(false);
                }

                if (string.IsNullOrEmpty(Convert.ToString(expenseapprovalmodel.ExpenseID)))
                {
                    return Json(false);
                }

                _IExpense.UpdateExpenseStatus(expenseapprovalmodel, 2); //Approve

                if (_IExpense.IsExpenseALreadyProcessed(expenseapprovalmodel.ExpenseID))
                {
                    _IExpense.UpdateExpenseAuditStatus(expenseapprovalmodel.ExpenseID, expenseapprovalmodel.Comment, 2);
                }
                else
                {
                    _IExpense.InsertExpenseAuditLog(InsertExpenseAudit(expenseapprovalmodel, 2));
                }


                return Json(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Rejected(ExpenseApprovalModel expenseapprovalmodel)
        {
            try
            {
                if (expenseapprovalmodel.Comment == null)
                {
                    return Json(false);
                }

                if (string.IsNullOrEmpty(Convert.ToString(expenseapprovalmodel.ExpenseID)))
                {
                    return Json(false);
                }

                _IExpense.UpdateExpenseStatus(expenseapprovalmodel, 3); // Reject


                if (_IExpense.IsExpenseALreadyProcessed(expenseapprovalmodel.ExpenseID))
                {
                    _IExpense.UpdateExpenseAuditStatus(expenseapprovalmodel.ExpenseID, expenseapprovalmodel.Comment, 3);
                }
                else
                {
                    _IExpense.InsertExpenseAuditLog(InsertExpenseAudit(expenseapprovalmodel, 3));
                }

                return Json(true);
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
                if (string.IsNullOrEmpty(Convert.ToString(ExpenseID)))
                {
                    return Json("Error");
                }

                var dataSubmitted = _IExpense.IsExpenseSubmitted(ExpenseID, Convert.ToInt32(HttpContext.Session.GetString("AdminUser")));

                if (dataSubmitted == true)
                {
                    var data = _IExpense.DeleteExpensetByExpenseID(ExpenseID, Convert.ToInt32(HttpContext.Session.GetString("UserID")));

                    if (data > 0)
                    {
                        return Json(true);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
                else
                {
                    return Json("Cannot");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Download(string id, int DocumentID)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(id)) && !string.IsNullOrEmpty(Convert.ToString(DocumentID)))
            {
                var document = _IDocument.GetDocumentByExpenseID(Convert.ToInt32(id), Convert.ToInt32(DocumentID));
                return File(document.DocumentBytes, System.Net.Mime.MediaTypeNames.Application.Octet, document.DocumentName);
            }
            else
            {
                return RedirectToAction("Expense", "ShowAllExpense");
            }
        }

        private ExpenseAuditTB InsertExpenseAudit(ExpenseApprovalModel TimeSheetApproval, int Status)
        {
            try
            {
                ExpenseAuditTB objAuditTB = new ExpenseAuditTB();
                objAuditTB.ApprovaExpenselLogID = 0;
                objAuditTB.ExpenseID = TimeSheetApproval.ExpenseID;
                objAuditTB.Status = Status;
                objAuditTB.CreatedOn = DateTime.Now;
                objAuditTB.Comment = TimeSheetApproval.Comment;
                objAuditTB.ApprovalUser = Convert.ToInt32(HttpContext.Session.GetString("AdminUser"));
                objAuditTB.ProcessedDate = DateTime.Now;
                objAuditTB.UserID = _IUsers.GetUserIDbyExpenseID(TimeSheetApproval.ExpenseID);
                return objAuditTB;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ActionResult SubmittedExpense()
        {
            return View();
        }

        public ActionResult ApprovedExpense()
        {
            return View();
        }

        public ActionResult RejectedExpense()
        {
            return View();
        }

        public ActionResult LoadExpenseSubmittedData()
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

                var expensedata = _IExpense.ShowAllSubmittedExpense(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(HttpContext.Session.GetString("AdminUser")));
                recordsTotal = expensedata.Count();
                var data = expensedata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }


        public ActionResult LoadExpenseApprovedData()
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

                var expensedata = _IExpense.ShowAllApprovedExpense(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(HttpContext.Session.GetString("AdminUser")));
                recordsTotal = expensedata.Count();
                var data = expensedata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult LoadExpenseRejectedData()
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

                var expensedata = _IExpense.ShowAllRejectedExpense(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(HttpContext.Session.GetString("AdminUser")));
                recordsTotal = expensedata.Count();
                var data = expensedata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}