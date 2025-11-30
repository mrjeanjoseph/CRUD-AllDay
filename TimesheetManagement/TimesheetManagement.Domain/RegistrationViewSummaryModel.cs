using System.ComponentModel.DataAnnotations.Schema;

namespace TimesheetManagement.Models
{
    [NotMapped]
    public class RegistrationViewSummaryModel
    {
        public int RegistrationID { get; set; }
        public string Name { get; set; }
        public string Mobileno { get; set; }
        public string EmailID { get; set; }
        public string Username { get; set; }
        public string AssignToAdmin { get; set; }

    }
}
