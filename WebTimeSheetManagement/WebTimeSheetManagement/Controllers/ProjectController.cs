using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebTimeSheetManagement.Concrete;
using WebTimeSheetManagement.Filters;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;
namespace WebTimeSheetManagement.Controllers
{
    [ValidateSuperAdminSession]
    public class ProjectController : Controller
    {
        IProject _IProject;
        public ProjectController()
        {
            _IProject = new ProjectConcrete();
        }

        // GET: Project
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        public JsonResult CheckProjectCodeExists(string ProjectCode)
        {
            try
            {
                var isProjectCodeExists = false;

                if (ProjectCode != null)
                {
                    isProjectCodeExists = _IProject.CheckProjectCodeExists(ProjectCode);
                }

                if (isProjectCodeExists)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult CheckProjectNameExists(string ProjectName)
        {
            try
            {
                var isProjectNameExists = false;

                if (ProjectName != null)
                {
                    isProjectNameExists = _IProject.CheckProjectNameExists(ProjectName);
                }

                if (isProjectNameExists)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProjectMaster ProjectMaster)
        {
            if (ModelState.IsValid)
            {
                var result = _IProject.SaveProject(ProjectMaster);

                if (result > 0)
                {
                    TempData["ProjectMessage"] = "Project Added Successfully";
                    ModelState.Clear();
                    return RedirectToAction("Add");
                }
            }

            return View(ProjectMaster);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadProjectData()
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

                var projectdata = _IProject.ShowProjects(sortColumn, sortColumnDir, searchValue);
                recordsTotal = projectdata.Count();
                var data = projectdata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public JsonResult Delete(string ProjectID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(ProjectID)))
                {
                    return Json("Error");
                }

                var isExistsinTimesheet = _IProject.CheckProjectIDExistsInTimesheet(Convert.ToInt32(ProjectID));

                var isExistsinExpense = _IProject.CheckProjectIDExistsInExpense(Convert.ToInt32(ProjectID));

                if (isExistsinTimesheet == false && isExistsinExpense == false)
                {
                    var data = _IProject.ProjectDelete(Convert.ToInt32(ProjectID));

                    if (data > 0)
                    {
                        return Json(data: true);
                    }
                    else
                    {
                        return Json(data: false);
                    }
                }
                else
                {
                    return Json(data: false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}