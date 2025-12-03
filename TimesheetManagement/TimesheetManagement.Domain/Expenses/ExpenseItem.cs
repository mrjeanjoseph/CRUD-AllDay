using System;
using TimesheetManagement.Domain.Common;
using TimesheetManagement.Domain.Expenses.ValueObjects;

namespace TimesheetManagement.Domain.Expenses;
public class ExpenseItem : Entity
{
    public DateOnly Date { get; private set; }
    public string Category { get; private set; } = string.Empty;
    public Money Amount { get; private set; }
    public string ReceiptPath { get; private set; }
    public string Notes { get; private set; }

    private ExpenseItem() { }

    public ExpenseItem(DateOnly date, string category, Money amount, string receiptPath = null, string notes = null)
    {
        if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException("Category required", nameof(category));
        Date = date;
        Category = category.Trim();
        Amount = amount;
        ReceiptPath = receiptPath;
        Notes = notes;
    }

    public void Update(string category, Money amount, string receiptPath, string notes)
    {
        if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException("Category required", nameof(category));
        Category = category.Trim();
        Amount = amount;
        ReceiptPath = receiptPath;
        Notes = notes;
    }
}
