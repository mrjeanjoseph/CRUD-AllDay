using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Interface
{
    public interface IAudit
    {
        void InsertAuditData(AuditTB audittb);
    }
}
