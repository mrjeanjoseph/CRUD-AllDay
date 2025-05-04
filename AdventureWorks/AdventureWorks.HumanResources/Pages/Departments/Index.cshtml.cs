using AdventureWorks.HumanResources.Services;
using AdventureWorks.ServiceAPI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdventureWorks.HumanResources.Pages.Departments;

public class IndexModel : PageModel
{
    private readonly DepartmentService _departmentService;
    private readonly ILogger<IndexModel> _logger; // Add a logger instance for the specific class

    public IndexModel(DepartmentService departmentService, ILogger<IndexModel> logger)
    {
        _departmentService = departmentService;
        _logger = logger; // Initialize the logger
    }

    public IEnumerable<DepartmentDTO> Departments { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            Departments = await _departmentService.GetAllDepartmentsAsync();
        }
        catch (Exception ex)
        {
            // Use the logger instance to log the error
            _logger.LogError(ex, "Failed to load departments.");
            Departments = Enumerable.Empty<DepartmentDTO>();
        }
    }
}
