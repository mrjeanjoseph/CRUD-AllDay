﻿using AdventureWorks.Domain.DataAccessLayer;
using AdventureWorks.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.ServiceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase {
    private readonly ILogger<DepartmentController> _logger;
    private readonly AdWDbContext _context;

    public DepartmentController(ILogger<DepartmentController> logger, AdWDbContext context) {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments() {
        // This is working as expected
        return await _context.Departments.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetDepartment(short id) {
        var department = await _context.Departments.FindAsync(id);
        if (department == null) {
            return NotFound();
        }
        return department;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDepartment(int id, Department department) {
        if (id != department.DepartmentId) {
            return BadRequest();
        }
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
    public async Task<ActionResult<Department>> PostDepartment(Department department) {
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department);
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
