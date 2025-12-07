using System.ComponentModel.DataAnnotations.Schema;

namespace WebTimeSheetManagement.Models
{
    [NotMapped]
    public class DocumentsVM
    {
        public int DocumentID { get; set; }
        public string DocumentName { get; set; }
        public int ExpenseID { get; set; }
        public string DocumentType { get; set; }
    }
}
