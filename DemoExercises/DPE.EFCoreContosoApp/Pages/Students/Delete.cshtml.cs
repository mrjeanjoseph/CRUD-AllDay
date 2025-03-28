﻿using DPE.EFCoreContosoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DPE.EFCoreContosoApp.Pages.Students;

public class DeleteModel : PageModel
{
    private readonly Data.SchoolContext _context;

    public DeleteModel(Data.SchoolContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Student Student { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students.FirstOrDefaultAsync(m => m.ID == id);

        if (student == null)
        {
            return NotFound();
        }
        else
        {
            Student = student;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            Student = student;
            _context.Students.Remove(Student);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
