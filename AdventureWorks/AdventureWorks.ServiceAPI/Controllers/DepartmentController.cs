using AdventureWorks.Domain.DataAccessLayer;
using AdventureWorks.Domain.Models;
using AdventureWorks.ServiceAPI.Models;
using AdventureWorks.ServiceAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace AdventureWorks.ServiceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IDepartmentService _departmentService;
    private readonly ILogger<DepartmentController> _logger;
    private readonly IMapper _mapper;

    public DepartmentController([NotNull] ILogger<DepartmentController> logger, AdWDbContext context, IMapper mapper, IDepartmentService service, IConfiguration configuration)
    {
        _logger = logger;
        _mapper = mapper;
        _departmentService = service;
        _configuration = configuration;
    }

    [HttpGet("connectionString")]
    public IActionResult GetConnectionString() => Ok(_configuration.GetConnectionString("AdWConnStr"));
    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetDepartments()
    {

        _logger.LogInformation("GetDepartments called");

        var departments = await _departmentService.GetAllDepartmentsAsync();

        var departmentdtos = _mapper.Map<List<DepartmentDTO>>(departments);

        var logid = departmentdtos.Take(1).FirstOrDefault();
        _logger.LogTrace("Departments retrieved: {0}", logid);
        return departmentdtos;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDTO>> GetDepartment(short id)
    {
        var department = await _departmentService.GetDepartmentByIdAsync(id);
        if (department == null)
        {
            return NotFound();
        }
        return _mapper.Map<DepartmentDTO>(department);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDepartment(int id, DepartmentDTO model)
    {
        if (id != model.DepartmentId)
            return BadRequest();

        var department = _mapper.Map<Department>(model);
        await _departmentService.UpdateDepartmentAsync(department);

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentDTO>> PostDepartment(DepartmentDTO model)
    {

        _logger.LogInformation("PostDepartment called");

        var department = _mapper.Map<Department>(model);
        await _departmentService.AddDepartmentAsync(department);

        _logger.LogTrace("Department added");
        return CreatedAtAction(nameof(GetDepartment), new { id = department.DepartmentId }, model);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var department = await _departmentService.GetDepartmentByIdAsync(id);

        if (department == null)
            return NotFound();

        await _departmentService.DeleteDepartmentAsync(id);
        return NoContent();
    }
}
