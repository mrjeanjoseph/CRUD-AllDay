using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimesheetManagement.Domain
{
    // Legacy EF POCO retained for migration; replaced by Project aggregate in Projects folder. reviewed
    public class ProjectMaster
    {
        public int ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public string NatureofIndustry { get; set; }
        public string ProjectName { get; set; }
    }
}
