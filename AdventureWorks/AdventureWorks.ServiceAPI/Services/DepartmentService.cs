using AdventureWorks.Domain.DataAccessLayer;
using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AdventureWorks.ServiceAPI.Services;

public interface IDepartmentService {
    Task<IEnumerable<Department>> GetAllDepartmentsAsync();
    Task<Department> GetDepartmentByIdAsync(int id);
    Task AddDepartmentAsync(Department department);
    Task UpdateDepartmentAsync(Department department);
    Task DeleteDepartmentAsync(int departmentId);
}

public class DepartmentService : IDepartmentService {
    protected readonly AdWDbContext _context;
    protected ILogger<DepartmentService> _logger;

    public DepartmentService(AdWDbContext context, [NotNull] ILogger<DepartmentService> logger) {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync() {
        _logger.LogInformation(_logger.IsEnabled(LogLevel.Information) ? "GetAllDepartmentsAsync called" : "GetAllDepartmentsAsync not called");
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var result = await _context.Departments.ToListAsync();

        _logger.LogTrace("Departments retrieved");
        stopwatch.Stop();
        _logger.LogInformation($"GetAllDepartmentsAsync took {stopwatch.ElapsedMilliseconds} ms");

        return result;
    }

    public async Task<Department> GetDepartmentByIdAsync(int id) {
        var department = await _context.Departments.FindAsync(id);

        if (department == null) 
            throw new KeyNotFoundException($"Department with id {id} not found.");
        
        return department;
    }

    public Task AddDepartmentAsync(Department department) {
        //_context.Departments.Add(department);
        _context.Entry(department).State = EntityState.Added;
        return _context.SaveChangesAsync();
    }

    public async Task UpdateDepartmentAsync(Department department) {
        _context.Entry(department).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDepartmentAsync(int departmentId) {
        var department = await _context.Departments.FindAsync(departmentId);

        if(department != null) {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }
    }
}