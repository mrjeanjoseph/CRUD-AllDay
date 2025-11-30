using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TimesheetManagement.Infrastructure;
using TimesheetManagement.Application;
using TimesheetManagement.Models;

namespace TimesheetManagement.Controllers
{
    public class DemoAssignController : Controller
    {
        private IAssignRoles _IAssignRoles;
        public DemoAssignController()
        {
            _IAssignRoles = new AssignRolesConcrete();
        }
        // GET: DemoAssign
        public ActionResult Index()
        {
            try
            {
                AssignRolesModel assignRolesModel = new AssignRolesModel();
                assignRolesModel.ListofAdmins = _IAssignRoles.ListofAdmins();
                assignRolesModel.ListofUser = _IAssignRoles.GetListofUnAssignedUsers();
                return View(assignRolesModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Index(List<UserModel> list, AssignRolesModel assignRolesModel)
        {
            try
            {
                if (assignRolesModel.ListofUser == null)
                {
                    TempData["MessageErrorRoles"] = "There are no Users to Assign Roles";
                    assignRolesModel.ListofAdmins = _IAssignRoles.ListofAdmins();
                    assignRolesModel.ListofUser = _IAssignRoles.GetListofUnAssignedUsers();
                    return View(assignRolesModel);
                }

                if (ModelState.IsValid)
                {
                    if (HttpContext.Session.TryGetValue("SuperAdmin", out var superAdminBytes))
                    {
                        var superAdminString = System.Text.Encoding.UTF8.GetString(superAdminBytes);
                        assignRolesModel.CreatedBy = Convert.ToInt32(superAdminString);
                    }
                    _IAssignRoles.SaveAssignedRoles(assignRolesModel);
                    TempData["MessageRoles"] = "Roles Assigned Successfully!";
                }

                assignRolesModel = new AssignRolesModel();
                assignRolesModel.ListofAdmins = _IAssignRoles.ListofAdmins();
                assignRolesModel.ListofUser = _IAssignRoles.GetListofUnAssignedUsers();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}