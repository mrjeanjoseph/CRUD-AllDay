namespace TimesheetManagement.Domain
{
    public class ExpenseApprovalModel
    {
        public int ExpenseID { get; set; }
        public string Comment { get; set; }
    }

    public class TimeSheetApproval
    {
        public int TimeSheetMasterID { get; set; }
        public string Comment { get; set; }
    }
}
