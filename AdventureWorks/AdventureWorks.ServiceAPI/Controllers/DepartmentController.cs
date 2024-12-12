using AdventureWorks.Domain.DataAccessLayer;
using AdventureWorks.Domain.Models;
using AdventureWorks.ServiceAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.ServiceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase {
    private readonly ILogger<DepartmentController> _logger;
    private readonly IMapper _mapper;
    private readonly AdWDbContext _context;

    public DepartmentController(ILogger<DepartmentController> logger, AdWDbContext context, IMapper mapper) {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetDepartments() {
        var departments = await _context.Departments.ToListAsync();

        return _mapper.Map<List<DepartmentDTO>>(departments);        
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDTO>> GetDepartment(short id) {
        var department = await _context.Departments.FindAsync(id);
        if (department == null) {
            return NotFound();
        }
        return _mapper.Map<DepartmentDTO>(department);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDepartment(int id, DepartmentDTO dto) {
        if (id != dto.DepartmentId) {
            return BadRequest();
        }
        var department = _mapper.Map<Department>(dto);
        _context.Entry(department).State = EntityState.Modified;
        try {
            await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException) {
            if (!DepartmentExists(id)) {
                return NotFound();
            } else {
                throw;
            }
        }
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentDTO>> PostDepartment(DepartmentDTO dto) {
        var department = _mapper.Map<Department>(dto);
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id) {
        var department = await _context.Departments.FindAsync(id);
        if (department == null) {
            return NotFound();
        }
        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool DepartmentExists(int id) {
        return _context.Departments.Any(e => e.DepartmentId == id);
    }
}
