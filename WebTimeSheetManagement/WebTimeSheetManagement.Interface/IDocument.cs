using System.Collections.Generic;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Interface
{
    public interface IDocument
    {
        int AddDocument(Documents Documents);
        Documents GetDocumentByExpenseID(int? ExpenseID, int? DocumentID);
        List<DocumentsVM> GetListofDocumentByExpenseID(int? ExpenseID);
    }
}
