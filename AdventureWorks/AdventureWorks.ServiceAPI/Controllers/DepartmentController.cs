using AdventureWorks.Domain.DataAccessLayer;
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

    // GET: api/Department
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetDepartments()
    {
        var departments = await _departmentService.GetAllDepartmentsAsync();
        return Ok(departments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDTO>> GetDepartment(short id)
    {
        var departmentDto = await _departmentService.GetDepartmentByIdAsync(id);
        if (departmentDto == null)
        {
            return NotFound();
        }
        return Ok(departmentDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDepartment(int id, DepartmentDTO model)
    {
        if (id != model.DepartmentId)
            return BadRequest("The ID in the URL does not match the ID in the body.");

        try
        {
            await _departmentService.UpdateDepartmentAsync(model);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex.Message);
            return NotFound(new { message = ex.Message });
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentDTO>> PostDepartment(DepartmentDTO model)
    {
        _logger.LogInformation("PostDepartment called");

        try
        {
            await _departmentService.AddDepartmentAsync(model);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex.Message);
            return Conflict(new { message = ex.Message });
        }

        _logger.LogTrace("Department added");
        return CreatedAtAction(nameof(GetDepartment), new { id = model.DepartmentId }, model);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid department ID.");

        try
        {
            await _departmentService.DeleteDepartmentAsync(id);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex.Message);
            return NotFound(new { message = ex.Message });
        }

        return NoContent();
    }
}
