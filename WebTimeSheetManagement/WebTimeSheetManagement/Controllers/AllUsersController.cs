using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebTimeSheetManagement.Concrete;
using WebTimeSheetManagement.Filters;
using WebTimeSheetManagement.Interface;

namespace WebTimeSheetManagement.Controllers
{
    [ValidateSuperAdminSession]

    public class AllUsersController : Controller
    {
        private IUsers _IUsers;
        public AllUsersController()
        {
            _IUsers = new UsersConcrete();
        }

        // GET: AllUsers
        public ActionResult Users()
        {
            return View();
        }

        public ActionResult LoadUsersData()
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

                var rolesData = _IUsers.ShowallUsers(sortColumn, sortColumnDir, searchValue);
                recordsTotal = rolesData.Count();
                var data = rolesData.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult UserDetails(int? RegistrationID)
        {
            try
            {
                if (RegistrationID == null)
                {

                }
                var userDetailsResponse = _IUsers.GetUserDetailsByRegistrationID(RegistrationID);
                return PartialView("_UserDetails", userDetailsResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult LoadAdminsData()
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

                var rolesData = _IUsers.ShowallAdmin(sortColumn, sortColumnDir, searchValue);
                recordsTotal = rolesData.Count();
                var data = rolesData.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult AdminDetails(int? RegistrationID)
        {
            try
            {
                if (RegistrationID == null)
                {

                }
                var userDetailsResponse = _IUsers.GetAdminDetailsByRegistrationID(RegistrationID);
                return PartialView("_UserDetails", userDetailsResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}