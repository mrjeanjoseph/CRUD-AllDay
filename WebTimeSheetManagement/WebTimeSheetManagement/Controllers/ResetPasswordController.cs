using EventApplicationCore.Library;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebTimeSheetManagement.Concrete;
using WebTimeSheetManagement.Filters;
using WebTimeSheetManagement.Interface;

namespace WebTimeSheetManagement.Controllers
{
    [ValidateSuperAdminSession]
    public class ResetPasswordController : Controller
    {
        IRegistration _IRegistration;
        public ResetPasswordController()
        {
            _IRegistration = new RegistrationConcrete();
        }

        // GET: ResetPassword
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadRegisteredUserData()
        {
            try
            {
                Microsoft.Extensions.Primitives.StringValues drawValues;
                Request.Form.TryGetValue("draw", out drawValues);
                var draw = drawValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues startValues;
                Request.Form.TryGetValue("start", out startValues);
                var start = startValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues lengthValues;
                Request.Form.TryGetValue("length", out lengthValues);
                var length = lengthValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues orderColumnValues;
                Request.Form.TryGetValue("order[0][column]", out orderColumnValues);
                var orderColumn = orderColumnValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues sortColumnValues;
                Request.Form.TryGetValue($"columns[{orderColumn}][name]", out sortColumnValues);
                var sortColumn = sortColumnValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues sortColumnDirValues;
                Request.Form.TryGetValue("order[0][dir]", out sortColumnDirValues);
                var sortColumnDir = sortColumnDirValues.FirstOrDefault();
                Microsoft.Extensions.Primitives.StringValues searchValueValues;
                Request.Form.TryGetValue("search[value]", out searchValueValues);
                var searchValue = searchValueValues.FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                var projectdata = _IRegistration.ListofRegisteredUser(sortColumn, sortColumnDir, searchValue);
                recordsTotal = projectdata.Count();
                var data = projectdata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public JsonResult ResetUserPasswordProcess(string RegistrationID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(RegistrationID)))
                {
                    return Json("Error");
                }

                var Password = EncryptionLibrary.EncryptText("default@123");
                var isPasswordUpdated = _IRegistration.UpdatePassword(RegistrationID, Password);

                if (isPasswordUpdated)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}