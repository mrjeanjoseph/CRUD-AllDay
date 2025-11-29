using System.ComponentModel.DataAnnotations.Schema;

namespace WTSM.Domain.Entities;

[NotMapped]
public class DisplayViewModel
{
    public int ApprovalUser { get; set; }
    public int SubmittedCount { get; set; }
    public int ApprovedCount { get; set; }
    public int RejectedCount { get; set; }
}
