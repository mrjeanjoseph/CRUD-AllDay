using AdventureWorks.Domain.DataAccessLayer;
using AdventureWorks.Domain.Models;
using AdventureWorks.ServiceAPI.Models;
using AdventureWorks.ServiceAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.ServiceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase {
    private readonly IDepartmentService _departmentService;
    private readonly ILogger<DepartmentController> _logger;
    private readonly IMapper _mapper;

    public DepartmentController(ILogger<DepartmentController> logger, AdWDbContext context, IMapper mapper, IDepartmentService service) {
        _logger = logger;
        _mapper = mapper;
        _departmentService = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetDepartments() {
        var departments = await _departmentService.GetAllDepartmentsAsync();

        var departmentdtos = _mapper.Map<List<DepartmentDTO>>(departments);

        return departmentdtos;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDTO>> GetDepartment(short id) {
        var department = await _departmentService.GetDepartmentByIdAsync(id);
        if (department == null) {
            return NotFound();
        }
        return _mapper.Map<DepartmentDTO>(department);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDepartment(int id, DepartmentDTO model) {
        if (id != model.DepartmentId)
            return BadRequest();

        var department = _mapper.Map<Department>(model);
        await _departmentService.UpdateDepartmentAsync(department);

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentDTO>> PostDepartment(DepartmentDTO model) {
        var department = _mapper.Map<Department>(model);
        await _departmentService.AddDepartmentAsync(department);
        return CreatedAtAction(nameof(GetDepartment), new { id = department.DepartmentId }, model);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id) {
        var department = await _departmentService.GetDepartmentByIdAsync(id);

        if (department == null)
            return NotFound();

        await _departmentService.DeleteDepartmentAsync(id);
        return NoContent();
    }
}
