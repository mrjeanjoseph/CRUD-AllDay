using System.Collections.Generic;
using System.Linq;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Interface
{
    public interface IProject
    {
        List<ProjectMaster> GetListofProjects();
        bool CheckProjectCodeExists(string ProjectCode);
        bool CheckProjectNameExists(string ProjectName);
        int SaveProject(ProjectMaster ProjectMaster);
        IQueryable<ProjectMaster> ShowProjects(string sortColumn, string sortColumnDir, string Search);
        bool CheckProjectIDExistsInTimesheet(int ProjectID);
        bool CheckProjectIDExistsInExpense(int ProjectID);
        int ProjectDelete(int ProjectID);
        int GetTotalProjectsCounts();
    }
}
