using TimesheetManagement.Domain;

namespace TimesheetManagement.Application
{
    public interface IAudit
    {
        void InsertAuditData(AuditTB audittb);
    }
}
