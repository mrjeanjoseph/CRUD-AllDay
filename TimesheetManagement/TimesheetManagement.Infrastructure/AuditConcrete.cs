using TimesheetManagement.Application;
using TimesheetManagement.Models;

namespace TimesheetManagement.Infrastructure
{
    public class AuditConcrete : IAudit
    {
        public void InsertAuditData(AuditTB audittb)
        {
            using (var _context = new DatabaseContext())
            {
                _context.AuditTB.Add(audittb);
                _context.SaveChanges();
            }
        }
    }
}
