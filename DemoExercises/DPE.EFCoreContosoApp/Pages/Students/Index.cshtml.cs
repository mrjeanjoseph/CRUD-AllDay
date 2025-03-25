using DPE.EFCoreContosoApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DPE.EFCoreContosoApp.Pages.Students;

public class IndexModel : PageModel
{
    private readonly Data.SchoolContext _context;

    public IndexModel(Data.SchoolContext context)
    {
        _context = context;
    }

    public IList<Student> Student { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Student = await _context.Students.ToListAsync();
    }
}
