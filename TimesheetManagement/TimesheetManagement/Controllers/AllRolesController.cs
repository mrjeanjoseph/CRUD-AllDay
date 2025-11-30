using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using TimesheetManagement.Infrastructure;
using TimesheetManagement.Filters;
using TimesheetManagement.Application;

namespace TimesheetManagement.Controllers
{
    [ValidateSuperAdminSession]
    public class AllRolesController : Controller
    {
        IAssignRoles _IAssignRoles;
        public AllRolesController()
        {
            _IAssignRoles = new AssignRolesConcrete();
        }

        // GET: AllRoles
        public ActionResult Roles()
        {
            return View();
        }

        public ActionResult LoadRolesData()
        {
            try
            {
                StringValues drawValues;
                Request.Form.TryGetValue("draw", out drawValues);
                var draw = drawValues.FirstOrDefault();
                StringValues startValues;
                Request.Form.TryGetValue("start", out startValues);
                var start = startValues.FirstOrDefault();
                StringValues lengthValues;
                Request.Form.TryGetValue("length", out lengthValues);
                var length = lengthValues.FirstOrDefault();
                StringValues orderColumnValues;
                Request.Form.TryGetValue("order[0][column]", out orderColumnValues);
                var orderColumn = orderColumnValues.FirstOrDefault();
                StringValues sortColumnValues;
                Request.Form.TryGetValue($"columns[{orderColumn}][name]", out sortColumnValues);
                var sortColumn = sortColumnValues.FirstOrDefault();
                StringValues sortColumnDirValues;
                Request.Form.TryGetValue("order[0][dir]", out sortColumnDirValues);
                var sortColumnDir = sortColumnDirValues.FirstOrDefault();
                StringValues searchValueValues;
                Request.Form.TryGetValue("search[value]", out searchValueValues);
                var searchValue = searchValueValues.FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                var rolesData = _IAssignRoles.ShowallRoles(sortColumn, sortColumnDir, searchValue);
                recordsTotal = rolesData.Count();
                var data = rolesData.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult RemovefromRole(string RegistrationID)
        {
            try
            {
                if (string.IsNullOrEmpty(RegistrationID))
                {
                    return RedirectToAction("Roles");
                }

                var role = _IAssignRoles.RemovefromUserRole(RegistrationID);
                return Json(role);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }


    }
}