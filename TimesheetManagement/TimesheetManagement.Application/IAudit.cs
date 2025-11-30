using TimesheetManagement.Models;

namespace TimesheetManagement.Application
{
    public interface IAudit
    {
        void InsertAuditData(AuditTB audittb);
    }
}
