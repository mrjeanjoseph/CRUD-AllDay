using System.Collections.Generic;
using TimesheetManagement.Models;

namespace TimesheetManagement.Application
{
    public interface IDocument
    {
        int AddDocument(Documents Documents);
        Documents GetDocumentByExpenseID(int? ExpenseID, int? DocumentID);
        List<DocumentsVM> GetListofDocumentByExpenseID(int? ExpenseID);
    }
}
