using System;
using System.Data;

namespace TimesheetManagement.Application
{
    public interface IExpenseExport
    {
        DataSet GetReportofExpense(DateTime? FromDate, DateTime? ToDate, int UserID);
        DataSet GetAllReportofExpense(DateTime? FromDate, DateTime? ToDate);
    }
}
