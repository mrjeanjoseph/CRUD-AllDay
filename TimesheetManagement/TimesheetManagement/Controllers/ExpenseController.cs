using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using TimesheetManagement.Infrastructure;
using TimesheetManagement.Filters;
using TimesheetManagement.Application;
using TimesheetManagement.Domain;

namespace TimesheetManagement.Controllers
{
    [ValidateUserSession]
    public class ExpenseController : Controller
    {
        IExpense _IExpense;
        IDocument _IDocument;
        IProject _IProject;
        IUsers _IUsers;
        public ExpenseController()
        {
            _IExpense = new ExpenseConcrete();
            _IProject = new ProjectConcrete();
            _IDocument = new DocumentConcrete();
            _IUsers = new UsersConcrete();
        }

        public ActionResult Add() => View(new ExpenseModel());

        [HttpPost]
        public ActionResult Add(ExpenseModel expensemodel, IFormFileCollection files)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Documents Documents = new Documents();
                    var userId = HttpContext.Session.GetInt32("UserID") ?? 0;
                    if (_IExpense.CheckIsDateAlreadyUsed(expensemodel.FromDate, expensemodel.ToDate, userId))
                    {
                        ModelState.AddModelError("", "Date you have choosen is already used !");
                        return View(expensemodel);
                    }
                    expensemodel.ExpenseID = 0;
                    expensemodel.CreatedOn = DateTime.Now;
                    expensemodel.ExpenseStatus = 1;
                    expensemodel.UserID = userId;
                    var ExpenseID = _IExpense.AddExpense(expensemodel);
                    if (ExpenseID > 0)
                    {
                        if (files != null)
                        {
                            foreach (var file in files)
                            {
                                if (file != null && file.Length > 0)
                                {
                                    string fileName = Path.GetFileName(file.FileName);
                                    Documents.DocumentID = 0;
                                    Documents.DocumentName = fileName;
                                    using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                                    {
                                        byte[] FileSize = binaryReader.ReadBytes((int)file.Length);
                                        Documents.DocumentBytes = FileSize;
                                        Documents.CreatedOn = DateTime.Now;
                                    }
                                    Documents.ExpenseID = ExpenseID;
                                    Documents.UserID = userId;
                                    var ext = Path.GetExtension(file.FileName);
                                    Documents.DocumentType = (ext == ".zip" || ext == ".rar") ? "Multi" : "Single";
                                    _IDocument.AddDocument(Documents);
                                }
                            }
                        }
                        TempData["ExpenseMessage"] = "Data Saved Successfully";
                        _IExpense.InsertExpenseAuditLog(InsertExpenseAudit(ExpenseID, 1));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Please Upload Required Attachments");
                        return View(expensemodel);
                    }
                    ModelState.Clear();
                    return View(new ExpenseModel());
                }
                return View(new ExpenseModel());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult ListofProjects()
        {
            try
            {
                var listofProjects = _IProject.GetListofProjects();
                return Json(listofProjects);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ExpenseAuditTB InsertExpenseAudit(int ExpenseID, int Status)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserID") ?? 0;
                ExpenseAuditTB objAuditTB = new ExpenseAuditTB
                {
                    ApprovaExpenselLogID = 0,
                    ExpenseID = ExpenseID,
                    Status = Status,
                    CreatedOn = DateTime.Now,
                    Comment = string.Empty,
                    ApprovalUser = _IUsers.GetAdminIDbyUserID(userId),
                    ProcessedDate = DateTime.Now,
                    UserID = userId
                };
                return objAuditTB;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}