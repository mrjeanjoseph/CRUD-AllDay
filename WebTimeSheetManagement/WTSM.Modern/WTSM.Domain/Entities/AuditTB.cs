using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WTSM.Domain.Entities;

[Table("AuditTB")]
public class AuditTB
{
    [Key]
    public int AuditID { get; set; }
    public string UserID { get; set; }
    public string SessionID { get; set; }
    public string IPAddress { get; set; }
    public string PageAccessed { get; set; }
    public DateTime? LoggedInAt { get; set; }
    public DateTime? LoggedOutAt { get; set; }
    public string LoginStatus { get; set; }
    public string ControllerName { get; set; }
    public string ActionName { get; set; }
    public string UrlReferrer { get; set; }
}
